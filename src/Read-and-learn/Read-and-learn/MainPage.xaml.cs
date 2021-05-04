using System;
using Xamarin.Forms;

namespace Read_and_learn
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            button1.Text = "Нажато!!!";
        }
    }
}
