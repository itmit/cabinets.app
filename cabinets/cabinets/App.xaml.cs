using cabinets.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets
{
    public partial class App : Application
    {
        public App() 
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AuthorizationPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
