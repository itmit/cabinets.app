using cabinets.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cabinets.Core.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TwoButtonPage : ContentPage
    {
        public TwoButtonPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
           // Navigation.PushAsync(new MainTabbedView());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
          //  Navigation.PushAsync(new MainTabbedView());
        }
    }
}