using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using cabinets.Core.ViewModels.Cabinets;
using cabinets.Core.ViewModels.Calendar;
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
		#region Data
		#region Fields
		private IMvxAsyncCommand _showInitialViewModelsCommand;
		private Type _type;
		#endregion
		#endregion

		#region .ctor
		public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
		#endregion

		#region Properties
		public int FirstPageIndex
		{
			get;
			private set;
		}

		public override void ViewAppearing()
		{
			base.ViewAppearing();
			ShowInitialViewModelsCommand.ExecuteAsync();
		}

		public IMvxAsyncCommand ShowInitialViewModelsCommand
		{
			get
			{
				_showInitialViewModelsCommand = _showInitialViewModelsCommand ?? new MvxAsyncCommand(ShowInitialViewModels);
				return _showInitialViewModelsCommand;
			}
		}
		#endregion

		#region Public
		public void NavigateToMainPage()
		{
			NavigationService.Navigate<MainViewModel>();
		}
		#endregion

		#region Overrided
		public override void Prepare(Type parameter)
		{
			_type = parameter;
		}
		#endregion

		#region Private
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
		#endregion
	}
}
