using System;
using cabinets.Core.ViewModels.Cabinets;
using cabinets.Core.ViewModels.Calendar;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.Auth
{
	public class TwoButtonViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _openCabinetsCommand;
		private MvxCommand _openCalendarCommand;
		#endregion
		#endregion

		#region .ctor
		public TwoButtonViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;
		#endregion

		#region Properties
		public MvxCommand OpenCabinetsCommand
		{
			get
			{
				_openCabinetsCommand = _openCabinetsCommand ??
									   new MvxCommand(() =>
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
				_openCalendarCommand = _openCalendarCommand ??
									   new MvxCommand(() =>
									   {
										   _navigationService.Navigate<MainViewModel, Type>(typeof(CalendarViewModel));
									   });

				return _openCalendarCommand;
			}
		}
		#endregion
	}
}
