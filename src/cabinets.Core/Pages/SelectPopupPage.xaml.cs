using System;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace cabinets.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectPopupPage : PopupPage
	{
		#region .ctor
		public SelectPopupPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void Button_Clicked(object sender, EventArgs e)
		{
			Navigation.PopPopupAsync();
		}

		private void Button_Clicked_1(object sender, EventArgs e)
		{
			Navigation.PopPopupAsync();
		}
		#endregion
	}
}
