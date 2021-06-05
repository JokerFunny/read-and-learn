using Read_and_learn.AppResources;
using Read_and_learn.Page.Settings;
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

            Children.Add(new ApplicationSettings() { Title = AppResource.ApplicationSettings_Title });
            Children.Add(new TranslationSettings());
            Children.Add(new ReaderSettings());
            Children.Add(new ControlSettings());
        }
    }
}