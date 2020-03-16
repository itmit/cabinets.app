using System;
using System.Threading.Tasks;
using cabinets.Core.Services;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.News
{
	public class NewsViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		private readonly IMvxNavigationService _navigationService;
		private MvxObservableCollection<Models.News> _news;
		private readonly INewsService _newsService;
		private Models.News _selectedNews;
		#endregion
		#endregion

		#region .ctor
		public NewsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, INewsService newsService)
			: base(logProvider, navigationService)
		{
			_navigationService = navigationService;
			_newsService = newsService;
		}
		#endregion

		#region Properties
		public MvxObservableCollection<Models.News> News
		{
			get => _news;
			private set => SetProperty(ref _news, value);
		}

		public Models.News SelectedNews
		{
			get => _selectedNews;
			set
			{
				SetProperty(ref _selectedNews, value);
				OpenDetailPage(value);
			}
		}
		#endregion

		#region Overrided
		public override async Task Initialize()
		{
			await base.Initialize();

			await Task.Run(async () =>
			{
				try
				{
					News = new MvxObservableCollection<Models.News>(await _newsService.GetAll());
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			});
		}
		#endregion

		#region Private
		private async void OpenDetailPage(Models.News news)
		{
			try
			{
				var temp = news.Uuid.ToString();
				var detailNews = await _newsService.GetNews(news.Uuid);
				await _navigationService.Navigate<NewsDetailViewModel, Models.News>(detailNews);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		#endregion
	}
}
