using cabinets.Core.ViewModels.Auth;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrationPage : MvxContentPage<RegistrationViewModel>
	{
		#region .ctor
		public RegistrationPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
