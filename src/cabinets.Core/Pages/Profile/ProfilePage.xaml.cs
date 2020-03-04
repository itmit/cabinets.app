using System;
using System.Collections.Generic;
using System.Linq;
using cabinets.Core.Models;
using cabinets.Core.Pages.Auth;
using cabinets.Core.ViewModels.Profile;
using cabinets.Core.Views;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, WrapInNavigationPage = true)]
	public partial class ProfilePage : MvxContentPage<ProfileViewModel>
	{
		#region .ctor
		public ProfilePage()
		{
			InitializeComponent();
		}
		#endregion

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			ViewModel.SelectedReservation = (sender as View)?.BindingContext as Reservation;
		}
	}
}
