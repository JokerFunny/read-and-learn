using Fb2.Document;
using Fb2.Document.Models;
using FB2Library;
using Read_and_learn.Model;
using Read_and_learn.Model.Bookshelf;
using System;
using System.IO;
using System.Linq;
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

            internal FB2ToAppropriateFormatParser(Fb2Document fb2Document, FB2File fB2File)
            {
                _fb2Document = fb2Document;
                _fB2File = fB2File;
            }

            internal Ebook ParseTargetFB2Book()
            {
                Ebook ebook = new Ebook()
                {
                    Id = Guid.NewGuid(),
                    Title = _GetTitle(),
                    Author = _GetAuthor(),
                    Description = _GetDescription(),
                    Language = _GetLanguage(),
                    Cover = _GetCoverImage(),
                    Sections = null,
                    Navigation = new System.Collections.Generic.List<Model.DataStructure.Navigation>()
                };

                return ebook;
            }

            private string _GetCoverImage()
            {
                return _fb2Document.BinaryImages
                    .FirstOrDefault(i => i.Attributes["id"] == "cover.jpg")?.Content
                    ?? null;
            }

            private string _GetTitle()
            {
                return ((BookTitle)_fb2Document.Title?.Content?
                    .Where(c => c.Name == "book-title")
                    .FirstOrDefault())?.Content
                    ?? null;
            }

            private string _GetLanguage()
            {
                return ((Lang)_fb2Document.Title?.Content?
                    .Where(c => c.Name == "lang")
                    .FirstOrDefault())?.Content
                    ?? DefaultLanguage;
            }

            private string _GetDescription()
            {
                return ((Annotation)_fb2Document.Title?.Content?
                    .Where(c => c.Name == "annotation")
                    .FirstOrDefault()).Content?
                    .Select(p => ((TextItem)((Paragraph)p).Content[0])?.Content)?
                    .Aggregate((total, part) => total + part)
                    ?? null;
            }

            private string _GetAuthor()
            {
                return string.Join(",",
                    _fB2File?.TitleInfo?.BookAuthors?
                    .Select(a => a.FirstName.Text + " " + a.LastName.Text)
                    .ToArray() ?? null);
            }
        }
    }
}
