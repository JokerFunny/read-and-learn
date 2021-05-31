using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Read_and_learn.Droid.PlatformRelatedServices;
using Android.Content;
using Android.Views;
using Autofac;
using Read_and_learn.Service.Interface;
using Read_and_learn.Model.Message;
using System;
using Read_and_learn.PlatformRelatedServices;
using Plugin.Permissions;
using Read_and_learn.Page;

namespace Read_and_learn.Droid
{
    [Activity(Label = "Read_and_learn", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        BatteryBroadcastReceiver _batteryBroadcastReceiver;
        private bool _disposed = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            
            _SetUpIoc();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            Window.SetSoftInputMode(SoftInput.AdjustResize);

            _batteryBroadcastReceiver = new BatteryBroadcastReceiver();

            // add reciever for battary change.
            Application.Context.RegisterReceiver(_batteryBroadcastReceiver, new IntentFilter(Intent.ActionBatteryChanged));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (UserSettings.Control.VolumeButtons 
                && (keyCode == Keycode.VolumeDown || keyCode == Keycode.VolumeUp) 
                && App.IsCurrentPageOfTargetTypeType(typeof(ReaderPage)))
            {
                var messageBus = IocManager.Container.Resolve<IMessageBus>();
                messageBus.Send(new GoToPageMessage { Next = keyCode == Keycode.VolumeDown, Previous = keyCode == Keycode.VolumeUp });

                return true;
            }

            return base.OnKeyDown(keyCode, e);
        }

        public override void OnBackPressed()
            => IocManager.Container.Resolve<IMessageBus>().Send(new BackPressedMessage());

        protected override void OnStart()
        {
            base.OnStart();

            _SetUpSubscribers();
        }

        protected override void OnStop()
        {
            base.OnStop();

            IocManager.Container.Resolve<IMessageBus>().UnSubscribe("MainActivity");
        }

        /// <summary>
        /// On dispose unsubscribe <see cref="_batteryBroadcastReceiver"/>
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_batteryBroadcastReceiver != null)
                        Application.Context.UnregisterReceiver(_batteryBroadcastReceiver);
                }

                _disposed = true;
            }

            base.Dispose(disposing);
        }

        private void _SetUpIoc()
        {
            IocManager.ContainerBuilder.RegisterType<AndroidAssetsManager>().As<IAssetsManager>();
            IocManager.ContainerBuilder.RegisterType<BrightnessProvider>().As<IBrightnessProvider>();
            IocManager.ContainerBuilder.RegisterInstance(new BrightnessProvider
            { Brightness = Android.Provider.Settings.System.GetFloat(ContentResolver, Android.Provider.Settings.System.ScreenBrightness) / 255 })
                .As<IBrightnessProvider>();
            IocManager.ContainerBuilder.RegisterType<FileHelper>().As<IFileHelper>();
            IocManager.ContainerBuilder.RegisterType<ToastService>().As<IToastService>();
            IocManager.ContainerBuilder.RegisterType<VersionProvider>().As<IVersionProvider>();

            IocManager.Build();
        }

        private void _SetUpSubscribers()
        {
            var messageBus = IocManager.Container.Resolve<IMessageBus>();

            messageBus.Subscribe<CloseApplicationMessage>(_CloseApplication, new string[] { "MainActivity" });
            messageBus.Subscribe<ChangeBrightnessMessage>(_ChangeBrightness, new string[] { "MainActivity" });
            messageBus.Subscribe<FullscreenRequestMessage>(_ToggleFullscreen, new string[] { "MainActivity" });
        }

        private void _CloseApplication(CloseApplicationMessage msg)
            => Finish();

        private void _ChangeBrightness(ChangeBrightnessMessage msg)
        {
            RunOnUiThread(() => {
                var brightness = Math.Min(msg.Brightness, 1);
                brightness = Math.Max(brightness, 0);

                var attributesWindow = new WindowManagerLayoutParams();
                attributesWindow.CopyFrom(Window.Attributes);
                attributesWindow.ScreenBrightness = brightness;
                Window.Attributes = attributesWindow;
            });
        }

        private void _ToggleFullscreen(FullscreenRequestMessage msg)
        {
            if (msg.Fullscreen)
            {
                RunOnUiThread(() => {
                    Window.AddFlags(WindowManagerFlags.Fullscreen);
                });
            }
            else
            {
                RunOnUiThread(() => {
                    Window.ClearFlags(WindowManagerFlags.Fullscreen);
                });
            }
        }
    }
}