using Fb2.Document;
using Fb2.Document.Models;
using Fb2.Document.Models.Base;
using Read_and_learn.Model;
using Read_and_learn.Model.Bookshelf;
using Read_and_learn.Model.DataStructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Fb2Document fb2Document = new Fb2Document();
            string fileContent = await _fileService.ReadFileContent(bookId);

            fb2Document.Load(fileContent);

            var parser = new FB2ToAppropriateFormatParser(fb2Document);

            // MAGIC OF CONVERSION
            Ebook ebook = parser.ParseTargetFB2Book();
            ebook.Path = fullPath;

            return ebook;
        }

        private class FB2ToAppropriateFormatParser
        {
            private Fb2Document _fb2Document;

            private int _currentInSectionPosition;
            private static List<string> _missedTypes = new List<string>();
            private static Regex _itemsRegex = new Regex(@"\w+|\W+", RegexOptions.Compiled);
            private static Regex _wordRegex = new Regex(@"\w+", RegexOptions.Compiled);

            internal FB2ToAppropriateFormatParser(Fb2Document fb2Document)
            {
                _fb2Document = fb2Document;
            }

            internal Ebook ParseTargetFB2Book()
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                Ebook ebook = new Ebook()
                {
                    Id = Guid.NewGuid(),
                    Title = _GetTitle(),
                    Author = _GetAuthor(),
                    Description = _GetDescription(),
                    Language = _GetLanguage(),
                    Cover = _GetCoverImage()
                };

                var sections = _GetSections();
                var navigation = _GetNavigationItems(sections);

                ebook.Sections = sections;
                ebook.Navigation = navigation;

                stopWatch.Stop();

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
                var authorElement = ((Author)_fb2Document.Title?.Content?
                    .Where(c => c.Name == "author")
                    .FirstOrDefault());

                string firstName = ((FirstName)authorElement.Content?
                    .Where(c => c.Name == "first-name")?
                    .FirstOrDefault())?.Content ?? null;

                string middleName = ((MiddleName)authorElement.Content?
                    .Where(c => c.Name == "middle-name")?
                    .FirstOrDefault())?.Content ?? null;

                string lastName = ((LastName)authorElement.Content?
                    .Where(c => c.Name == "last-name")?
                    .FirstOrDefault())?.Content ?? null;

                return $"{firstName} {middleName} {lastName}";
            }

            private List<Section> _GetSections()
            {
                var result = new List<Section>();

                IEnumerable<BookBody> bodies = _fb2Document.Bodies;
                int positionCounter = 0;

                foreach (var body in bodies)
                {
                    var sec = new Section()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = _GetSectionTitle(body, positionCounter),
                        Position = positionCounter,
                        Depth = 0,
                        Elements = new List<Element>()
                    };

                    // handle section elements.
                    _currentInSectionPosition = 0;

                    var innerAdditionalElements = body.Content?
                        .Where(c => c.Name == "title" || c.Name == "epigraph");

                    if (innerAdditionalElements.Any())
                        sec.Elements = _GetElementsFromTargetNodes(innerAdditionalElements, positionCounter);

                    // handle structure for notes.
                    if (body.Attributes != null && body.Attributes["name"] == "notes")
                    {
                        var innerSections = body.Content?
                            .Where(c => c.Name == "section");

                        var innerNotesElements = innerSections.SelectMany(s => ((BodySection)s).Content?.Where(c => c.Name != "image" && c.Name != "table" && c.Name != "section"));

                        if (innerNotesElements.Any())
                            sec.Elements.AddRange(_GetElementsFromTargetNodes(innerNotesElements, positionCounter));

                    }
                    // handle inner sections in any other case.
                    else
                    {
                        // increment section position before proceed inner sections.
                        positionCounter++;

                        result.AddRange(_HadleInnerSections(body, ref positionCounter));
                    }

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
                    var sec = new Section()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = _GetSectionTitle(section, positionCounter),
                        Position = positionCounter,
                        Depth = 0,
                        Elements = new List<Element>()
                    };

                    // handle inner section elements.
                    _currentInSectionPosition = 0;

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
            private List<Element> _GetElementsFromTargetNodes(IEnumerable<Fb2Node> targetContent, int parentPosition)
            {
                var result = new List<Element>();

                foreach (var content in targetContent)
                {
                    if (content is Epigraph)
                    {
                        Epigraph epigraph = content as Epigraph;
                        var epigraphElements = epigraph.Content?.Where(c => c.Name != "cite");

                        if (epigraphElements.Any())
                            result.AddRange(_GetElementsFromTargetNodes(epigraphElements, parentPosition));
                    }
                    else if (content is Title)
                    {
                        Title title = content as Title;
                        var titleContent = title.Content;

                        if (titleContent.Count > 0)
                            result.AddRange(_GetElementsFromTargetNodes(titleContent, parentPosition));
                    }
                    else if (content is SubTitle)
                    {
                        SubTitle subTitle = content as SubTitle;
                        var subTitleContent = subTitle.Content;

                        if (subTitleContent.Count > 0)
                            result.AddRange(_GetElementsFromTargetNodes(subTitleContent, parentPosition));
                    }
                    else if (content is Paragraph)
                    {
                        Paragraph paragraph = content as Paragraph;

                        var textElements = paragraph.Content?.Where(c => c.Name == "text");

                        if (textElements.Any())
                            result.AddRange(_GetElementsFromTargetNodes(textElements, parentPosition));
                    }
                    else if (content is TextAutor)
                    {
                        TextAutor textAutor = content as TextAutor;

                        var textAutorContent = textAutor.Content;

                        if (textAutorContent.Count > 0)
                            result.AddRange(_GetElementsFromTargetNodes(textAutorContent, parentPosition));
                    }
                    else if (content is Poem)
                    {
                        Poem poem = content as Poem;

                        var poemContent = poem.Content;
                        if (poemContent.Count > 0)
                            result.AddRange(_GetElementsFromTargetNodes(poemContent, parentPosition));
                    }
                    else if (content is Quote)
                    {
                        Quote quote = content as Quote;

                        var quoteContent = quote.Content;
                        if (quoteContent.Count > 0)
                            result.AddRange(_GetElementsFromTargetNodes(quoteContent, parentPosition));
                    }
                    else if (content is Emphasis)
                    {
                        Emphasis emphasis = content as Emphasis;

                        var emphasisContent = emphasis.Content;
                        if (emphasisContent.Count > 0)
                            result.AddRange(_GetElementsFromTargetNodes(emphasisContent, parentPosition));
                    }
                    else if (content is Annotation)
                    {
                        Annotation annotation = content as Annotation;

                        var annotationContent = annotation.Content;
                        if (annotationContent.Count > 0)
                            result.AddRange(_GetElementsFromTargetNodes(annotationContent, parentPosition));
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
                                    Position = new Position(parentPosition, _currentInSectionPosition++)
                                };

                                result.Add(newElement);
                            }

                            result.Add(new Element()
                            {
                                Value = null,
                                Type = ElementType.NewLine,
                                Position = new Position(parentPosition, _currentInSectionPosition++)
                            });
                        }
                    }
                    else if (content is EmptyLine)
                    {
                        result.Add(new Element()
                        {
                            Value = null,
                            Type = ElementType.NewLine,
                            Position = new Position(parentPosition, _currentInSectionPosition++)
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

            private List<Navigation> _GetNavigationItems(List<Section> bookSections)
            {
                var result = new List<Navigation>();

                foreach (var section in bookSections)
                {
                    result.Add(new Navigation()
                    {
                        Id = section.Id,
                        Title = section.Title,
                        Position = section.Position,
                        Depth = section.Depth
                    });
                }

                return result.OrderBy(r => r.Position)
                    .ThenBy(r => r.Depth)
                    .ToList();
            }
        }
    }
}
