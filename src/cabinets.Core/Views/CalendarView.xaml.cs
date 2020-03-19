using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamForms.Controls;

namespace cabinets.Core.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalendarView : ContentView
	{
		#region .ctor
		public CalendarView()
		{
			InitializeComponent();
			Calendar.Culture = CultureInfo.GetCultureInfo("ru-RU");
			Calendar.SelectedDate = Calendar.MinimumDate;
			Calendar.Month = DateTime.Now.Month;
			Calendar.Year = DateTime.Now.Year;
		}
		#endregion
	}
}
