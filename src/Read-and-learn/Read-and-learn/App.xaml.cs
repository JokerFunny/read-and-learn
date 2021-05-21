using Autofac;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using PCLAppConfig;
using Read_and_learn.Model.Message;
using Read_and_learn.Page;
using Read_and_learn.PlatformRelatedServices;
using Read_and_learn.Service.Interface;
using System;
using System.Reflection;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;

namespace Read_and_learn
{
    public partial class App : Application
    {
        private IMessageBus _messageBus;
        private bool _exitPressedOnce = false;

        /// <summary>
        /// Check if current platform has MasterDetailPage.
        /// </summary>
        public static bool HasMasterDetailPage
            => Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS;

        public App()
        {
            InitializeComponent();

            _LoadConfig();

            _messageBus = IocManager.Container.Resolve<IMessageBus>();

            MainPage = new MainPage();

            if (HasMasterDetailPage)
                MainPage = new FlyoutPage();
            else
                MainPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart()
        {
            AppCenter.Start($"ios={AppSettings.AppCenter.Apple};android={AppSettings.AppCenter.Android};uwp={AppSettings.AppCenter.UWP};", typeof(Analytics), typeof(Crashes));
            Analytics.SetEnabledAsync(UserSettings.AnalyticsAgreement);

            _messageBus.UnSubscribe("App");
            _messageBus.Subscribe<BackPressedMessage>(_BackPressedMessageSubscriber, new string[] { "App" });
        }

        protected override void OnSleep()
        {
            _messageBus.Send(new ApplicationSleepMessage());
        }

        protected override void OnResume()
        { }

        private void _LoadConfig()
        {
            if (ConfigurationManager.AppSettings == null)
            {
                var assembly = typeof(App).GetTypeInfo().Assembly;

                ConfigurationManager.Initialise(assembly.GetManifestResourceStream("Read_and_learn.app.config"));
            }
        }

        private async void _BackPressedMessageSubscriber(BackPressedMessage msg)
        {
            var master = MainPage as MasterDetailPage1;

            if (master != null)
            {
                var detailPage = master.Detail.Navigation.NavigationStack.LastOrDefault();

                /*if (detailPage is ReaderPage readerPage && readerPage.IsQuickPanelVisible())
                {
                    _messageBus.Send(new CloseQuickPanelMessage());
                }
                else */if (detailPage is HomePage)
                {
                    if (_exitPressedOnce)
                    {
                        _messageBus.Send(new CloseApplicationMessage());
                    }
                    else
                    {
                        IocManager.Container.Resolve<IToastService>().Show("Press once again to exit!");
                        _exitPressedOnce = true;
                        Device.StartTimer(new TimeSpan(0, 0, 2), () => {
                            _exitPressedOnce = false;
                            return false;
                        });
                    }
                }
                else
                {
                    await master.Detail.Navigation.PopAsync();
                }
            }
        }
    }
}
