using cabinets.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.Cabinets
{
	public class CabinetDetailViewModel : MvxViewModel<Cabinet>
	{
		#region Data
		#region Fields
		private IMvxCommand _backCommand;
		private Cabinet _cabinet;

		private readonly IMvxNavigationService _navigationService;
		private IMvxCommand _openBookingCommand;
		private MvxObservableCollection<string> _photos;
		#endregion
		#endregion

		#region .ctor
		public CabinetDetailViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;
		#endregion

		#region Properties
		public IMvxCommand BackCommand
		{
			get
			{
				_backCommand = _backCommand ??
							   new MvxCommand(() =>
							   {
								   _navigationService.Close(this);
							   });

				return _backCommand;
			}
		}

		public Cabinet Cabinet
		{
			get => _cabinet;
			private set => SetProperty(ref _cabinet, value);
		}

		public IMvxCommand OpenBookingCommand
		{
			get
			{
				_openBookingCommand = _openBookingCommand ??
									  new MvxCommand(async () =>
									  {
										  var result = await _navigationService.Navigate<BookingViewModel, BookingViewModelAttribute, bool>(new BookingViewModelAttribute(Cabinet));
										  if (result)
										  {
											  await _navigationService.Close(this);
										  }
									  });
				return _openBookingCommand;
			}
		}

		public MvxObservableCollection<string> Photos
		{
			get => _photos;
			private set => SetProperty(ref _photos, value);
		}
		#endregion

		#region Overrided
		public override void Prepare(Cabinet parameter)
		{
			Cabinet = parameter;
		}
		#endregion
	}
}
