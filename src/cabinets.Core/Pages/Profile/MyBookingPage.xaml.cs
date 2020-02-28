using System;
using cabinets.Core.ViewModels.Profile;
using cabinets.Pages;
using MvvmCross.Forms.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyBookingPage : MvxContentPage<MyBookingViewModel>
	{
		#region .ctor
		public MyBookingPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void Button_Clicked(object sender, EventArgs e)
		{
			EditContentView.IsVisible = true;
			FirstContentView.IsVisible = false;
		}

		private void Button_Clicked_1(object sender, EventArgs e)
		{
			var page = new SelectPopupPage();
			Navigation.PushPopupAsync(page);
		}
		#endregion
	}
}
