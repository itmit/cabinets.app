using System;
using System.Threading.Tasks;
using cabinets.Core.Models;
using cabinets.Core.Services;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.News
{
	public class NewsViewModel : MvxNavigationViewModel
	{
		private readonly INewsService _newsService;
		private MvxObservableCollection<Models.News> _news;
		private Models.News _selectedNews;

		public MvxObservableCollection<Models.News> News
		{
			get => _news;
			private set => SetProperty(ref _news, value);
		}

		public NewsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, INewsService newsService)
			: base(logProvider, navigationService)
		{
			_navigationService = navigationService;
			_newsService = newsService;
		}

		public Models.News SelectedNews
		{
			get => _selectedNews;
			set
			{
				if (value == null)
				{
					return;
				}

				if (SetProperty(ref _selectedNews, value))
				{
					OpenDetailPage(value);
				}
			}
		}

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

		private readonly IMvxNavigationService _navigationService;

		public override async Task Initialize()
		{
			await base.Initialize();

			try
			{
				News = new MvxObservableCollection<Models.News>(await _newsService.GetAll());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
