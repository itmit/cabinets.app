using System.Threading.Tasks;
using cabinets.Models;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.Cabinets
{
	public class CabinetsViewModel : MvxNavigationViewModel
	{

		private MvxObservableCollection<CabinetModel> _cabinets;
		private int _number;
		private string _image;
		private int _capacity;
		private double _square;
		private string _boxColor;

		public override async Task Initialize()
		{
			await base.Initialize();
			Cabinets = new MvxObservableCollection<CabinetModel>
			{
				new CabinetModel
				{
					Number = 1,
					Image = "pic_cabinet.png",
					Сapacity = 7,
					Square = 14,
					BoxColor = "#7B1FA2"
				},
				new CabinetModel
				{
					Number = 2,
					Image = "pic_cabinet2.png",
					Сapacity = 9,
					Square = 19,
					BoxColor = "#FF5252"
				},
				new CabinetModel
				{
					Number = 3,
					Image = "pic_cabinet3.png",
					Сapacity = 4,
					Square = 5,
					BoxColor = "#0D47A1"
				}
			};
		}

		public MvxObservableCollection<CabinetModel> Cabinets
		{
			get => _cabinets;
			set => _cabinets = value;
		}

		public int Number
		{
			get => _number;
			set => _number = value;
		}

		public string Image
		{
			get => _image;
			set => _image = value;
		}

		public int Сapacity
		{
			get => _capacity;
			set => _capacity = value;
		}

		public double Square
		{
			get => _square;
			set => _square = value;
		}

		public string BoxColor
		{
			get => _boxColor;
			set => _boxColor = value;
		}

		public CabinetsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
	}
}
