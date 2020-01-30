using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cabinets.Core.Models;
using cabinets.Core.Repositories;
using cabinets.Models;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.Profile
{
	public class ProfileViewModel : MvxNavigationViewModel
	{
		private MvxObservableCollection<CabinetModel> _bookings;
		private readonly IUserRepository _userRepository;
		private User _user;

		public override async Task Initialize()
		{
			await base.Initialize();

			Bookings = new MvxObservableCollection<CabinetModel> {
				new CabinetModel
				{
					Number = 1,
					Date = "01.01.2020"
				},
				new CabinetModel
				{
					Number = 2,
					Date = "02.01.2020"
				},
				new CabinetModel
				{
					Number = 3,
					Date = "03.01.2020"
				}

			};

			User = _userRepository.GetAll()
						   .Single();
		}

		public User User
		{
			get => _user;
			private set => SetProperty(ref _user, value);
		}

		public MvxObservableCollection<CabinetModel> Bookings
		{
			get => _bookings;
			set => _bookings = value;
		}

		public ProfileViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserRepository userRepository)
			: base(logProvider, navigationService)
		{
			_userRepository = userRepository;
		}
	}
}
