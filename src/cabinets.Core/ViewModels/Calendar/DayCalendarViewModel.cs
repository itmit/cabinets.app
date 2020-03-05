using System.Threading.Tasks;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.Calendar
{
	public class DayCalendarViewModel : MvxViewModel
	{
		private MvxObservableCollection<string> _times;

		public MvxObservableCollection<string> Times
		{
			get => _times;
			set => SetProperty(ref _times, value);
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			var times = new MvxObservableCollection<string>();

			for (int i = 7; i < 24; i++)
			{
				times.Add($"{i}:00");
				times.Add($"{i}:30");
			}

			Times = times;
		}
	}
}
