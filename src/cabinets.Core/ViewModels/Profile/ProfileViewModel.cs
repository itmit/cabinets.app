using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cabinets.Core.Models;
using cabinets.Core.Repositories;
using cabinets.Core.Services;
using cabinets.Core.ViewModels.Auth;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.Profile
{
	public class ProfileViewModel : MvxNavigationViewModel
	{
		private MvxObservableCollection<Cabinet> _bookings;
		private readonly IUserRepository _userRepository;
		private User _user;
		private MvxCommand _logoutCommand;
		private readonly IAuthService _authService;

		public override async Task Initialize()
		{
			await base.Initialize();

			Bookings = new MvxObservableCollection<Cabinet> {
				new Cabinet
				{
					Number = 1,
					Date = "01.01.2020"
				},
				new Cabinet
				{
					Number = 2,
					Date = "02.01.2020"
				},
				new Cabinet
				{
					Number = 3,
					Date = "03.01.2020"
				}

			};

			User = _userRepository.GetAll()
						   .Single();
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}

		public MvxObservableCollection<Cabinet> Bookings
		{
			get => _bookings;
			set => _bookings = value;
		}

		public MvxCommand LogoutCommand
		{
			get
			{
				_logoutCommand = _logoutCommand ?? new MvxCommand(LogoutCommandExecute);
				return _logoutCommand;
			}
		}

		private void LogoutCommandExecute()
		{
			_authService.Logout(User);
			_userRepository.Remove(User);
			NavigationService.Navigate<AuthorizationViewModel>();
		}

		public ProfileViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserRepository userRepository, IAuthService authService)
			: base(logProvider, navigationService)
		{
			_userRepository = userRepository;
			_authService = authService;
		}
	}
}
