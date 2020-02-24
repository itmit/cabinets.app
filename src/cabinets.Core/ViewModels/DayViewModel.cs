using System;
using System.Threading.Tasks;
using cabinets.Core.Models;
using cabinets.Core.Services;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels
{
	public class DayViewModel : MvxViewModel<DateTime>
	{
		private DateTime _parameter;

		public override void Prepare(DateTime parameter)
		{
			_parameter = parameter;
		}

		private readonly ICabinetsService _cabinetService;
		private MvxObservableCollection<CalendarDay> _cabinets;

		public MvxObservableCollection<CalendarDay> Cabinets
		{
			get => _cabinets;
			private set => SetProperty(ref _cabinets, value);
		}

		public DayViewModel(ICabinetsService cabinetService)
		{
			_cabinetService = cabinetService;
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			Cabinets = new MvxObservableCollection<CalendarDay>(await _cabinetService.GetBusyCabinetsByDate(_parameter));
		}
	}
}
