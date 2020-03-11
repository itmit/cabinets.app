using System;
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
		#region Data
		#region Fields
		private int _amount;
		private readonly IAuthService _authService;
		private MvxObservableCollection<Reservation> _bookings;
		private bool _isRefreshing;
		private MvxCommand _logoutCommand;
		private readonly IProfileService _profileService;
		private MvxCommand _refreshCommand;
		private Reservation _selectedReservation;
		private User _user;
		private readonly IUserRepository _userRepository;
		private readonly IFireBaseService _fireBaseService;
		#endregion
		#endregion

		#region .ctor
		public ProfileViewModel(IMvxLogProvider logProvider,
								IMvxNavigationService navigationService,
								IUserRepository userRepository,
								IAuthService authService,
								IProfileService profileService,
								ICabinetsService cabinetsService,
								IFireBaseService fireBaseService)
			: base(logProvider, navigationService)
		{
			_userRepository = userRepository;
			_authService = authService;
			_fireBaseService = fireBaseService;
			User = _authService.User;
			_profileService = profileService;
			cabinetsService.MakeReservationSuccesed += CabinetsServiceOnMakeReservationSucceed;
		}

		private void CabinetsServiceOnMakeReservationSucceed(object sender, EventArgs e)
		{
			Refresh();
		}
		#endregion

		#region Properties
		public int Amount
		{
			get => _amount;
			set => SetProperty(ref _amount, value);
		}

		public MvxObservableCollection<Reservation> Bookings
		{
			get => _bookings;
			set => SetProperty(ref _bookings, value);
		}

		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
		}

		public MvxCommand LogoutCommand
		{
			get
			{
				_logoutCommand = _logoutCommand ?? new MvxCommand(LogoutCommandExecute);
				return _logoutCommand;
			}
		}

		public IMvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ?? new MvxCommand(Refresh);
				return _refreshCommand;
			}
		}

		public Reservation SelectedReservation
		{
			get => _selectedReservation;
			set
			{
				if (value == null)
				{
					return;
				}

				SetProperty(ref _selectedReservation, value);

				SelectedBookingExecute(value);
			}
		}

		public async void SelectedBookingExecute(Reservation reservation)
		{
			if (await NavigationService.Navigate<MyBookingViewModel, Reservation, bool>(reservation))
			{
				Refresh();
			}
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			await Task.Run(Refresh);
		}
		#endregion

		#region Private
		private void LogoutCommandExecute()
		{
			_authService.Logout(User);
			_fireBaseService.DeleteInstance();
			_userRepository.Remove(User);
			NavigationService.Navigate<AuthorizationViewModel>();
		}

		private async void Refresh()
		{
			IsRefreshing = true;
			try
			{
				Amount = await _profileService.GetAmount();
				Bookings = new MvxObservableCollection<Reservation>(await _profileService.GetReservations());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			IsRefreshing = false;
		}
		#endregion
	}
}
