using System;
using cabinets.Core.ViewModels.Auth;
using cabinets.Pages;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : MvxContentPage<RegistrationViewModel>
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

    }
}