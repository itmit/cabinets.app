using System;
using cabinets.Core.ViewModels.Cabinets;
using cabinets.Core.ViewModels.Profile;
using cabinets.Pages;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Cabinets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CabinetsDetailPage : MvxContentPage<CabinetDetailViewModel>
    {
        public CabinetsDetailPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BookingPage());
        }
    }
}