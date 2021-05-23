using Read_and_learn.Model.View.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page.Settings
{
    /// <summary>
    /// Page for customize settings related to the reader.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReaderSettings : ContentPage
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public ReaderSettings()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.UWP)
            {
                Content.HorizontalOptions = LayoutOptions.Start;
                Content.WidthRequest = 500;
            }

            BindingContext = new ReaderSettingsVM();
        }
    }
}