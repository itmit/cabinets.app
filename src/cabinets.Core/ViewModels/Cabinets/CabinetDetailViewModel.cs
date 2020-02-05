using cabinets.Core.Models;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.Cabinets
{
	public class CabinetDetailViewModel : MvxViewModel<Cabinet>
	{
		private Cabinet _cabinet;

		public Cabinet Cabinet
		{
			get => _cabinet;
			private set => SetProperty(ref _cabinet, value);
		}

		public override void Prepare(Cabinet parameter)
		{
			Cabinet = parameter;
		}
	}
}
