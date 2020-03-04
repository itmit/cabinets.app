using System;
using System.Threading.Tasks;
using cabinets.Core.Models;
using cabinets.Core.Services;
using cabinets.Core.ViewModels.Cabinets;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.Calendar
{
	public class DayViewModel : MvxViewModel<DateTime>
	{
		#region Data
		#region Fields
		private MvxObservableCollection<CalendarDay> _cabinets;

		private readonly ICabinetsService _cabinetService;
		private readonly IMvxNavigationService _navigationService;
		private DateTime _parameter;
		private CalendarDay _selectedCabinet;
		#endregion
		#endregion

		#region .ctor
		public DayViewModel(ICabinetsService cabinetService, IMvxNavigationService navigationService)
		{
			_cabinetService = cabinetService;
			_navigationService = navigationService;
		}
		#endregion

		#region Properties
		public MvxObservableCollection<CalendarDay> Cabinets
		{
			get => _cabinets;
			private set => SetProperty(ref _cabinets, value);
		}

		public CalendarDay SelectedCabinet
		{
			get => _selectedCabinet;
			set
			{
				SetProperty(ref _selectedCabinet, value);
				if (value != null)
				{
					_navigationService.Navigate<BookingViewModel, BookingViewModelAttribute>(new BookingViewModelAttribute(value.Cabinet, _parameter));
				}
			}
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();
			try
			{
				Cabinets = new MvxObservableCollection<CalendarDay>(await _cabinetService.GetBusyCabinetsByDate(_parameter));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public override void Prepare(DateTime parameter)
		{
			_parameter = parameter;
		}
		#endregion
	}
}
