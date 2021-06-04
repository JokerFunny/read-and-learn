using Autofac;
using Read_and_learn.Model.Message;
using Read_and_learn.Model.View.Reader;
using Read_and_learn.Service.Interface;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page.Reader
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderPanel : StackLayout
    {
        IMessageBus _messageBus;

        public HeaderPanel()
        {
            _messageBus = IocManager.Container.Resolve<IMessageBus>();

            InitializeComponent();

            BindingContext = new HeaderPanelVM();
        }

        private void _OpenReaderMenu_Clicked(object sender, EventArgs e)
            => _messageBus.Send(new OpenReaderMenuMessage());
    }
}