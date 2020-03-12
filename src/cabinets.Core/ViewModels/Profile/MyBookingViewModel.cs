using System;
using System.Linq;
using System.Threading.Tasks;
using cabinets.Core.Models;
using cabinets.Core.Repositories;
using cabinets.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace cabinets.Core.ViewModels.Profile
{
	public class MyBookingViewModel : MvxViewModel<Reservation, bool>
	{
		#region Data
		#region Fields
		private DateTime _date;
		private Reservation _parameter;

		private readonly IProfileService _profileService;
		private Reservation _reservation;
		private User _user;
		private MvxCommand _cancelCommand;
		private readonly IMvxNavigationService _navigationService;
		private string _status;
		#endregion
		#endregion

		#region .ctor
		public MyBookingViewModel(IProfileService profileService, IAuthService authService, IMvxNavigationService navigationService)
		{
			_profileService = profileService;
			User = authService.User;
			_navigationService = navigationService;
		}
		#endregion

		#region Properties
		public DateTime Date
		{
			get => _date;
			set => SetProperty(ref _date, value);
		}

		public string Status
		{
			get => _status;
			set => SetProperty(ref _status, value);
		}

		public Reservation Reservation
		{
			get => _reservation;
			private set => SetProperty(ref _reservation, value);
		}

		public MvxCommand CancelCommand
		{
			get
			{
				_cancelCommand = _cancelCommand ?? new MvxCommand(CancelCommandExecute);
				return _cancelCommand;
			}
		}

		private async void CancelCommandExecute()
		{
			var confirm = await Application.Current.MainPage.DisplayAlert("Внимание",
																		  "Вы действительно хотите отменить бронирование?", "Да", "Нет");
			if (confirm)
			{
				bool isCanceled = false;
				try
				{
					isCanceled = await _profileService.CancelReservation(_parameter.Uuid);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}

				if (isCanceled)
				{
					await _navigationService.Close(this, true);
					Device.BeginInvokeOnMainThread(() =>
					{
						Application.Current.MainPage.DisplayAlert("Внимание", "Бронирование отменено", "Ок");
					});
				}
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

			Reservation = await _profileService.GetReservationDetail(_parameter.Uuid);
			Status = Reservation.IsPaid ? "Оплачено" : "Не оплачено";
		}

		public override void Prepare(Reservation parameter)
		{
			_parameter = parameter;
			Date = parameter.Date;
		}
		#endregion
	}
}
