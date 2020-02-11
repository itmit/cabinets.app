using System;
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
		private MvxObservableCollection<Reservation> _bookings;
		private readonly IUserRepository _userRepository;
		private User _user;
		private MvxCommand _logoutCommand;
		private readonly IAuthService _authService;
		private readonly IProfileService _profileService;

		public override async Task Initialize()
		{
			await base.Initialize();

			User = _userRepository.GetAll()
						   .Single();

			try
			{
				Bookings = new MvxObservableCollection<Reservation>(await _profileService.GetReservations());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}

		public MvxObservableCollection<Reservation> Bookings
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

		public ProfileViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserRepository userRepository, IAuthService authService, IProfileService profileService)
			: base(logProvider, navigationService)
		{
			_userRepository = userRepository;
			_authService = authService;
			_profileService = profileService;
		}
	}
}
