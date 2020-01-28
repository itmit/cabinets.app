using System.Linq;
using cabinets.Core.Models;
using cabinets.Core.Repositories;
using cabinets.Core.ViewModels;
using cabinets.Core.ViewModels.Auth;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Xamarin.Essentials;

namespace cabinets.Core
{
	public class CoreApp : MvxApplication
	{
		public override void Initialize()
		{
			CreatableTypes()
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsLazySingleton();

			CreatableTypes()
				.InNamespace("cabinets.Core.Repositories")
				.EndingWith("Repository")
				.AsInterfaces()
				.RegisterAsDynamic();
			
			var userRepository = Mvx.IoCProvider.Resolve<IUserRepository>();

			User user = userRepository.GetUsers().SingleOrDefault();
			
			if (user?.AccessToken == null)
			{
				RegisterAppStart<AuthorizationViewModel>();
				return;
			}
			
			RegisterAppStart<MainViewModel>();
		}
	}
}
