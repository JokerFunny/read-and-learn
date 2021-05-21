using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterFlyoutPageDetail : ContentPage
    {
        public ListView ListView;

        public MasterFlyoutPageDetail()
        {
            InitializeComponent();

            BindingContext = new MasterFlyoutPageDetailViewModel();
            ListView = MenuItemsListView;
        }

        class MasterFlyoutPageDetailViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterFlyoutPageFlyoutMenuItem> MenuItems { get; set; }

            public MasterFlyoutPageDetailViewModel()
            {
                MenuItems = new ObservableCollection<MasterFlyoutPageFlyoutMenuItem>(new[]
                {
                    new MasterFlyoutPageFlyoutMenuItem { Id = 0, Title = "My Books", TargetType = typeof(HomePage) },
                    new MasterFlyoutPageFlyoutMenuItem { Id = 1, Title = "Settings", TargetType = typeof(SettingsPage) },
                    new MasterFlyoutPageFlyoutMenuItem { Id = 2, Title = "About", TargetType = typeof(AboutPage) },
                });
            }

            public event PropertyChangedEventHandler PropertyChanged;

            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}