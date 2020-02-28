using cabinets.Core.ViewModels.Auth;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PolicyPage : MvxContentPage<PolicyViewModel>
	{
		#region .ctor
		public PolicyPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
