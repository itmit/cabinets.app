using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.News
{
	public class NewsDetailViewModel : MvxViewModel<Models.News>
	{
		#region Data
		#region Fields
		private Models.News _news;
		#endregion
		#endregion

		#region Properties
		public Models.News News
		{
			get => _news;
			private set => SetProperty(ref _news, value);
		}
		#endregion

		#region Overrided
		public override void Prepare(Models.News parameter)
		{
			News = parameter;
		}
		#endregion
	}
}
