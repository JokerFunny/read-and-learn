using Autofac;
using Read_and_learn.Model;
using Read_and_learn.Model.Bookshelf;
using Read_and_learn.Model.DataStructure;
using Read_and_learn.Model.Message;
using Read_and_learn.Provider;
using Read_and_learn.Service.Interface;
using System;
using System.Linq;
using Xamarin.Essentials;
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

        private int _currentChapter;
        private Book _bookshelfBook;
        private Ebook _ebook;
        private bool _resizeFirstRun = true;
        private bool _resizeTimerRunning = false;
        private int? _resizeTimerWidth;
        private int? _resizeTimerHeight;
        private Position _lastSavedPosition = null;
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

            if (UserSettings.Reader.NightMode)
                BackgroundColor = Color.FromRgb(24, 24, 25);

            NavigationPage.SetHasNavigationBar(this, false);
        }

        public bool IsMenuPanelVisible()
            => MenuPanel.IsVisible;

        public async void LoadBook(Book book)
        {
            _bookshelfBook = book;
            _ebook = await _bookService.OpenBook(new FileResult(book.Path));
            var position = _bookshelfBook.Position;

            MenuPanel.NavigationPanel.SetNavigation(_ebook.Navigation);
            _RefreshBookmarks();

            Section chapter = null;
            int positionInChapter = -1;
            if (position != null)
            {
                var loadedChapter = _ebook.Sections.ElementAt(position.Section);

                if (loadedChapter != null)
                {
                    chapter = loadedChapter;
                    positionInChapter = position.SectionPosition;
                }
            }
            else
            {
                chapter = _ebook.Sections.First();
                positionInChapter = 0;
            }

            _OpenChapter(chapter, position: positionInChapter);
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
            _messageBus.Subscribe<OpenReaderMenuMessage>(_OnOpenQuickPanelRequest, new string[] { nameof(ReaderPage) });
        }

        private void _UnSubscribeMessages()
            => _messageBus.UnSubscribe(nameof(ReaderPage));

        /// <remarks>
        ///     DEBUG THIS PIECE.
        /// </remarks>
        private void _OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (UserSettings.Control.BrightnessChange == BrightnessChange.None)
                return;

            switch (e.StatusType)
            {
                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    var totalWidth = (int)ReaderContent.Width;
                    var edge = totalWidth / 5;

                    if ((UserSettings.Control.BrightnessChange == BrightnessChange.Left && e.TotalX <= edge) ||
                        (UserSettings.Control.BrightnessChange == BrightnessChange.Right && e.TotalX >= totalWidth - edge))
                    {
                        float brightness = 1 - ((float)e.TotalY / ((int)ReaderContent.Height + (2 * UserSettings.Reader.Margin)));

                        _messageBus.Send(new ChangeBrightnessMessage { Brightness = brightness });
                    }

                    break;
            }
        }

        private void _OnSwiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    // Handle the swipe
                    break;
                case SwipeDirection.Right:
                    // Handle the swipe
                    break;
            }
        }

        private void _ApplicationSleepSubscriber(ApplicationSleepMessage msg)
            => _SaveProgress();

        private void _AddBookmark(AddBookmarkMessage msg)
        {
            _bookmarkService.CreateBookmark(DateTimeOffset.Now.ToString(), _bookshelfBook.Id, _bookshelfBook.Position);

            _RefreshBookmarks();
        }

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
            var loadedChapter = _ebook.Sections.ElementAt(msg.Bookmark.Position.Section);
            if (loadedChapter != null)
            {
                if (_currentChapter != msg.Bookmark.Position.Section)
                {
                    _OpenChapter(loadedChapter, position: msg.Bookmark.Position.SectionPosition);
                }
                else
                {
                    _bookshelfBook.SectionPosition = msg.Bookmark.Position.SectionPosition;

                    _GoToPosition(msg.Bookmark.Position.SectionPosition);
                }
            }
        }

        private void _ChangeMargin(ChangeMarginMessage msg)
            => _SetMargin(msg.Margin);

        private void _ChangeFontSize(ChangeFontSizeMessage msg)
            => _SetFontSize(msg.FontSize);


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

        private async void _OpenChapter(Section chapter, int position = 0, bool lastPage = false, string marker = "")
        {
            _currentChapter = _ebook.Sections.IndexOf(chapter);
            _bookshelfBook.Section = _currentChapter;

            /////// SOME DARK MAGIC TI GET CONTENT FROM SPINE TO APPROPRIATE FORMAT
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
                var path = e.Id.Split('#');
                var id = path.First();
                var marker = path.Skip(1).FirstOrDefault() ?? string.Empty;

                var section = _ebook.Sections.FirstOrDefault(s => s.Position == e.Position);
                if (section != null)
                    _OpenChapter(section, marker: marker);
            }
        }

        private void _OnOpenQuickPanelRequest(OpenReaderMenuMessage msg)
            => MenuPanel.ShowPanel();
    }
}