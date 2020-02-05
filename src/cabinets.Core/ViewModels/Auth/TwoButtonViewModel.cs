using System;
using cabinets.Core.ViewModels.Cabinets;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.Auth
{
	public class TwoButtonViewModel : MvxViewModel
	{
		private MvxCommand _openCabinetsCommand;
		private MvxCommand _openCalendarCommand;

		private readonly IMvxNavigationService _navigationService;

		public TwoButtonViewModel(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		public MvxCommand OpenCabinetsCommand
		{
			get
			{
				_openCabinetsCommand = _openCabinetsCommand ?? new MvxCommand(() =>
				{
					_navigationService.Navigate<MainViewModel, Type>(typeof(CabinetsViewModel));
				});

				return _openCabinetsCommand;
			}
		}

		public MvxCommand OpenCalendarCommand
		{
			get
			{
				_openCalendarCommand = _openCalendarCommand ?? new MvxCommand(() =>
				{
					_navigationService.Navigate<MainViewModel, Type>(typeof(CalendarViewModel));
				});

				return _openCalendarCommand;
			}
		}
	}
}
