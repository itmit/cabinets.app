using System;
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
	public class MainViewModel : MvxNavigationViewModel<Type>
	{
		private IMvxAsyncCommand _showInitialViewModelsCommand;
		private Type _type;

		public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{ }

		public void NavigateToMainPage()
		{
			NavigationService.Navigate<MainViewModel>();
		}

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
			if (_type == typeof(CabinetsViewModel))
			{
				FirstPageIndex = 1;
			}

			if (_type == typeof(CalendarViewModel))
			{
				FirstPageIndex = 2;
			}
			var tasks = new List<Task>
			{
				NavigationService.Navigate<ProfileViewModel>(cancellationToken: arg),
				NavigationService.Navigate<CabinetsViewModel>(cancellationToken: arg),
				NavigationService.Navigate<CalendarViewModel>(cancellationToken: arg),
				NavigationService.Navigate<NewsViewModel>(cancellationToken: arg)
			};

			return Task.WhenAll(tasks);
		}

		public int FirstPageIndex
		{
			get;
			private set;
		}

		public override void Prepare(Type parameter)
		{
			_type = parameter;
		}
	}
}
