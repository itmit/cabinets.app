using System;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimeViewCell : ViewCell
	{
		#region .ctor
		public TimeViewCell()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			if (sender is Frame frame)
			{
				frame.BackgroundColor = Color.FromHex("#31516D");
			}
		}
		#endregion
	}
}
