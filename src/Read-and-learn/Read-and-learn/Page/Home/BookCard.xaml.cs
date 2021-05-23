using Autofac;
using Read_and_learn.Model.Bookshelf;
using System;
using PCLStorage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Read_and_learn.Service.Interface;
using Read_and_learn.Model.Message;
using System.IO;

namespace Read_and_learn.Page.Home
{
    /// <summary>
    /// Layout for book cards.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookCard : StackLayout
    {
        private Book _book;

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="book"></param>
        public BookCard(Book book)
        {
            _book = book;

            StyleId = book.Id;

            BindingContext = new
            {
                book.Title,
                Width = BookCardModel.CardWidth,
                Height = BookCardModel.CardHeight,
                PlaceholderWidth = BookCardModel.CardWidth * .75,
                IsFinished = book.FinishedReading.HasValue
            };

            InitializeComponent();

            // add correct cover if it exist.
            _LoadImage();

            DeleteIcon.GestureRecognizers.Add(
                new TapGestureRecognizer
                {
                    Command = new Command(() => _Delete())
                }
            );

            GestureRecognizers.Add(
                new TapGestureRecognizer
                {
                    Command = new Command(() => _Open()),
                });
        }

        private void _LoadImage()
        {
            if (!string.IsNullOrEmpty(_book.Cover))
            {
                Cover.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(_book.Cover)));
                Cover.Aspect = Aspect.Fill;
                Cover.WidthRequest = BookCardModel.CardWidth;
                Cover.HeightRequest = BookCardModel.CardHeight;
            }
        }

        private void _Open()
        {
            var messageBus = IocManager.Container.Resolve<IMessageBus>();
            messageBus.Send(new OpenBookMessage { Book = _book });
        }

        private void _Delete()
        {
            var messageBus = IocManager.Container.Resolve<IMessageBus>();
            messageBus.Send(new DeleteBookMessage { Book = _book });
        }
    }
}