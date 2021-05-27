using Autofac;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Read_and_learn.Helpers;
using Read_and_learn.Model.Bookshelf;
using Read_and_learn.Model.Message;
using Read_and_learn.Page.Home;
using Read_and_learn.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        IBookshelfService _bookshelfService;
        IMessageBus _messageBus;

        public HomePage()
        {
            InitializeComponent();

            _bookshelfService = IocManager.Container.Resolve<IBookshelfService>();
            _messageBus = IocManager.Container.Resolve<IMessageBus>();

            _messageBus.Subscribe<AddBookMessage>(_AddBook);
            _messageBus.Subscribe<OpenBookMessage>(_OpenBook);
            _messageBus.Subscribe<DeleteBookMessage>(_DeleteBook);

            if (!App.HasMasterDetailPage)
            {
                var settingsItem = new ToolbarItem
                {
                    Text = "Settings",
                    IconImageSource = "settings.png"
                };
                settingsItem.Clicked += _Settings_Clicked;
                ToolbarItems.Add(settingsItem);

                var aboutItem = new ToolbarItem
                {
                    Text = "About",
                    IconImageSource = "info.png",
                };
                aboutItem.Clicked += _About_Clicked;
                ToolbarItems.Add(aboutItem);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // because of floating action button on android
            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 200), () => {
                _messageBus.Send(new FullscreenRequestMessage(false));
                return false;
            });

            _ShowAnalyticsAgreement();

            UserSettings.FirstRun = false;

            _LoadBookshelf();
        }

        private async void _About_Clicked(object sender, EventArgs e)
            => await Navigation.PushAsync(new AboutPage());

        private async void _Settings_Clicked(object sender, EventArgs e)
            => await Navigation.PushAsync(new SettingsPage());


        private async void _ShowAnalyticsAgreement()
        {
            if (UserSettings.FirstRun)
            {
                var result = await DisplayAlert("Agreement with collection of anonymous data", "I agree with collecting of anonymous information about using of the app. This is important for application improvements.", "I agree", "No");

                UserSettings.AnalyticsAgreement = result;
            }
        }

        private async void _LoadBookshelf()
        {
            Bookshelf.Children.Clear();

            var books = await _bookshelfService.LoadBooks();

            foreach (var book in books)
            {
                Bookshelf.Children.Add(new BookCard(book));
            }
        }

        private async void _AddBook(AddBookMessage msg)
        {
            var permissionStatus = await PermissionHelper.CheckAndRequestPermission(Plugin.Permissions.Abstractions.Permission.Storage);

            if (permissionStatus == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                var pickedFile = await FilePicker.PickAsync();

                if (pickedFile != null)
                {
                    try
                    {
                        var book = await _bookshelfService.AddBook(pickedFile);

                        if (book.Item2)
                            Bookshelf.Children.Add(new BookCard(book.Item1));

                        _SendBookToReader(book.Item1);
                    }
                    catch (Exception e)
                    {
                        var ext = string.Empty;
                        if (!string.IsNullOrEmpty(pickedFile.FileName))
                        {
                            ext = pickedFile.FileName.Split('.').LastOrDefault();
                        }

                        Analytics.TrackEvent("Failed to open book", new Dictionary<string, string> 
                        {
                            { "File name", ext }
                        });

                        Crashes.TrackError(e, new Dictionary<string, string> 
                        {
                            { "File name", pickedFile.FileName }
                        });

                        await DisplayAlert("Error", "File failed to open", "OK");
                    }

                }
            }
            else
            {
                await DisplayAlert("Permission not granted", "Cannot open book without storage permissions.", "OK");
            }

        }

        private void _OpenBook(OpenBookMessage msg)
            => _SendBookToReader(msg.Book);

        private async void _DeleteBook(DeleteBookMessage msg)
        {
            var deleteButton = "Delete";
            var confirm = await DisplayActionSheet("Delete book?", deleteButton, "No");
            if (confirm == deleteButton)
            {
                var card = Bookshelf.Children.FirstOrDefault(o => o.StyleId == msg.Book.Id);

                if (card != null)
                    Bookshelf.Children.Remove(card);

                var res = await _bookshelfService.RemoveById(msg.Book.Id);
            }
        }

        private async void _SendBookToReader(Book book)
        {
            var page = new ReaderPage();
            page.LoadBook(book);

            await Navigation.PushAsync(page);
        }

        private void _AddButton_Clicked(object sender, EventArgs e)
            => _messageBus.Send(new AddBookMessage());
    }
}