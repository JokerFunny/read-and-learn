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
        IBookService _bookService;
        IBookRepository _bookRepository;
        IBookmarkRepository _bookmarkRepository;

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="fileService">Target <see cref="IFileHelper"/></param>
        /// <param name="cryptoService">Target <see cref="ICryptoService"/></param>
        /// <param name="bookService">Target <see cref="IBookService"/></param>
        /// <param name="bookRepository">Target <see cref="IBookRepository"/></param>
        /// <param name="bookmarkRepository">Target <see cref="IBookmarkRepository"/></param>
        public BookshelfService(IFileService fileService, ICryptoService cryptoService, IBookService bookService,
            IBookRepository bookRepository, IBookmarkRepository bookmarkRepository)
        {
            _fileService = fileService;
            _cryptoService = cryptoService;
            _bookService = bookService;
            _bookRepository = bookRepository;
            _bookmarkRepository = bookmarkRepository;
        }

        public async Task<Tuple<Book, bool>> AddBook(FileResult file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            var newBook = false;

            if (!file.FileName.EndsWith(".fb2"))
                throw new UnknownFileFormatException(file.FileName);

            byte[] fileContent = await _fileService.GetByteArrayFromFile(file);

            string id = _cryptoService.GetMd5(fileContent);
            Book bookshelfBook = await _bookRepository.GetBookByIdAsync(id);

            if (bookshelfBook == null)
            {
                Ebook ebook = await _bookService.GetBook(file, id);

                bookshelfBook = _bookService.CreateBookshelfBook(ebook);
                bookshelfBook.Id = id;
                bookshelfBook.Path = ebook.Path;

                await _bookRepository.SaveBookAsync(bookshelfBook);
                newBook = true;
            }

            return new Tuple<Book, bool>(bookshelfBook, newBook);
        }

        public async Task<List<Book>> LoadBooks()
            => await _bookRepository.GetAllBooksAsync();

        public async Task<bool> RemoveById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book != null)
            {
                // delete local book copy.
                await _fileService.DeleteFolder(book.Id);

                // delete all related bookmarks.
                var bookmarks = await _bookmarkRepository.GetBookmarksByBookIdAsync(id);
                foreach (var bookmark in bookmarks)
                    await _bookmarkRepository.DeleteBookmarkAsync(bookmark);

                return await _bookRepository.DeleteBookAsync(book) > 0;
            }

            return false;
        }

        public async Task<bool> SaveBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            return await _bookRepository.SaveBookAsync(book) != 0;
        }
    }
}
