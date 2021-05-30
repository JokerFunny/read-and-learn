using Fb2.Document;
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

            string cover = fb2Document.BinaryImages
                .FirstOrDefault(i => i.Attributes["id"] == "cover.jpg")?.Content 
                ?? null;

            // MAGIC OF CONVERSION
            Ebook ebook = new Ebook()
            {
                Id = Guid.NewGuid(),
                Path = fullPath,
                Sections = null, ///add magic
                Author = fB2File?.TitleInfo?.BookAuthors?.ToString(),
                Cover = cover,
                Description = ((FB2Library.Elements.SimpleText)((FB2Library.Elements.ParagraphItem)fB2File?.TitleInfo?.Annotation?.Content?.FirstOrDefault())?
                    .ParagraphData?.FirstOrDefault())?.Text ?? string.Empty,
                Language = fB2File?.TitleInfo?.Language ?? "en",
                Title = fB2File?.TitleInfo?.BookTitle?.Text ?? string.Empty
            };

            return ebook;
        }

        public Task<FormattedBook> PrepareFormattedData(Ebook book)
        {
            throw new System.NotImplementedException();
        }

        private class FB2ToAppropriateFormatParser
        {

        }
    }
}
