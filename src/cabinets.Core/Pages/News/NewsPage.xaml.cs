using cabinets.Core.ViewModels.News;
using cabinets.Pages;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.News
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(NoHistory = true, Animated = false)]
	public partial class NewsPage : MvxContentPage<NewsViewModel>
	{
        public NewsPage()
        {
            InitializeComponent();
		}

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			((ListView) sender).SelectedItem = null;
		}
    }
}