using cabinets.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.Cabinets
{
	public class CabinetDetailViewModel : MvxViewModel<Cabinet>
	{
		private Cabinet _cabinet;
		private MvxObservableCollection<string> _photos;
		private IMvxCommand _backCommand;

		public Cabinet Cabinet
		{
			get => _cabinet;
			private set => SetProperty(ref _cabinet, value);
		}

		public MvxObservableCollection<string> Photos
		{
			get => _photos;
			private set => SetProperty(ref _photos, value);
		}

		public override void Prepare(Cabinet parameter)
		{
			Cabinet = parameter;
		}

		private readonly IMvxNavigationService _navigationService;
		private IMvxCommand _openBookingCommand;

		public CabinetDetailViewModel(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		public IMvxCommand OpenBookingCommand
		{
			get
			{
				_openBookingCommand = _openBookingCommand ??
									  new MvxCommand(async () =>
									  {
										  var result = await _navigationService.Navigate<BookingViewModel, Cabinet, bool>(Cabinet);
										  if (result)
										  {
											  await _navigationService.Close(this);
										  }
									  });
				return _openBookingCommand;
			}
		}

		public IMvxCommand BackCommand
		{
			get
			{
				_backCommand = _backCommand ?? new MvxCommand(() =>
				{
					_navigationService.Close(this);
				});

				return _backCommand;
			}
		}
	}
}
