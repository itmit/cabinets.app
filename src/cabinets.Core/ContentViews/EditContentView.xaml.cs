using cabinets.Pages;
using Rg.Plugins.Popup.Extensions;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditContentView : ContentView
    {
        public EditContentView()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var popupPage = new SavePopupPage();
            Navigation.PushPopupAsync(popupPage);
        }
    }
}