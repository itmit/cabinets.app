using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyBookingPage : ContentPage
    {
        public MyBookingPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            EditContentView.IsVisible = true;
            FirstContentView.IsVisible = false;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Application.Current.MainPage.DisplayActionSheet("Вы действительно хотите отменить запись?", "ОТМЕНИТЬ","НЕТ");
        }
    }
}