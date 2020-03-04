using System.Linq;
using cabinets.Core.Repositories;
using cabinets.Core.ViewModels;
using cabinets.Core.ViewModels.Auth;
using MvvmCross;
using MvvmCross.Forms.Presenters;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Realms;

namespace cabinets.Core
{
	public class CoreApp : MvxApplication
	{
		#region Overrided
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

			RealmConfiguration.DefaultConfiguration.SchemaVersion = 2;

			var userRepository = Mvx.IoCProvider.Resolve<IUserRepository>();

			var user = userRepository.GetAll()
									 .SingleOrDefault();

			if (user?.AccessToken == null)
			{
				RegisterAppStart<AuthorizationViewModel>();
				return;
			}

			RegisterAppStart<MainViewModel>();
		}
		#endregion
	}
}
