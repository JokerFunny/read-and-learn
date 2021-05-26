using Read_and_learn.Model.Bookshelf;
using Read_and_learn.Model.View.Reader;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page.Reader.ReaderTabs.BookmarkTab
{
    /// <summary>
    /// Tab to handle work with bookmarks.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookmarkItem : StackLayout
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="bookmark">Target <see cref="Bookmark"/></param>
        public BookmarkItem(Bookmark bookmark)
        {
            InitializeComponent();

            BindingContext = new BookmarkVM(bookmark);
        }
    }
}