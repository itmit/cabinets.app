using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.News
{
	public class NewsDetailViewModel : MvxViewModel<Models.News>
	{
		private Models.News _news;

		public override void Prepare(Models.News parameter)
		{
			News = parameter;
		}

		public Models.News News
		{
			get => _news;
			private set => SetProperty(ref _news, value);
		}
	}
}
