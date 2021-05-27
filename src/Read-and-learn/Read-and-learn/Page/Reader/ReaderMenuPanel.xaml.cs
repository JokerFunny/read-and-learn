using Autofac;
using Read_and_learn.Model.DataStructure;
using Read_and_learn.Model.Message;
using Read_and_learn.Model.View.Reader;
using Read_and_learn.Service.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page.Reader
{
    /// <summary>
    /// Panel for navigation and inreader settings.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReaderMenuPanel : StackLayout
    {
        IMessageBus _messageBus;

        /// <summary>
        /// Default ctor.
        /// </summary>
        public ReaderMenuPanel()
        {
            _messageBus = IocManager.Container.Resolve<IMessageBus>();

            InitializeComponent();

            _messageBus.Subscribe<CloseReaderMenuMessage>((msg) => HidePanel());

            BindingContext = new ReaderMenuVM();
        }

        /// <summary>
        /// Show current panel.
        /// </summary>
        public void ShowPanel()
        {
            Device.BeginInvokeOnMainThread(() => {
                IsVisible = true;
            });
        }

        /// <summary>
        /// Hide current panel.
        /// </summary>
        public void HidePanel()
        {
            Device.BeginInvokeOnMainThread(() => {
                IsVisible = false;
            });
        }

        private void _NavigationPanel_OnChapterChange(object sender, Navigation e)
            => HidePanel();
    }
}