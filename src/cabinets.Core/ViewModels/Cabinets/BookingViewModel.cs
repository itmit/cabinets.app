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
		#region Data
		#region Fields
		private MvxCommand _backCommand;
		private Cabinet _cabinet;
		private readonly ICabinetsService _cabinetsService;
		private bool _isReservationEnabled;
		private readonly IMvxNavigationService _navigationService;
		private IMvxCommand _reservationCommand;
		private DateTime _selectedDate = DateTime.Now;
		private MvxObservableCollection<CabinetTime> _selectedTimes = new MvxObservableCollection<CabinetTime>();
		private MvxObservableCollection<CabinetTime> _times;
		private int _price;
		#endregion
		#endregion

		#region .ctor
		public BookingViewModel(ICabinetsService cabinetsService, IMvxNavigationService navigationService)
		{
			_cabinetsService = cabinetsService;
			_navigationService = navigationService;
		}
		#endregion

		#region Properties
		public IMvxCommand BackCommand
		{
			get
			{
				_backCommand = _backCommand ??
							   new MvxCommand(() =>
							   {
								   (Application.Current.MainPage as TabbedPage)?.CurrentPage.Navigation.PopToRootAsync();
							   });
				return _backCommand;
			}
		}

		public Cabinet Cabinet
		{
			get => _cabinet;
			private set => SetProperty(ref _cabinet, value);
		}

		public bool IsReservationEnabled
		{
			get => _isReservationEnabled;
			set => SetProperty(ref _isReservationEnabled, value);
		}

		public IMvxCommand ReservationCommand
		{
			get
			{
				_reservationCommand = _reservationCommand ?? new MvxCommand(Reservation, () => IsReservationEnabled);
				return _reservationCommand;
			}
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

		public MvxObservableCollection<CabinetTime> SelectedTimes
		{
			get => _selectedTimes;
			set => SetProperty(ref _selectedTimes, value);
		}

		public void RestatePrice()
		{
			int price = 0;
			foreach (var time in SelectedTimes)
			{
				price += time.Key < 18 ? Cabinet.PriceMorning : Cabinet.PriceEvening;
			}

			Price = price / 2;
		}

		public MvxObservableCollection<CabinetTime> Times
		{
			get => _times;
			private set => SetProperty(ref _times, value);
		}

		public int Price
		{
			get => _price;
			private set => SetProperty(ref _price, value);
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();
			try
			{
				Times = new MvxObservableCollection<CabinetTime>(await _cabinetsService.CheckCabinetByDate(Cabinet, SelectedDate));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public override void Prepare(BookingViewModelAttribute parameter)
		{
			Cabinet = parameter.Cabinet;
			SelectedDate = parameter.Date ?? DateTime.Now;
		}
		#endregion

		#region Private
		private async void LoadTimes(Cabinet cabinet, DateTime date)
		{
			Times = new MvxObservableCollection<CabinetTime>(await _cabinetsService.CheckCabinetByDate(cabinet, date));
		}

		private async void Reservation()
		{
			IsReservationEnabled = false;
			var result = false;
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
		#endregion
	}
}

public class BookingViewModelAttribute
{
	#region .ctor
	public BookingViewModelAttribute(Cabinet cabinet, DateTime? date = null)
	{
		Cabinet = cabinet;
		Date = date;
	}
	#endregion

	#region Properties
	public Cabinet Cabinet
	{
		get;
	}

	public DateTime? Date
	{
		get;
	}
	#endregion
}
