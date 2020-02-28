using cabinets.Core.ViewModels.Profile;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(NoHistory = true, Animated = false)]
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
