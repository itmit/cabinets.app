using cabinets.Core.ViewModels.News;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.News
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsDetailPage : MvxContentPage<NewsDetailViewModel>
	{
		#region .ctor
		public NewsDetailPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
