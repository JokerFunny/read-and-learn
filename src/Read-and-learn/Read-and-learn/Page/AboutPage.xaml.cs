using Read_and_learn.Model.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Read_and_learn.Page
{
    /// <summary>
    /// Page to show info about application and developer.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            BindingContext = new AboutVM();

            if (!App.HasMasterDetailPage)
                NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}