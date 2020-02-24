using System;
using System.Threading.Tasks;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels
{
	public class CalendarViewModel : MvxNavigationViewModel
	{
		public CalendarViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}

		private DateTime? _selectedDate;

		public override async Task Initialize()
		{
			await base.Initialize();
		}

		public DateTime? SelectedDate
		{
			get => _selectedDate;
			set
			{
				SetProperty(ref _selectedDate, value);
				if (value != null)
				{
					NavigationService.Navigate<DayViewModel, DateTime>(value.Value);
				}
			}
		}
	}
}
