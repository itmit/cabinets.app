using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cabinets.Core.Models;
using cabinets.Core.Services;
using cabinets.Core.ViewModels.Cabinets;
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
		private MvxObservableCollection<string> _times;
		private MvxObservableCollection<CalendarEventModel> _events;
		private double _calendarWidth;
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
			for (int i = 7; i < 24; i++)
			{
				times.Add($"{i}:00");
				times.Add($"{i}:30");
			}
			Times = times;

			try
			{
				Cabinets = new MvxObservableCollection<CalendarDay>(await _cabinetService.GetBusyCabinetsByDate(_parameter));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			var cabinetGroups = Cabinets.GroupBy(day => day.Cabinet.Uuid);
			var cabs = new MvxObservableCollection<CalendarDay>();
			foreach (var cab in cabinetGroups)
			{
				var a = cab.SelectMany(c => c.Times).ToList();
				var b = cab.First().Cabinet;
				cabs.Add(new CalendarDay()
				{
					Times = a,
					Cabinet = b
				});
			}

			var events = new MvxObservableCollection<CalendarEventModel>();

			foreach (var cabinet in cabs)
			{
				var cabTimes = new List<string>();
				foreach (var time in cabinet.Times)
				{
					cabTimes.Add(time.Split('-')[0]);
				}
				var intList = new List<int>();
				foreach (var cabTime in cabTimes)
				{
					intList.Add(Times.IndexOf(cabTime));
				}

				var indexes = intList.OrderBy(i => i).ToList();
				var eventTimes = new List<string>
				{
					Times[indexes[0]]
				};

				if (indexes.Count == 1)
				{
					events.Add(new CalendarEventModel
					{
						Cabinet = cabinet.Cabinet,
						Height = 1,
						IndexStart = indexes[0],
						Times = eventTimes,
						LeftMargin = 0
					});
					continue;
				}

				int minIndex = indexes[0];
				int height = 1;
				for (int i = 1; i < indexes.Count; i++)
				{
					if (indexes[i] == indexes[i - 1] + 1)
					{
						eventTimes.Add(Times[indexes[i]]);
						height++;
						if (i != indexes.Count - 1)
						{
							continue;
						}
					}

					events.Add(new CalendarEventModel
					{
						Cabinet = cabinet.Cabinet,
						Height = height,
						IndexStart = minIndex,
						Times = eventTimes
					});
					minIndex = indexes[i];
					eventTimes = new List<string>();
					height = 1;
				}
			}

			int index = 0;
			events = new MvxObservableCollection<CalendarEventModel>(events.OrderBy(e => e.IndexStart));
			foreach (var model in events)
			{
				model.LeftMargin = events.Count(e => model.IndexStart > e.IndexStart && model.IndexStart < e.IndexStart + e.Height) * 20;

				var c = events.Count(e => e.IndexStart == model.IndexStart);
				if (c > 0)
				{
					model.Width = CalendarWidth / events.Count(e => e.IndexStart == model.IndexStart);
					model.LeftMargin += model.Width * index; 
					index++;
					continue;
				}

				index = 0;
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

		public double CalendarWidth
		{
			get => _calendarWidth;
			set 
			{
				_calendarWidth = value;

				if (Events != null)
				{
					int index = 0;
					var events = Events;
					foreach (var model in events)
					{
						model.LeftMargin = events.Count(e => model.IndexStart > e.IndexStart && model.IndexStart < e.IndexStart + e.Height) * 20;

						var c = events.Count(e => e.IndexStart == model.IndexStart);
						if (c > 1)
						{
							model.Width = value / c;
							model.LeftMargin += model.Width * index;
							index++;
							continue;
						}

						index = 0;
					}

					Events = events;
				}
			}
		}

		public override void Prepare(DateTime parameter)
		{
			_parameter = parameter;
			DateTime = parameter;
		}
		#endregion
	}

	public class CalendarEventModel : MvxViewModel
	{
		private double _leftMargin;

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

		public double Width
		{
			get;
			set;
		}

		public double LeftMargin
		{
			get => _leftMargin;
			set => SetProperty(ref _leftMargin, value);
		}
	}
}
