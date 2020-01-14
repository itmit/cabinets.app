using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private async void Button_Clicked(object sender, EventArgs e)
        {
           
           await Application.Current.MainPage.DisplayAlert(" ", "Внесенные изменения успешно сохранены", "В ПРОФИЛЬ");
        }
    }
}