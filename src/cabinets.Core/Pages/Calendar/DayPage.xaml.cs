using System;
using cabinets.Core.ViewModels;
using cabinets.Core.ViewModels.Calendar;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Calendar
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DayPage : MvxContentPage<DayViewModel>
	{
		#region .ctor
		public DayPage()
		{
			InitializeComponent();
		}
		#endregion

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			base.OnAppearing();

			ViewModel.CalendarWidth = Grid.Width;
		}
	}
}
