using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cabinets.Core.Models;
using cabinets.Core.Services;
using cabinets.Core.ViewModels.Cabinets;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace cabinets.Core.ViewModels.Calendar
{
	public class DayViewModel : MvxViewModel<DateTime>
	{
		#region Data
		#region Fields
		private MvxObservableCollection<CalendarDay> _cabinets;

		private readonly ICabinetsService _cabinetService;
		private readonly IMvxNavigationService _navigationService;
		private DateTime _parameter;
		private DateTime _dateTime;

		public DateTime DateTime { get => _dateTime; private set => SetProperty(ref _dateTime, value); }

		private CalendarDay _selectedCabinet;
		private MvxObservableCollection<RowDefinition> _rows;
		private MvxObservableCollection<string> _times;
		private MvxObservableCollection<CalendarEventModel> _events;
		#endregion
		#endregion

		#region .ctor
		public DayViewModel(ICabinetsService cabinetService, IMvxNavigationService navigationService)
		{
			_cabinetService = cabinetService;
			_navigationService = navigationService;
		}
		#endregion

		#region Properties
		public MvxObservableCollection<CalendarDay> Cabinets
		{
			get => _cabinets;
			private set => SetProperty(ref _cabinets, value);
		}

		public CalendarDay SelectedCabinet
		{
			get => _selectedCabinet;
			set
			{
				SetProperty(ref _selectedCabinet, value);
				if (value != null)
				{
					_navigationService.Navigate<BookingViewModel, BookingViewModelAttribute>(new BookingViewModelAttribute(value.Cabinet, _parameter));
				}
			}
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			var times = new MvxObservableCollection<string>();
			var rows = new MvxObservableCollection<RowDefinition>();
			for (int i = 7; i < 24; i++)
			{
				times.Add($"{i}:00");
				times.Add($"{i}:30");
				rows.Add(new RowDefinition());
				rows.Add(new RowDefinition());
			}

			Rows = rows;
			Times = times;

			try
			{
				Cabinets = new MvxObservableCollection<CalendarDay>(await _cabinetService.GetBusyCabinetsByDate(_parameter));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			var events = new MvxObservableCollection<CalendarEventModel>();

			foreach (var cabinet in Cabinets)
			{
				var cabTimes = new List<string>();
				foreach (var time in cabinet.Times)
				{
					cabTimes.Add(time.Split('-')[0]);
				}
				var indexes = new List<int>();
				foreach (var cabTime in cabTimes)
				{
					indexes.Add(Times.IndexOf(cabTime));
				}

				int minIndex = indexes[0];
				int height = 1;
				var eventTimes = new List<string>();

				for (int i = 1; i < indexes.Count; i++)
				{
					if (indexes[i] == indexes[i - 1] + 1 && i != indexes.Count - 1)
					{
						eventTimes.Add(Times[indexes[i]]);
						height++;
						continue;
					}

					events.Add(new CalendarEventModel
					{
						Cabinet = cabinet.Cabinet,
						Height = height,
						IndexStart = minIndex,
						Times = eventTimes,
						Width = events.Count + 1

					});
					eventTimes = new List<string>();
					height = 1;
				}
			}
			Events = events;
		}

		public MvxObservableCollection<CalendarEventModel> Events
		{
			get => _events;
			set => SetProperty(ref _events, value);
		}

		public MvxObservableCollection<string> Times
		{
			get => _times;
			set => SetProperty(ref _times, value);
		}

		public override void Prepare(DateTime parameter)
		{
			_parameter = parameter;
			DateTime = parameter;
		}

		public MvxObservableCollection<RowDefinition> Rows
		{
			get => _rows;
			private set => SetProperty(ref _rows, value);
		}
		#endregion
	}

	public class CalendarEventModel
	{
		public Cabinet Cabinet
		{
			get;
			set;
		}

		public int IndexStart
		{
			get;
			set;
		}

		public List<string> Times
		{
			get;
			set;
		}

		public int Height
		{
			get;
			set;
		}

		public int Width
		{
			get;
			set;
		}
	}
}
