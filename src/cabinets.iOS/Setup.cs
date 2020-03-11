using cabinets.Core;
using cabinets.Core.Services;
using cabinets.iOS.Services;
using MvvmCross.Forms.Platforms.Ios.Core;
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
			provider.RegisterType<ISubscribeTopicFireBase, SubscribeTopicFireBase>();
			return provider;
		}
	}
}
