using Autofac;
using Read_and_learn.Model.Message;
using Read_and_learn.Model.View.Reader;
using Read_and_learn.PlatformRelatedServices;
using Read_and_learn.Service.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page.Reader.ReaderTabs
{
    /// <summary>
    /// Layout for reader settings.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : StackLayout
    {
        private IMessageBus _messageBus;

        /// <summary>
        /// Default ctor.
        /// </summary>
        public Settings()
        {
            // IOC
            _messageBus = IocManager.Container.Resolve<IMessageBus>();

            InitializeComponent();

            BindingContext = new ReaderMenuSettingsVM();

            if (Device.RuntimePlatform == Device.Android)
            {
                FontPicker.WidthRequest = 75;
                FontPicker.Title = "Font size";

                MarginPicker.WidthRequest = 75;
                MarginPicker.Title = "Margin";

                var brightnessProvider = IocManager.Container.Resolve<IBrightnessProvider>();
                Brightness.Value = brightnessProvider.Brightness * 100;
            }

            if (Device.RuntimePlatform == Device.UWP)
                BrightnessWrapper.IsVisible = false;

        }

        private void _Brightness_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
                _messageBus.Send(new ChangeBrightnessMessage
                {
                    Brightness = (float)e.NewValue / 100
                });
        }
    }
}