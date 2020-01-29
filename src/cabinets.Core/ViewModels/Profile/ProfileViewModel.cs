using System.Collections.Generic;
using System.Threading.Tasks;
using cabinets.Models;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.Profile
{
	public class ProfileViewModel : MvxViewModel
	{
		private MvxObservableCollection<CabinetModel> _bookings;
		private string _fullName;
		private string _phone;
		private string _email;

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
		}

		public MvxObservableCollection<CabinetModel> Bookings
		{
			get => _bookings;
			set => _bookings = value;
		}

		public string FullName
		{
			get => _fullName;
			set => _fullName = value;
		}

		public string Phone
		{
			get => _phone;
			set => _phone = value;
		}

		public string Email
		{
			get => _email;
			set => _email = value;
		}
    }
}
