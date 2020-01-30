using cabinets.Models;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.News
{
	public class NewsViewModel : MvxNavigationViewModel
	{
		public MvxObservableCollection<NewsModel> News
		{
			get;
			set;
		}

		public NewsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}
	}
}
