using System;
using cabinets.Core.Pages.Auth;
using cabinets.Core.ViewModels.Profile;
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

		#region Private
		private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			((ListView) sender).SelectedItem = null;
		}
		#endregion
	}
}
