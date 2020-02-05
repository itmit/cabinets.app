using System;
using System.Threading.Tasks;
using cabinets.Core.Models;
using cabinets.Core.Services;
using cabinets.Core.ViewModels.Profile;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.Cabinets
{
	public class CabinetsViewModel : MvxNavigationViewModel
	{

		private MvxObservableCollection<Cabinet> _cabinets;
		private readonly ICabinetsService _cabinetsService;
		private Cabinet _selectedCabinet;

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

		public MvxObservableCollection<Cabinet> Cabinets
		{
			get => _cabinets;
			set => SetProperty(ref _cabinets, value);
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

				if (SetProperty(ref _selectedCabinet, value))
				{
					NavigationService.Navigate<CabinetDetailViewModel, Cabinet>(value);
				}
			}
		}

		public CabinetsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ICabinetsService cabinetsService)
			: base(logProvider, navigationService)
		{
			_cabinetsService = cabinetsService;
		}
	}
}
