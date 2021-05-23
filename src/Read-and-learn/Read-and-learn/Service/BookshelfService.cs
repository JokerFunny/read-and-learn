using Read_and_learn.Helpers.Exceptions;
using Read_and_learn.Model;
using Read_and_learn.Model.Bookshelf;
using Read_and_learn.PlatformRelatedServices;
using Read_and_learn.Repository.Interface;
using Read_and_learn.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Read_and_learn.Service
{
    /// <summary>
    /// Implementation of <see cref="IBookshelfService"/>.
    /// </summary>
    public class BookshelfService : IBookshelfService
    {
        IFileService _fileService;
        ICryptoService _cryptoService;
        IBookRepository _bookRepository;
        IBookmarkRepository _bookmarkRepository;
        IBookService _bookService;

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="fileService">Target <see cref="IFileHelper"/></param>
        /// <param name="cryptoService">Target <see cref="ICryptoService"/></param>
        /// <param name="bookRepository">Target <see cref="IBookRepository"/></param>
        /// <param name="bookmarkRepository">Target <see cref="IBookmarkRepository"/></param>
        public BookshelfService(IFileService fileService, ICryptoService cryptoService, 
            IBookRepository bookRepository, IBookmarkRepository bookmarkRepository)
        {
            _fileService = fileService;
            _cryptoService = cryptoService;
            _bookRepository = bookRepository;
            _bookmarkRepository = bookmarkRepository;
        }

        public async Task<Tuple<Book, bool>> AddBook(FileResult file)
        {
            var newBook = false;

            if (!file.FileName.EndsWith(".fb2"))
                throw new UnknownFileFormatException(file.FileName);

            byte[] fileContent = await _fileService.GetByteArrayFromFile(file);

            string id = _cryptoService.GetMd5(fileContent);
            Book bookshelfBook = await _bookRepository.GetBookByIdAsync(id);

            if (bookshelfBook == null)
            {
                Ebook ebook = await _bookService.OpenBook(file.FullPath);

                bookshelfBook = _bookService.CreateBookshelfBook(ebook);
                bookshelfBook.Id = id;
                bookshelfBook.Path = file.FullPath;

                await _bookRepository.SaveBookAsync(bookshelfBook);
                newBook = true;
            }

            return new Tuple<Book, bool>(bookshelfBook, newBook);
        }

        public async Task<List<Book>> LoadBooks()
            => await _bookRepository.GetAllBooksAsync();

        public async Task<bool> RemoveById(string id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book != null)
            {
                var deleted = await _fileService.DeleteFolder(book.Path);
                var bookmarks = await _bookmarkRepository.GetBookmarksByBookIDAsync(id);
                foreach (var bookmark in bookmarks)
                {
                    await _bookmarkRepository.DeleteBookmarkAsync(bookmark);
                }
                return await _bookRepository.DeleteBookAsync(book) > 0;
            }

            return false;
        }

        public async Task<bool> SaveBook(Book book)
            => await _bookRepository.SaveBookAsync(book) != 0;

        public async Task<Book> LoadBookById(string id)
            => await _bookRepository.GetBookByIdAsync(id);
    }
}
