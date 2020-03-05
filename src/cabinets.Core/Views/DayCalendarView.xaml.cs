using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cabinets.Core.ViewModels.Calendar;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DayCalendarView : MvxContentView<DayCalendarViewModel>
	{
		public DayCalendarView()
		{
			InitializeComponent();
		}
	}
}