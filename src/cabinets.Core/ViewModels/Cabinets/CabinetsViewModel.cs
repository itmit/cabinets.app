using System;
using System.Threading.Tasks;
using cabinets.Core.Models;
using cabinets.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.Cabinets
{
	public class CabinetsViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		private MvxObservableCollection<Cabinet> _cabinets;
		private readonly ICabinetsService _cabinetsService;
		private bool _isRefreshing;
		private MvxCommand _refreshCommand;
		private Cabinet _selectedCabinet;
		#endregion
		#endregion

		#region .ctor
		public CabinetsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ICabinetsService cabinetsService)
			: base(logProvider, navigationService) =>
			_cabinetsService = cabinetsService;
		#endregion

		#region Properties
		public MvxObservableCollection<Cabinet> Cabinets
		{
			get => _cabinets;
			set => SetProperty(ref _cabinets, value);
		}

		public bool IsRefreshing
		{
			get => _isRefreshing;
			set => SetProperty(ref _isRefreshing, value);
		}

		public IMvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ?? new MvxCommand(Refresh);
				return _refreshCommand;
			}
		}

		public Cabinet SelectedCabinet
		{
			get => _selectedCabinet;
			set
			{
				if (value == null)
				{
					return;
				}

				OpenDetailPage(value);
				SetProperty(ref _selectedCabinet, value);
			}
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				Cabinets = new MvxObservableCollection<Cabinet>(await _cabinetsService.GetAll());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		#endregion

		#region Private
		private async void OpenDetailPage(Cabinet cabinet)
		{
			await NavigationService.Navigate<CabinetDetailViewModel, Cabinet>(await _cabinetsService.GetCabinetDetail(cabinet.Uuid));
		}

		private async void Refresh()
		{
			IsRefreshing = true;
			try
			{
				Cabinets = new MvxObservableCollection<Cabinet>(await _cabinetsService.GetAll());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			IsRefreshing = false;
		}
		#endregion
	}
}
