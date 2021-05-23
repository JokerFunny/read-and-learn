using Read_and_learn.Model.Bookshelf;
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
        /// <summary>
        /// Default ctor.
        /// </summary>
        public ReaderPage()
        {
            InitializeComponent();
        }

        public bool IsQuickPanelVisible()
        {
            return false;
        }

        public void LoadBook(Book book)
        {

        }
    }
}