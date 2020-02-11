using System;
using System.Linq;
using cabinets.Core.ViewModels.Cabinets;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Cabinets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingPage : MvxContentPage<BookingViewModel>
    {
        public BookingPage()
        {
            InitializeComponent();
        }

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
            if (sender is Frame frame)
            {
                if (frame.Content is Label label)
                {
					if (ViewModel.SelectedTimes.Any(time => time.Equals(label.Text)))
					{
						frame.BackgroundColor = Color.Default;
						label.TextColor = Color.FromHex("#505050");
						ViewModel.SelectedTimes.Remove(label.Text);
					}
					else
					{
						frame.BackgroundColor = Color.FromHex("#31516D");
						label.TextColor = Color.White;
						ViewModel.SelectedTimes.Add(label.Text);
					}

					ViewModel.IsReservationEnabled = ViewModel.SelectedTimes.Count > 0;
				}
			}
        }
	}
}