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
		#region .ctor
		public BookingPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			if (sender is Frame frame)
			{
				if (frame.Content is Label label)
				{
					var time = ViewModel.SelectedTimes.SingleOrDefault(t => t.Value.Equals(label.Text));
					if (time != null)
					{
						frame.BackgroundColor = Color.Default;
						label.TextColor = Color.FromHex("#505050");
						ViewModel.SelectedTimes.Remove(time);
					}
					else
					{
						frame.BackgroundColor = Color.FromHex("#31516D");
						label.TextColor = Color.White;
						ViewModel.SelectedTimes.Add(ViewModel.Times.Single(t => t.Value.Equals(label.Text)));
					}

					ViewModel.IsReservationEnabled = ViewModel.SelectedTimes.Count > 0;
				}
			}
		}
		#endregion
	}
}
