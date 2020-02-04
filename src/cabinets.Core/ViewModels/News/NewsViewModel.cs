using System;
using System.Threading.Tasks;
using cabinets.Core.Models;
using cabinets.Core.Services;
using cabinets.Models;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.News
{
	public class NewsViewModel : MvxNavigationViewModel
	{
		private readonly INewsService _newsService;
		private MvxObservableCollection<Models.News> _news;

		public MvxObservableCollection<Models.News> News
		{
			get => _news;
			set => SetProperty(ref _news, value);
		}

		public NewsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, INewsService newsService)
			: base(logProvider, navigationService)
		{
			_newsService = newsService;
		}

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
