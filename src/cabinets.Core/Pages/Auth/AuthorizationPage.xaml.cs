using System;
using cabinets.Core.ViewModels;
using cabinets.Core.ViewModels.Auth;
using cabinets.Pages;
using cabinets.Views;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationPage : MvxContentPage<AuthorizationViewModel>
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }
    }
}