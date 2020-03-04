using cabinets.Core.ViewModels;
using cabinets.Core.ViewModels.Calendar;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Calendar
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
