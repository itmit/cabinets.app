using System;
using System.Threading.Tasks;
using cabinets.Core.Models;
using cabinets.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace cabinets.Core.ViewModels.Cabinets
{
	public class BookingViewModel : MvxViewModel<BookingViewModelAttribute, bool>
	{
		private Cabinet _cabinet;
		private DateTime _selectedDate = DateTime.Now;
		private readonly IMvxNavigationService _navigationService;
		private readonly ICabinetsService _cabinetsService;
		private MvxObservableCollection<CabinetTime> _times;
		private MvxObservableCollection<CabinetTime> _selectedTimes = new MvxObservableCollection<CabinetTime>();
		private bool _isReservationEnabled;

		public BookingViewModel(ICabinetsService cabinetsService, IMvxNavigationService navigationService)
		{
			_cabinetsService = cabinetsService;
			_navigationService = navigationService;
		}

		public override async Task Initialize()
		{
			await base.Initialize();
            try
            {
                Times = new MvxObservableCollection<CabinetTime>(await _cabinetsService.CheckCabinetByDate(Cabinet, SelectedDate));
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
		}

		public bool IsReservationEnabled
		{
			get => _isReservationEnabled;
			set => SetProperty(ref _isReservationEnabled, value);
		}

		public MvxObservableCollection<CabinetTime> Times
		{
			get => _times;
			private set => SetProperty(ref _times, value);
		}

		public MvxObservableCollection<CabinetTime> SelectedTimes
		{
			get => _selectedTimes;
			set => SetProperty(ref _selectedTimes, value);
		}

		public Cabinet Cabinet
		{
			get => _cabinet;
			private set => SetProperty(ref _cabinet, value);
		}

		public override void Prepare(BookingViewModelAttribute parameter)
		{
			Cabinet = parameter.Cabinet;
            SelectedDate = parameter.Date == null ? DateTime.Now : parameter.Date.Value;
        }

		public IMvxCommand ReservationCommand
		{
			get
			{
				_reservationCommand = _reservationCommand ?? new MvxCommand(Reservation, () => IsReservationEnabled);
				return _reservationCommand;
			}
		}

		private async void Reservation()
		{
			IsReservationEnabled = false;
			bool result = false;
			try
			{
				result = await _cabinetsService.MakeReservation(Cabinet, SelectedDate, SelectedTimes);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			IsReservationEnabled = SelectedTimes.Count > 0;
			if (result)
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", "Кабинет успешно забронирован", "Ок");
				});
				return;
			}

			if (_cabinetsService.Errors.ContainsKey("Fatal"))
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", _cabinetsService.Errors["Fatal"], "Ок");
				});
				return;
			}

			Device.BeginInvokeOnMainThread(() =>
			{
				Application.Current.MainPage.DisplayAlert("Внимание", "Ошибка сервера.", "Ок");
			});
		}

		public DateTime SelectedDate
		{
			get => _selectedDate;
			set
			{
				SetProperty(ref _selectedDate, value);

				LoadTimes(Cabinet, value);
			}
		}

		private MvxCommand _backCommand;
		private IMvxCommand _reservationCommand;

		public IMvxCommand BackCommand
		{
			get
			{
				_backCommand = _backCommand ?? new MvxCommand(() =>
				{
					_navigationService.Close(this, true);
				});
				return _backCommand;
			}
		}

		private async void LoadTimes(Cabinet cabinet, DateTime date)
		{
			Times = new MvxObservableCollection<CabinetTime>(await _cabinetsService.CheckCabinetByDate(cabinet, date));
		}
	}
}
public class BookingViewModelAttribute
{
    public BookingViewModelAttribute(Cabinet cabinet, DateTime? date = null)
    {
        Cabinet = cabinet;
        Date = date;
    }

    public Cabinet Cabinet
    {
        get;
    }

    public DateTime? Date
    {
        get;
    }
}