using Autofac;
using Read_and_learn.Model.Message;
using Read_and_learn.Service.Interface;
using Windows.UI.Xaml;

namespace Read_and_learn.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(new Read_and_learn.App());

            Window.Current.CoreWindow.KeyDown += _CoreWindow_KeyDown;
        }

        private void _CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
            => IocManager.Container.Resolve<IMessageBus>().Send(NavigationKeyMessage.FromKeyCode((int)args.VirtualKey));
    }
}
