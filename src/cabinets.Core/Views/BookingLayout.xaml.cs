using MvvmCross.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BookingLayout : StackLayout
	{
		#region .ctor
		public BookingLayout()
		{
			InitializeComponent();
		}
		#endregion
	}
}
