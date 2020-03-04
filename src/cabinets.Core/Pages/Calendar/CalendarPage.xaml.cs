﻿using cabinets.Core.ViewModels;
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
	}
}