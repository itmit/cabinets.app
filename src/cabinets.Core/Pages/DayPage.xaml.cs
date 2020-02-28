using cabinets.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DayPage : MvxContentPage<DayViewModel>
	{
		#region .ctor
		public DayPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			((ListView) sender).SelectedItem = null;
		}
		#endregion
	}
}
