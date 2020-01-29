using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using cabinets.Core.ViewModels.Cabinets;
using cabinets.Core.ViewModels.News;
using cabinets.Core.ViewModels.Profile;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels
{
	public class MainViewModel : MvxNavigationViewModel
	{
		private IMvxAsyncCommand _showInitialViewModelsCommand;

		public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{ }

		public IMvxAsyncCommand ShowInitialViewModelsCommand
		{
			get
			{
				_showInitialViewModelsCommand = _showInitialViewModelsCommand ?? new MvxAsyncCommand(ShowInitialViewModels);
				return _showInitialViewModelsCommand;
			}
		}

		private Task ShowInitialViewModels(CancellationToken arg)
		{
			var tasks = new List<Task>
			{
				NavigationService.Navigate<ProfileViewModel>(cancellationToken: arg),
				NavigationService.Navigate<CabinetsViewModel>(cancellationToken: arg),
				NavigationService.Navigate<CalendarViewModel>(cancellationToken: arg),
				NavigationService.Navigate<NewsViewModel>(cancellationToken: arg)
			};

			return Task.WhenAll(tasks);
		}
	}
}
