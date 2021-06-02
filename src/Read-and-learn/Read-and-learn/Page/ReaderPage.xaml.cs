using Autofac;
using Read_and_learn.Model;
using Read_and_learn.Model.Bookshelf;
using Read_and_learn.Model.DataStructure;
using Read_and_learn.Model.Message;
using Read_and_learn.PlatformRelatedServices;
using Read_and_learn.Service.Interface;
using System;
using System.Collections.Generic;
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
        private IToastService _toastService;

        private const int _marginTopConstant = 40;
        // rework to be more flexible.
        private int _charactersPerPage;
        private int _charactersInOneLine = Device.RuntimePlatform == Device.Android ? 30 : 40;
        private int _linesCount = Device.RuntimePlatform == Device.Android ? 25 : 27;

        private bool _labelDoubleCliked = false;
        private Label _firsClickedLabel = null;
        private Color _backGroundColor = Color.White;
        private Color _textColor = Color.Black;
        private Color _singleClickColor = Color.Green;
        private Color _doubleClickColor = Color.Orange;

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
            _toastService = IocManager.Container.Resolve<IToastService>();

            MenuPanel.NavigationPanel.OnChapterChange += _NavigationPanel_OnChapterChange;

            _messageBus.Send(new FullscreenRequestMessage(true));

            _charactersPerPage = _linesCount * _charactersInOneLine;
            _SetFontSize();
            _SetMargin();

            if (UserSettings.Reader.NightMode)
            {
                BackgroundColor = Color.FromRgb(24, 24, 25);

                _backGroundColor = BackgroundColor;
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

        #region Subscribers

        private void _ChangeMargin(ChangeMarginMessage msg)
            => _SetMargin();

        private void _ChangeFontSize(ChangeFontSizeMessage msg)
            => _SetFontSize();

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

        private void _OnOpenReaderMenuRequest(OpenReaderMenuMessage msg)
        {
            // [TODO]: check if this really needed...
            _labelDoubleCliked = false;
            _firsClickedLabel = null;

            ReaderContent.IsVisible = false;

            MenuPanel.ShowPanel();
        }

        private void _ShowReaderContent()
            => ReaderContent.IsVisible = true;

        #endregion

        #region ReaderMainFunctions

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

        private async void _RefreshBookmarks()
        {
            var bookmarks = await _bookmarkService.LoadBookmarksByBookId(_bookshelfBook.Id);

            MenuPanel.BookmarksPanel.UpdateBookmarks(bookmarks);
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

        private void _SetFontSize()
        {
            _fontSize = UserSettings.Reader.FontSize;

            if (_ebook != null)
            {
                // [TODO]: calculate a proper count of chars on page...
                //_PreparePages(_ebook);
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
            _labelDoubleCliked = false;
            _firsClickedLabel = null;

            if (page == 0 && next)
                _currentPage++;
            else if (page == 0 && previous && _currentPage > 0)
                _currentPage--;
            else
                _currentPage = page;

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

                _toastService.Show("You have finished reading this book!");
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
                    }
                    else
                    {
                        int currentElementLength = element.Value.Length;

                        // change value for WhiteSpace element due to feature with label component...
                        if (element.Type == ElementType.WhiteSpace)
                            element.Value = new string('-', currentElementLength);

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
            Device.BeginInvokeOnMainThread(() =>
            {
                _SetItems(_pages.FirstOrDefault(p => p.Number == _currentPage).Content);
            });

            //Device.BeginInvokeOnMainThread(() =>
            //{
            //    _SetItemsTestMode();
            //});

            _bookshelfService.SaveBook(_bookshelfBook);
        }

        private void _SetItems(List<Model.DataStructure.Element> items)
        {
            PageContentLayout.Children.Clear();

            FlexLayout flexLayout = new FlexLayout()
            {
                Direction = FlexDirection.Row,
                Wrap = FlexWrap.Wrap,
                JustifyContent = FlexJustify.Start,
                AlignItems = FlexAlignItems.Start,
                AlignContent = FlexAlignContent.Start
            };

            foreach (Model.DataStructure.Element item in items)
            {
                // in case of new line - create new container to handle text.
                if (item.Type == ElementType.NewLine)
                {
                    PageContentLayout.Children.Add(flexLayout);

                    flexLayout = new FlexLayout()
                    {
                        Direction = FlexDirection.Row,
                        Wrap = FlexWrap.Wrap,
                        JustifyContent = FlexJustify.Start,
                        AlignItems = FlexAlignItems.Start,
                        AlignContent = FlexAlignContent.Start
                    };
                }

                Label label = new Label
                {
                    Text = item.Value,
                    FontSize = _fontSize,
                    TextColor = _textColor
                };

                if (item.Type == ElementType.WhiteSpace)
                {
                    label.TextColor = _backGroundColor;
                }
                else if (item.Type == ElementType.Text)
                {
                    var singleTapGestureRecognizer = new TapGestureRecognizer();
                    singleTapGestureRecognizer.Tapped += (o, e) => _OnTextPressed(o, e);

                    var doubleTapGestureRecognizer = new TapGestureRecognizer();
                    doubleTapGestureRecognizer.NumberOfTapsRequired = 2;
                    doubleTapGestureRecognizer.Tapped += (o, e) => _OnTextDoublePressed(o, e);

                    label.GestureRecognizers.Add(singleTapGestureRecognizer);
                    label.GestureRecognizers.Add(doubleTapGestureRecognizer);
                }

                flexLayout.Children.Add(label);
            }

            PageContentLayout.Children.Add(flexLayout);
        }

        private void _SetItemsTestMode()
        {
            //PageContent.Children.Clear();

            PageContentLayout.Children.Clear();

            for (int i = 0; i < 2; i++)
            {
                FlexLayout flexLayout = new FlexLayout()
                {
                    Direction = FlexDirection.Row,
                    Wrap = FlexWrap.Wrap,
                    JustifyContent = FlexJustify.Start,
                    AlignItems = FlexAlignItems.Start,
                    AlignContent = FlexAlignContent.Start
                };

                List<char> items = "aaaaaaaaa1aaaaaaaaa2aaaaaaaaa3aaaaaaaaa4aaaaaaaaa5aaaaaaaaa6aaaaaaaaa7aaaaaaaaa1aaaaaaaaa2aaaaaaaaa3aaaaaaaaa4aaaaaaaaa5aaaaaaaaa6aaaaaaaaa7aaaaaaaaa1aaaaaaaaa2aaaaaaaaa3aaaaaaaaa4aaaaaaaaa5aaaaaaaaa6aaaaaaaaa7aaaaaaaaa1aaaaaaaaa2aaaaaaaaa3aaaaaaaaa4aaaaaaaaa5aaaaaaaaa6aaaaaaaaa7aaaaaaaaa1aaaaaaaaa2aaaaaaaaa3aaaaaaaaa4aaaaaaaaa5aaaaaaaaa6aaaaaaaaa7aaaaaaaaa1aaaaaaaaa2aaaaaaaaa3aaaaaaaaa4aaaaaaaaa5aaaaaaaaa6aaaaaaaaa7aaaaaaaaa1aaaaaaaaa2aaaaaaaaa3aaaaaaaaa4aaaaaaaaa5aaaaaaaaa6aaaaaaaaa7".ToCharArray().ToList();

                foreach (var item in items)
                {
                    Label label = new Label
                    {
                        Text = item + "",
                        FontSize = _fontSize
                    };

                    flexLayout.Children.Add(label);
                }

                PageContentLayout.Children.Add(flexLayout);
            }
        }

        private async void _ChangeBackgroundColor(Label targetObj)
        {

        }

        #endregion

        #region Translation

        private void _OnTextPressed(object sender, EventArgs e)
        {
            // remove double clicked from label.
            if (_labelDoubleCliked)
                return;

            Label targetObj = (Label)sender;

            targetObj.BackgroundColor = _singleClickColor;

            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 500), () => {
                if (_labelDoubleCliked)
                {
                    if (targetObj.BackgroundColor == _singleClickColor)
                        targetObj.BackgroundColor = _backGroundColor;

                    return false;
                }

                var translation = _translateService.TranslateWord(targetObj.Text, _ebook.Language).Result;

                string result = null;
                if (translation.Error != null)
                    result = "Error during translation. Please contact the developer.";
                else
                    result = $"Provider: {translation.Provider}\n\rResult: {translation.Result}{(translation.Synonyms.Any() ? $"\n\rSynonyms: {string.Join(", ", translation.Synonyms)}" : "")}";

                _toastService.Show(result);

                Device.StartTimer(new TimeSpan(0, 0, 2), () =>
                {
                    targetObj.BackgroundColor = _backGroundColor;
                    return false;
                });

                return false;
            });
        }

        private void _OnTextDoublePressed(object sender, EventArgs e)
        {
            Label targetObj = (Label)sender;

            if (!_labelDoubleCliked)
            {
                _labelDoubleCliked = true;

                _firsClickedLabel = targetObj;
                targetObj.BackgroundColor = Color.Orange;
            }
            else
            {
                var current = Connectivity.NetworkAccess;

                // if exist - try to translate current part
                if (current == NetworkAccess.Internet)
                {
                    _labelDoubleCliked = false;

                    var startPos = PageContentLayout.Children.IndexOf(_firsClickedLabel);
                    var endPos = PageContentLayout.Children.IndexOf(targetObj);

                    if (endPos < startPos)
                    {
                        int swap = endPos;
                        endPos = startPos;
                        startPos = swap;
                    }

                    // add backgroun color for all elements that should be translated.
                    string targetPart = null;
                    for (; startPos <= endPos; startPos++)
                    {
                        Label part = (Label)PageContentLayout.Children[startPos];
                        part.BackgroundColor = Color.Orange;

                        targetPart += part.Text;
                    }

                    var translation = _translateService.TranslatePart(targetPart, _ebook.Language).Result;

                    string result = $"{(translation.Error != null ? "Error during translation. Please contact the developer." : "Provider: {translation.Provider}\n\rResult: {translation.Result}.")}";

                    DisplayAlert("Translation result:", result, "OK");

                    //Device.StartTimer(new TimeSpan(0, 0, 2), () => {
                    //    //change color for all object to default.
                    //
                    //    _labelDoubleCliked = false;
                    //    _firsClickedLabel = null;
                    //
                    //    return false;
                    //});
                }
                else
                {
                    _toastService.Show("You can`t translate part of text without Internet connection.");
                    _firsClickedLabel.BackgroundColor = _backGroundColor;

                    _labelDoubleCliked = false;
                    _firsClickedLabel = null;
                }
            }
        }

        #endregion
    }
}