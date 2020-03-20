using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalendarView : ContentView
	{
		public Xamarin.Plugin.Calendar.Controls.Calendar Calendar
		{
			get;
			private set;
		}

		#region .ctor
		public CalendarView()
		{
			InitializeComponent();
			Calendar = Cal;
			Calendar.Culture = CultureInfo.GetCultureInfo("ru-RU");
			Calendar.SelectedDate = Calendar.MinimumDate;
			Calendar.Month = DateTime.Now.Month;
			Calendar.Year = DateTime.Now.Year;
		}
		#endregion
	}
}
