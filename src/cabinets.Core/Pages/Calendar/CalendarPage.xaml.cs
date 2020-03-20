using System;
using System.Globalization;
using cabinets.Core.ViewModels;
using cabinets.Core.ViewModels.Calendar;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Calendar
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(NoHistory = true, Animated = false)]
	public partial class CalendarPage : MvxContentPage<CalendarViewModel>
	{
		#region .ctor
		public CalendarPage()
		{
			InitializeComponent();
		}
		#endregion

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			base.OnAppearing();

			CalendarView.Calendar.Culture = CultureInfo.GetCultureInfo("ru-RU");
			CalendarView.Calendar.SelectedDate = CalendarView.Calendar.MinimumDate;
			CalendarView.Calendar.Month = DateTime.Now.Month;
			CalendarView.Calendar.Year = DateTime.Now.Year;
		}
	}
}
