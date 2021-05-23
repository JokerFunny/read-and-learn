using Read_and_learn.Model.View.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page.Settings
{
    /// <summary>
    /// Page to customize application settings.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApplicationSettings : ContentPage
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public ApplicationSettings()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.UWP)
            {
                Content.HorizontalOptions = LayoutOptions.Start;
                Content.WidthRequest = 500;
            }

            BindingContext = new ApplicationSettingsVM();
        }
    }
}