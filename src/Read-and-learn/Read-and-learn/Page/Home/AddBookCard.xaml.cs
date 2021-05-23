using Autofac;
using Read_and_learn.Model.Message;
using Read_and_learn.Service.Interface;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page.Home
{
    /// <summary>
    /// Layout for adding book card for Windows device.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBookCard : StackLayout
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public AddBookCard()
        {
            BindingContext = new
            {
                Width = BookCardModel.CardWidth,
            };

            InitializeComponent();
        }

        private void _AddButton_Clicked(object sender, EventArgs e)
        {
            var messageBus = IocManager.Container.Resolve<IMessageBus>();
            messageBus.Send(new AddBookMessage());
        }
    }
}