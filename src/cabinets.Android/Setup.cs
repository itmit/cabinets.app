using cabinets.Core;
using cabinets.Core.Services;
using cabinets.Droid.Services;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Presenters;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace cabinets.Droid
{
	public class Setup : MvxFormsAndroidSetup<CoreApp, App>
	{
		#region Overrided
		protected override IMvxApplication CreateApp() => new CoreApp();

		protected override Application CreateFormsApplication() => new App();

		protected override IMvxIoCProvider CreateIocProvider()
		{
			var provider = base.CreateIocProvider();
			provider.RegisterType<IFireBaseService, AndroidFireBaseService>();
			return provider;
		}

		protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
		{
			var formsPagePresenter = new CustomMvxFormsPagePresenter(viewPresenter);
			Mvx.IoCProvider.RegisterSingleton<IMvxFormsPagePresenter>(formsPagePresenter);
			return formsPagePresenter;
		}
		#endregion
	}
}
