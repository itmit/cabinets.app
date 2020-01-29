using cabinets.Models;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.News
{
	public class NewsViewModel : MvxViewModel
	{
		public MvxObservableCollection<NewsModel> News
		{
			get;
			set;
		}
	}
}
