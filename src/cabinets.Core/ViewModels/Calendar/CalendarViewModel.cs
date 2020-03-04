using System;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace cabinets.Core.ViewModels.Calendar
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

		private MvxCommand _openCabinetsCommand;

		public MvxCommand OpenCabinetsCommand
		{
			get
			{
				_openCabinetsCommand = _openCabinetsCommand ?? new MvxCommand(() =>
				{
					if (Application.Current.MainPage is TabbedPage tabbedPage)
					{
						tabbedPage.CurrentPage = tabbedPage.Children[1];
						(tabbedPage.Children[1] as NavigationPage)?.PopToRootAsync();
					}
				});
				return _openCabinetsCommand;
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
