using System.ComponentModel;
using cabinets.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Application = Xamarin.Forms.Application;

namespace cabinets.Core.Pages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
	[MvxTabbedPagePresentation(TabbedPosition.Root, NoHistory = true, Animated = false)]
	[DesignTimeVisible(false)]
    public partial class MainPage : MvxTabbedPage<MainViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
			On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

		}

		private bool _firstTime = true;

		private static bool _firstSt = true;
		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (_firstSt)
			{
				_firstSt = false;
				ViewModel.NavigateToMainPage();
				return;
			}

			if (_firstTime)
			{
				_firstTime = false;
				ViewModel.ShowInitialViewModelsCommand.ExecuteAsync();
				CurrentPage = Children[ViewModel.FirstPageIndex];
			}
		}
	}
}
