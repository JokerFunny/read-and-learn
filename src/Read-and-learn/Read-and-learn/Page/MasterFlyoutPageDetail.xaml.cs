using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page
{
    /// <summary>
    /// Details part for <see cref="MasterFlyoutPage"/>
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterFlyoutPageDetail : ContentPage
    {
        /// <summary>
        /// Elemens of target page.
        /// </summary>
        public ListView ListView;

        /// <summary>
        /// Default ctor.
        /// </summary>
        public MasterFlyoutPageDetail()
        {
            InitializeComponent();

            BindingContext = new MasterFlyoutPageDetailViewModel();
            ListView = MenuItemsListView;
        }

        class MasterFlyoutPageDetailViewModel : INotifyPropertyChanged
        {
            /// <summary>
            /// Observable collection of <see cref="MasterFlyoutPageFlyoutMenuItem"/>.
            /// </summary>
            public ObservableCollection<MasterFlyoutPageFlyoutMenuItem> MenuItems { get; set; }

            /// <summary>
            /// Default ctor.
            /// </summary>
            public MasterFlyoutPageDetailViewModel()
            {
                MenuItems = new ObservableCollection<MasterFlyoutPageFlyoutMenuItem>(new[]
                {
                    new MasterFlyoutPageFlyoutMenuItem { Id = 0, Title = "My Books", TargetType = typeof(HomePage) },
                    new MasterFlyoutPageFlyoutMenuItem { Id = 1, Title = "Settings", TargetType = typeof(SettingsPage) },
                    new MasterFlyoutPageFlyoutMenuItem { Id = 2, Title = "About", TargetType = typeof(AboutPage) },
                });
            }

            /// <summary>
            /// Event for handle property change.
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            void _OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}