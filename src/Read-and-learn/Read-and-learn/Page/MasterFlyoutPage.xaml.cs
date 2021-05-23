using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page
{
    /// <summary>
    /// Custom <see cref="FlyoutPage"/>.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterFlyoutPage : FlyoutPage
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public MasterFlyoutPage()
        {
            InitializeComponent();

            MasterPage.ListView.ItemSelected += _ListView_ItemSelected;
        }

        private void _ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterFlyoutPageFlyoutMenuItem;
            if (item == null)
                return;

            if (!Detail.Navigation.NavigationStack.Any() ||
                Detail.Navigation.NavigationStack.Last().GetType() != item.TargetType)
            {
                var page = (Xamarin.Forms.Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;

                Detail.Navigation.PushAsync(page);

                if (item.TargetType == typeof(HomePage))
                {
                    foreach (var pageToRemove in Detail.Navigation.NavigationStack.Where(o => o != page).ToList())
                    {
                        Detail.Navigation.RemovePage(pageToRemove);
                    }
                }
            }

            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}