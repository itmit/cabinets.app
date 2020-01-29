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
	[MvxTabbedPagePresentation(TabbedPosition.Root, NoHistory = true)]
	[DesignTimeVisible(false)]
    public partial class MainPage : MvxTabbedPage<MainViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
			On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}
