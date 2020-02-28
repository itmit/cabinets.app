using System;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace cabinets.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SavePopupPage : PopupPage
	{
		#region .ctor
		public SavePopupPage()
		{
			InitializeComponent();
		}
		#endregion

		#region Private
		private void Button_Clicked(object sender, EventArgs e)
		{
			Navigation.PopPopupAsync();
		}
		#endregion
	}
}
