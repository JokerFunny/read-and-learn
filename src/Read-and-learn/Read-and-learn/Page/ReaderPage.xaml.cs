﻿using Autofac;
using Read_and_learn.Model;
using Read_and_learn.Model.Bookshelf;
using Read_and_learn.Model.DataStructure;
using Read_and_learn.Model.Message;
using Read_and_learn.PlatformRelatedServices;
using Read_and_learn.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page
{
    /// <summary>
    /// Main page. Shows book page.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReaderPage : ContentPage
    {
        private IBookshelfService _bookshelfService;
        private IMessageBus _messageBus;
        private IBookmarkService _bookmarkService;
        private IBookService _bookService;
        private ITranslateService _translateService;

        private const int _marginTopConstant = 40;
        // rework to be more flexible.
        private int _charactersInOneLine = Device.RuntimePlatform == Device.Android ? 30 : 36;
        private int _linesCount = Device.RuntimePlatform == Device.Android ? 25 : 27;

        private Color _textColor = Color.Black;
        private int _charactersPerPage;
        private int _currentChapter;
        private int _currentPage;
        private Book _bookshelfBook;
        private Ebook _ebook;
        private List<BookPage> _pages;
        private int _fontSize;
        private Position _lastLoadedPosition = new Position();

        /// <summary>
        /// Default ctor.
        /// </summary>
        public ReaderPage()
        {
            InitializeComponent();

            _bookshelfService = IocManager.Container.Resolve<IBookshelfService>();
            _messageBus = IocManager.Container.Resolve<IMessageBus>();
            _bookmarkService = IocManager.Container.Resolve<IBookmarkService>();
            _bookService = IocManager.Container.Resolve<IBookService>();
            _translateService = IocManager.Container.Resolve<ITranslateService>();

            MenuPanel.NavigationPanel.OnChapterChange += _NavigationPanel_OnChapterChange;

            _messageBus.Send(new FullscreenRequestMessage(true));

            _charactersPerPage = _linesCount * _charactersInOneLine;
            _SetFontSize();
            _SetMargin();

            if (UserSettings.Reader.NightMode)
            {
                BackgroundColor = Color.FromRgb(24, 24, 25);
                _textColor = Color.AntiqueWhite;
            }

            NavigationPage.SetHasNavigationBar(this, false);
        }

        public bool IsMenuPanelVisible()
            => MenuPanel.IsVisible;

        public async void LoadBook(Book book)
        {
            _bookshelfBook = book;

            _ebook = await _bookService.OpenBook(book.Path, book.Id);

            _PreparePages(_ebook);

            MenuPanel.NavigationPanel.SetNavigation(_ebook.Navigation);
            _RefreshBookmarks();

            // open target page.
            var position = _bookshelfBook.Position;
            if (position != null)
                _OpenChapter(position.Section, position.SectionPosition);
            else
                _OpenChapter();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _messageBus.Send(new FullscreenRequestMessage(true));
            _SubscribeMessages();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _SaveProgress();
            _UnSubscribeMessages();
        }

        private void _SubscribeMessages()
        {
            _messageBus.Subscribe<ChangeMarginMessage>(_ChangeMargin, new string[] { nameof(ReaderPage) });
            _messageBus.Subscribe<ChangeFontSizeMessage>(_ChangeFontSize, new string[] { nameof(ReaderPage) });
            _messageBus.Subscribe<ApplicationSleepMessage>(_ApplicationSleepSubscriber, new string[] { nameof(ReaderPage) });
            _messageBus.Subscribe<AddBookmarkMessage>(_AddBookmark, new string[] { nameof(ReaderPage) });
            _messageBus.Subscribe<OpenBookmarkMessage>(_OpenBookmark, new string[] { nameof(ReaderPage) });
            _messageBus.Subscribe<DeleteBookmarkMessage>(_DeleteBookmark, new string[] { nameof(ReaderPage) });
            _messageBus.Subscribe<BookmarkNameChangedMessage>(_BookmarkNameChangedHandler, new string[] { nameof(ReaderPage) });
            _messageBus.Subscribe<GoToPageMessage>(_GoToPageHandler, new string[] { nameof(ReaderPage) });
            _messageBus.Subscribe<NavigationKeyMessage>(_NavigationKeyHandler, new string[] { nameof(ReaderPage) });
            _messageBus.Subscribe<OpenReaderMenuMessage>(_OnOpenReaderMenuRequest, new string[] { nameof(ReaderPage) });
            _messageBus.Subscribe<CloseReaderMenuMessage>((msg) => _ShowReaderContent(), new string[] { nameof(ReaderPage) });
        }

        private void _UnSubscribeMessages()
            => _messageBus.UnSubscribe(nameof(ReaderPage));

        /// <summary>
        /// [TODO]
        /// </summary>
        private void _OnSwiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    _OpenPage(0, true, false);
                    break;
                case SwipeDirection.Right:
                    _OpenPage(0, false, true);
                    break;
            }
        }

        private void _ApplicationSleepSubscriber(ApplicationSleepMessage msg)
            => _SaveProgress();

        /// <summary>
        /// [TODO]: check work.
        /// </summary>
        private void _AddBookmark(AddBookmarkMessage msg)
        {
            _bookmarkService.CreateBookmark(DateTimeOffset.Now.ToString(), _bookshelfBook.Id, _bookshelfBook.Position);

            _RefreshBookmarks();
        }

        /// <summary>
        /// [TODO]: check work.
        /// </summary>
        private void _DeleteBookmark(DeleteBookmarkMessage msg)
        {
            _bookmarkService.DeleteBookmark(msg.Bookmark);

            _RefreshBookmarks();
        }

        public void _BookmarkNameChangedHandler(BookmarkNameChangedMessage msg)
        {
            _bookmarkService.SaveBookmark(msg.Bookmark);

            _RefreshBookmarks();
        }

        private async void _RefreshBookmarks()
        {
            var bookmarks = await _bookmarkService.LoadBookmarksByBookId(_bookshelfBook.Id);

            MenuPanel.BookmarksPanel.UpdateBookmarks(bookmarks);
        }

        private void _OpenBookmark(OpenBookmarkMessage msg)
        {
            // check if target section exist in the book (for magic reasons it could not exist).
            var bookSection = _ebook.Sections.ElementAt(msg.Bookmark.Position.Section);
            if (bookSection != null)
            {
                _OpenChapter(msg.Bookmark.Position.Section, msg.Bookmark.Position.SectionPosition);

                _ShowReaderContent();
            }
        }

        private void _ChangeMargin(ChangeMarginMessage msg)
            => _SetMargin();

        private void _ChangeFontSize(ChangeFontSizeMessage msg)
            => _SetFontSize();

        private void _GoToPageHandler(GoToPageMessage msg)
            => _OpenPage(msg.Page, msg.Next, msg.Previous);

        private void _NavigationKeyHandler(NavigationKeyMessage msg)
        {
            switch (msg.Key)
            {
                case Key.Space:
                case Key.ArrowRight:
                case Key.ArrowDown:
                    _OpenPage(0, true, false);
                    break;
                case Key.ArrowLeft:
                case Key.ArrowUp:
                    _OpenPage(0, false, true);
                    break;
            }
        }

        private void _OpenChapter(int sectionPosition = 0, int inSectionPosition = 0)
        {
            _currentChapter = sectionPosition;
            _bookshelfBook.Section = sectionPosition;

            int targetPage = _pages?
                .Where(p => p.SectionId == _ebook.Sections.FirstOrDefault(s => s.Position == sectionPosition).Id 
                    && p.StartPosition <= inSectionPosition)?
                .LastOrDefault()?.Number ?? 0;

            _OpenPage(targetPage, sectionPosition: sectionPosition);
        }

        private void _SaveProgress()
        {
            if (_bookshelfBook == null) 
                return;

            _bookshelfService.SaveBook(_bookshelfBook);
        }

        private void _NavigationPanel_OnChapterChange(object sender, Navigation e)
        {
            if (e.Id != null)
            {
                if (_ebook.Sections.FirstOrDefault(s => s.Position == e.Position) != null)
                {
                    _OpenChapter(e.Position);

                    _ShowReaderContent();
                }
                else
                    throw new Exception("Bug during navigation via chapters!");
            }
        }

        private void _OnOpenReaderMenuRequest(OpenReaderMenuMessage msg)
        {
            ReaderContent.IsVisible = false;

            MenuPanel.ShowPanel();
        }

        private void _ShowReaderContent()
            => ReaderContent.IsVisible = true;

        private void _SetFontSize()
        {
            _fontSize = UserSettings.Reader.FontSize;

            if (_ebook != null)
            {
                _PreparePages(_ebook);
                _RefreshPage();
            }
        }

        private void _SetMargin()
        {
            ReaderContent.Margin = new Thickness(UserSettings.Reader.Margin,
                _marginTopConstant,
                UserSettings.Reader.Margin,
                UserSettings.Reader.Margin);

            // In ideal world this should be inplemented also...
            //_PreparePages(_ebook);
            //_RefreshPage();
        }

        private void _OpenPage(int page, bool next = false, bool previous = false, int sectionPosition = -1)
        {
            if (page == 0 && next)
                _currentPage++;
            else if (page == 0 && previous && _currentPage > 0)
                _currentPage--;
            else
                _currentPage = page;

            _RefreshPage();

            var nextPage = _pages.FirstOrDefault(p => p.Number == _currentPage);

            if (nextPage != null)
            {
                _RefreshPage();

                _bookshelfBook.FinishedReading = null;

                // handle change in HeaderPanel for pages count.

                _bookshelfBook.Section = sectionPosition == -1
                    ? _ebook.Sections.FirstOrDefault(s => s.Id == nextPage.SectionId).Position
                    : sectionPosition;
                _bookshelfBook.SectionPosition = nextPage.StartPosition;
                _messageBus.Send(new PageChangeMessage { CurrentPage = _currentPage + 1, TotalPages = _pages.Count, Position = nextPage.StartPosition });
            }
            else
            {
                _bookshelfBook.FinishedReading = DateTime.UtcNow;

                IocManager.Container.Resolve<IToastService>().Show("You have finished reading this book!");
            }

            _bookshelfService.SaveBook(_bookshelfBook);
        }

        private void _PreparePages(Ebook ebook)
        {
            _pages = new List<BookPage>();
            int pageNumber = 0;
            int startPostition = 0;

            foreach (var section in ebook.Sections)
            {
                int pageContentCount = 0;

                BookPage result = new BookPage()
                {
                    SectionId = section.Id,
                    Number = pageNumber,
                    StartPosition = startPostition,
                    Content = new List<Model.DataStructure.Element>()
                };

                foreach (var element in section.Elements)
                {
                    int availableLineSpace = _charactersInOneLine - pageContentCount % _charactersInOneLine;

                    // if element is a new line - ignore end of current line due to line brake.
                    if (element.Type == ElementType.NewLine)
                    {
                        pageContentCount += availableLineSpace;

                        element.Value = new string('-', availableLineSpace);
                    }
                    else
                    {
                        int currentElementLength = element.Value.Length;

                        // handle if target element bigger than available in line space.
                        if (currentElementLength > availableLineSpace)
                            pageContentCount += availableLineSpace + currentElementLength;
                        else
                            pageContentCount += currentElementLength;
                    }

                    // check if page already full and target element can`t be added to it.
                    if (pageContentCount > _charactersPerPage)
                    {
                        _pages.Add(result);

                        pageNumber++;
                        pageContentCount = 0;

                        // prepare a "new" page.

                        result = new BookPage()
                        {
                            SectionId = section.Id,
                            Number = pageNumber,
                            StartPosition = element.Position.SectionPosition,
                            Content = new List<Model.DataStructure.Element>()
                        };
                    }

                    result.Content.Add(element);
                }

                // add page with some content.
                if (result.Content.Any())
                {
                    _pages.Add(result);

                    pageNumber++;
                }
            }
        }

        private void _RefreshPage()
        {
            // refresh current page.
            //Device.BeginInvokeOnMainThread(() =>
            //{
            //    _SetItems(_pages.FirstOrDefault(p => p.Number == _currentPage).Content);
            //});

            Device.BeginInvokeOnMainThread(() =>
            {
                _SetItemsTestMode();
            });

            _bookshelfService.SaveBook(_bookshelfBook);
        }

        private void _SetItems(List<Model.DataStructure.Element> items)
        {
            PageContent.Children.Clear();

            foreach (Model.DataStructure.Element item in items)
            {
                Label label = new Label
                {
                    Text = item.Value,
                    FontSize = _fontSize,
                    TextColor = _textColor,
                    LineBreakMode = LineBreakMode.TailTruncation
                };

                if (item.Type == ElementType.NewLine)
                    label.IsVisible = false;
                
                PageContent.Children.Add(label);
            }
        }

        private void _SetItemsTestMode()
        {
            PageContent.Children.Clear();
            List<char> items = "aaaaaaaaa1aaaaaaaaa2aaaaaaaaa3aaaaaaaaa4aaaaaaaaa5aaaaaaaaa6aaaaaaaaa7aaaaaaaaa1aaaaaaaaa2aaaaaaaaa3aaaaaaaaa4aaaaaaaaa5aaaaaaaaa6aaaaaaaaa7aaaaaaaaa1aaaaaaaaa2aaaaaaaaa3aaaaaaaaa4aaaaaaaaa5aaaaaaaaa6aaaaaaaaa7aaaaaaaaa1aaaaaaaaa2aaaaaaaaa3aaaaaaaaa4aaaaaaaaa5aaaaaaaaa6aaaaaaaaa7aaaaaaaaa1aaaaaaaaa2aaaaaaaaa3aaaaaaaaa4aaaaaaaaa5aaaaaaaaa6aaaaaaaaa7aaaaaaaaa1aaaaaaaaa2aaaaaaaaa3aaaaaaaaa4aaaaaaaaa5aaaaaaaaa6aaaaaaaaa7aaaaaaaaa1aaaaaaaaa2aaaaaaaaa3aaaaaaaaa4aaaaaaaaa5aaaaaaaaa6aaaaaaaaa7".ToCharArray().ToList();

            foreach (var item in items)
            {
                Label label = new Label
                {
                    Text = item + "",
                    FontSize = _fontSize
                };

                PageContent.Children.Add(label);
            }
        }
    }
}