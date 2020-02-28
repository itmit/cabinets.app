using System;
using cabinets.Pages;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.ContentViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditContentView : ContentView
	{
		#region .ctor
		public EditContentView()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void Button_Clicked(object sender, EventArgs e)
		{
			var popupPage = new SavePopupPage();
			Navigation.PushPopupAsync(popupPage);
		}
		#endregion
	}
}
