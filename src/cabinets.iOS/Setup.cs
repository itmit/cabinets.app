using cabinets.Core;
using cabinets.Core.Services;
using cabinets.iOS.Services;
using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.Forms.Presenters;
using MvvmCross.IoC;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.ViewModels;

namespace cabinets.iOS
{
	public class Setup : MvxFormsIosSetup
	{
		protected override IMvxApplication CreateApp() => new CoreApp();

		protected override Xamarin.Forms.Application CreateFormsApplication() => new App();

		protected override IMvxIoCProvider CreateIocProvider()
		{
			var provider = base.CreateIocProvider();
			provider.RegisterType<IFireBaseService, IosFireBaseService>();
			return provider;
		}


		protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
		{
			var formsPagePresenter = new CustomMvxFormsPagePresenter(viewPresenter);
			Mvx.IoCProvider.RegisterSingleton<IMvxFormsPagePresenter>(formsPagePresenter);
			return formsPagePresenter;
		}
	}
}
