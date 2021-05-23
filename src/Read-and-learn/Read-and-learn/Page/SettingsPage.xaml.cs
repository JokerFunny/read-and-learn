using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page
{
    /// <summary>
    /// Page that contain all settings across the app.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : TabbedPage
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public SettingsPage()
        {
            InitializeComponent();

            if (!App.HasMasterDetailPage)
                NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}