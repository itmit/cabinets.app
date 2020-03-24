using cabinets.Core.ViewModels.Auth;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecoveryPage : MvxContentPage<RecoveryViewModel>
	{
		public RecoveryPage()
		{
			InitializeComponent();
		}
	}
}