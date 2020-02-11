using System;
using cabinets.Core.ViewModels.Auth;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxContentPagePresentation(NoHistory = true, Animated = false)]
    public partial class TwoButtonPage : MvxContentPage<TwoButtonViewModel>
    {
        public TwoButtonPage()
        {
            InitializeComponent();
        }
	}
}