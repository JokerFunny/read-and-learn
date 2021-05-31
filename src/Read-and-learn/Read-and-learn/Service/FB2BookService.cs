using Fb2.Document;
using Fb2.Document.Models;
using Fb2.Document.Models.Base;
using FB2Library;
using Read_and_learn.Model;
using Read_and_learn.Model.Bookshelf;
using Read_and_learn.Model.DataStructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Read_and_learn.Service.Interface
{
    /// <summary>
    /// Implementation of <see cref="IBookService"/>
    /// </summary>
    public class FB2BookService : IBookService
    {
        private IFileService _fileService;
        public const string DefaultLanguage = "en";

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="fileService">Target <see cref="IFileService"/></param>
        public FB2BookService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public Book CreateBookshelfBook(Ebook book)
            => new Book
            {
                Title = book.Title,
                Path = book.Path,
                Cover = book.Cover
            };

        public async Task<Ebook> OpenBook(string fullPath, string bookId)
            => await _OpenTargetBook(bookId, bookId);

        public async Task<Ebook> GetBook(FileResult targetFile, string bookId)
        {
            Stream fileStream = await targetFile.OpenReadAsync();

            var path = await _fileService.CreateLocalCopy(fileStream, bookId);

            return await _OpenTargetBook(path, bookId);
        }

        private async Task<Ebook> _OpenTargetBook(string fullPath, string bookId)
        {
            FB2File fB2File;
            Fb2Document fb2Document = new Fb2Document();

            string fileContent = await _fileService.ReadFileContent(bookId);

            fB2File = await new FB2Reader().ReadAsync(fileContent);

            fb2Document.Load(fileContent);

            var parser = new FB2ToAppropriateFormatParser(fb2Document, fB2File);

            // MAGIC OF CONVERSION
            Ebook ebook = parser.ParseTargetFB2Book();
            ebook.Path = fullPath;

            return ebook;
        }

        public Task<FormattedBook> PrepareFormattedData(Ebook book)
        {
            throw new NotImplementedException();
        }

        private class FB2ToAppropriateFormatParser
        {
            private Fb2Document _fb2Document;
            private FB2File _fB2File;

            private static List<string> _missedTypes = new List<string>();
            private static Regex _itemsRegex = new Regex(@"\w+|\W+", RegexOptions.Compiled);
            private static Regex _wordRegex = new Regex(@"\w+", RegexOptions.Compiled);

            internal FB2ToAppropriateFormatParser(Fb2Document fb2Document, FB2File fB2File)
            {
                _fb2Document = fb2Document;
                _fB2File = fB2File;
            }

            internal Ebook ParseTargetFB2Book()
            {
                Guid bookId = Guid.NewGuid();

                Ebook ebook = new Ebook()
                {
                    Id = bookId,
                    Title = _GetTitle(),
                    Author = _GetAuthor(),
                    Description = _GetDescription(),
                    Language = _GetLanguage(),
                    Cover = _GetCoverImage(),
                    Sections = _GetSections(),
                    Navigation = _GetNavigationItems()
                };

                return ebook;
            }

            private string _GetCoverImage()
            {
                return _fb2Document.BinaryImages?
                    .FirstOrDefault(i => i.Attributes["id"] == "cover.jpg")?.Content
                    ?? null;
            }

            private string _GetTitle()
            {
                return ((BookTitle)_fb2Document.Title?.Content?
                    .Where(c => c.Name == "book-title")?
                    .FirstOrDefault())?.Content
                    ?? null;
            }

            private string _GetLanguage()
            {
                return ((Lang)_fb2Document.Title?.Content?
                    .Where(c => c.Name == "lang")?
                    .FirstOrDefault())?.Content
                    ?? DefaultLanguage;
            }

            private string _GetDescription()
            {
                return ((Annotation)_fb2Document.Title?.Content?
                    .Where(c => c.Name == "annotation")?
                    .FirstOrDefault())?.Content?
                    .Select(p => ((TextItem)((Paragraph)p).Content[0])?.Content)?
                    .Aggregate((total, part) => total + part)
                    ?? null;
            }

            private string _GetAuthor()
            {
                return string.Join(",",
                    _fB2File?.TitleInfo?.BookAuthors?
                    .Select(a => a.FirstName.Text + " " + a.LastName.Text)?
                    .ToArray() ?? null);
            }

            private List<Section> _GetSections()
            {
                var result = new List<Section>();

                IEnumerable<BookBody> bodies = _fb2Document.Bodies;
                int positionCounter = 0;

                foreach (var body in bodies)
                {
                    var sec = new Section();
                    sec.Id = Guid.NewGuid().ToString();
                    sec.Title = _GetSectionTitle(body, positionCounter);
                    sec.Position = positionCounter;
                    sec.Depth = 0;

                    var innerAdditionalElements = body.Content?
                        .Where(c => c.Name == "title" || c.Name == "epigraph");

                    if (innerAdditionalElements.Any())
                        sec.Elements = _GetElementsFromTargetNodes(innerAdditionalElements, positionCounter);

                    // increment section position before proceed inner sections.
                    positionCounter++;

                    if (body.Attributes != null && body.Attributes["name"] == "notes")
                    {
                        var etsts = body;
                    }

                    result.AddRange(_HadleInnerSections(body, ref positionCounter));

                    result.Add(sec);
                }

                return result;
            }

            private List<Section> _HadleInnerSections(Fb2Container targetContainer, ref int positionCounter, int depth = 0)
            {
                var result = new List<Section>();

                var innerSections = targetContainer.Content?
                    .Where(c => c.Name == "section");

                foreach (BodySection section in innerSections)
                {
                    var sec = new Section();
                    sec.Id = Guid.NewGuid().ToString();
                    sec.Title = _GetSectionTitle(section, positionCounter);
                    sec.Position = positionCounter;
                    sec.Depth = depth;

                    // [TODO]: add support for target elements.
                    // except "section" -> due to inner sections model.
                    var innerAdditionalElements = section.Content?
                        .Where(c => c.Name != "image" && c.Name != "table" && c.Name != "section");

                    if (innerAdditionalElements.Any())
                        sec.Elements = _GetElementsFromTargetNodes(innerAdditionalElements, positionCounter);

                    positionCounter++;

                    result.AddRange(_HadleInnerSections(section, ref positionCounter, depth + 1));
                    result.Add(sec);
                }

                return result;
            }

            private string _GetSectionTitle(Fb2Container targetContainer, int sectionPosition)
            {
                string title = ((Title)targetContainer.Content?
                        .Where(c => c.Name == "title")?
                        .FirstOrDefault())?.Content?
                        .Select(p => ((TextItem)((Paragraph)p).Content[0])?.Content)?
                        .Aggregate((total, part) => total + " " + part)
                        ?? null;

                return $"{sectionPosition + 1} - {title}";
            }

            // add mechanism for element counter.
            // [TODO]: refactor this terrible method...
            private List<Element> _GetElementsFromTargetNodes(IEnumerable<Fb2Node> targetContent, int parentPosition, int elementPosition = 0)
            {
                var result = new List<Element>();

                foreach (var content in targetContent)
                {
                    if (content is Epigraph)
                    {
                        Epigraph epigraph = content as Epigraph;
                        var epigraphElements = epigraph.Content?.Where(c => c.Name != "cite");

                        if (epigraphElements.Any())
                            result.AddRange(_GetElementsFromTargetNodes(epigraphElements, parentPosition, elementPosition));
                    }
                    else if (content is Title)
                    {
                        Title title = content as Title;
                        var titleContent = title.Content;

                        if (titleContent.Count > 0)
                            result.AddRange(_GetElementsFromTargetNodes(titleContent, parentPosition, elementPosition));
                    }
                    else if (content is SubTitle)
                    {
                        SubTitle subTitle = content as SubTitle;
                        var subTitleContent = subTitle.Content;

                        if (subTitleContent.Count > 0)
                            result.AddRange(_GetElementsFromTargetNodes(subTitleContent, parentPosition, elementPosition));
                    }
                    else if (content is Paragraph)
                    {
                        Paragraph paragraph = content as Paragraph;

                        var textElements = paragraph.Content?.Where(c => c.Name == "text");

                        if (textElements.Any())
                            result.AddRange(_GetElementsFromTargetNodes(textElements, parentPosition, elementPosition));
                    }
                    else if (content is TextAutor)
                    {
                        TextAutor textAutor = content as TextAutor;

                        var textAutorContent = textAutor.Content;

                        if (textAutorContent.Count > 0)
                            result.AddRange(_GetElementsFromTargetNodes(textAutorContent, parentPosition, elementPosition));
                    }
                    else if (content is Poem)
                    {
                        Poem poem = content as Poem;

                        var poemContent = poem.Content;
                        if (poemContent.Count > 0)
                            result.AddRange(_GetElementsFromTargetNodes(poemContent, parentPosition, elementPosition));
                    }
                    else if (content is Quote)
                    {
                        Quote quote = content as Quote;

                        var quoteContent = quote.Content;
                        if (quoteContent.Count > 0)
                            result.AddRange(_GetElementsFromTargetNodes(quoteContent, parentPosition, elementPosition));
                    }
                    else if (content is Emphasis)
                    {
                        Emphasis emphasis = content as Emphasis;

                        var emphasisContent = emphasis.Content;
                        if (emphasisContent.Count > 0)
                            result.AddRange(_GetElementsFromTargetNodes(emphasisContent, parentPosition, elementPosition));
                    }
                    else if (content is Annotation)
                    {
                        Annotation annotation = content as Annotation;

                        var annotationContent = annotation.Content;
                        if (annotationContent.Count > 0)
                            result.AddRange(_GetElementsFromTargetNodes(annotationContent, parentPosition, elementPosition));
                    }
                    else if (content is TextItem)
                    {
                        TextItem textItem = content as TextItem;

                        var regexMatches = _itemsRegex.Matches(textItem.Content);

                        if (regexMatches.Count > 0)
                        {
                            var regexResult = regexMatches.Cast<Match>().Select(m => m.Value);

                            foreach (var element in regexResult)
                            {
                                var newElement = new Element()
                                {
                                    Value = element,
                                    Type = _wordRegex.IsMatch(element) 
                                        ? ElementType.Text 
                                        : ElementType.Symbol,
                                    Position = new Position(parentPosition, elementPosition++)
                                };

                                result.Add(newElement);
                            }

                            result.Add(new Element()
                            {
                                Value = null,
                                Type = ElementType.NewLine,
                                Position = new Position(parentPosition, elementPosition)
                            });
                        }
                    }
                    else if (content is EmptyLine)
                    {
                        result.Add(new Element()
                        {
                            Value = null,
                            Type = ElementType.NewLine,
                            Position = new Position(parentPosition, elementPosition)
                        });
                    }
                    // handle missed types to add later.
                    else
                    {
                        string missedType = content.GetType().Name;

                        if (!_missedTypes.Contains(missedType))
                            _missedTypes.Add(missedType);
                    }
                }

                return result;
            }    

            private List<Navigation> _GetNavigationItems()
            {
                var result = new List<Navigation>();



                return result;
            }
        }
    }
}
