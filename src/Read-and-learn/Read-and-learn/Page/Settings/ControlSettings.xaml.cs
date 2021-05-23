using Read_and_learn.Model.View.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page.Settings
{
    /// <summary>
    /// Page for customize settings related to the control.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlSettings : ContentPage
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public ControlSettings()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.UWP)
            {
                Content.HorizontalOptions = LayoutOptions.Start;
                Content.WidthRequest = 500;
            }

            BindingContext = new ControlSettingsVM();
        }
    }
}