using System;
using cabinets.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(NoHistory = true, Animated = false)]
    public partial class CalendarPage : MvxContentPage<CalendarViewModel>
    {
        public CalendarPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Booking.IsEnabled = true;
            Booking.Opacity = 1;
        }
    }
}