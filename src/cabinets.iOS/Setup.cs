
using cabinets.Core;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.Forms.Presenters;
using MvvmCross.Platforms.Ios.Presenters;
using MvvmCross.ViewModels;
using UIKit;
using Xamarin.Forms;

namespace cabinets.iOS
{
	public class Setup : MvxFormsIosSetup<CoreApp, App>
	{
		protected override IMvxApplication CreateApp() =>  new CoreApp();

		protected override Xamarin.Forms.Application CreateFormsApplication() => new App();
	}
}