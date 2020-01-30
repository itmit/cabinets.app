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
	}
}
