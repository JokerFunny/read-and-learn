using Read_and_learn.Model.View.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page.Settings
{
    /// <summary>
    /// Page for customize settings related to the translation.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TranslationSettings : ContentPage
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public TranslationSettings()
        {
            InitializeComponent();


            if (Device.RuntimePlatform == Device.UWP)
            {
                Content.HorizontalOptions = LayoutOptions.Start;
                Content.WidthRequest = 500;
            }

            BindingContext = new TransltaionSettingsVM();
        }
    }
}