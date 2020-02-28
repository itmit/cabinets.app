using System.ComponentModel;
using cabinets.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace cabinets.Core.Pages
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[MvxTabbedPagePresentation(TabbedPosition.Root, NoHistory = true, Animated = false)]
	[DesignTimeVisible(false)]
	public partial class MainPage : MvxTabbedPage<MainViewModel>
	{
		#region Data
		#region Static
		private static bool _firstSt = true;
		#endregion

		#region Fields
		private bool _firstTime = true;
		#endregion
		#endregion

		#region .ctor
		public MainPage()
		{
			InitializeComponent();
			On<Android>()
				.SetToolbarPlacement(ToolbarPlacement.Bottom);
		}
		#endregion

		#region Overrided
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
		#endregion
	}
}
