using cabinets.Core.ViewModels.Cabinets;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Cabinets
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CabinetsDetailPage : MvxContentPage<CabinetDetailViewModel>
	{
		#region .ctor
		public CabinetsDetailPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
