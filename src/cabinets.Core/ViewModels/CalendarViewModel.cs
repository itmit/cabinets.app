using System;
using System.Threading.Tasks;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels
{
	public class CalendarViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		private DateTime? _selectedDate;
		#endregion
		#endregion

		#region .ctor
		public CalendarViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion

		#region Properties
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
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();
		}
		#endregion
	}
}
