using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using cabinets.Core.Dtos;
using cabinets.Core.Models;
using cabinets.Core.Repositories;
using MvvmCross;
using Newtonsoft.Json;

namespace cabinets.Core.Services
{
	public class NewsService : INewsService
	{
		#region Data
		#region Consts
		private const string DomainUri = "http://cabinets.itmit-studio.ru/";

		private const string GetAllUri = "http://cabinets.itmit-studio.ru/api/news/index";
		private const string GetNewsDetailUri = "http://cabinets.itmit-studio.ru/api/news/show";
		#endregion

		#region Fields
		private readonly Mapper _mapper;
		private readonly AccessToken _token;
		#endregion
		#endregion

		#region .ctor
		public NewsService(IAuthService authService)
		{
			_token = authService.User.AccessToken;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<News, NewsDto>()
				   .ForMember(dto => dto.Head, m => m.MapFrom(news => news.Title))
				   .ForMember(dto => dto.PreviewPicture, m => m.MapFrom(news => news.Image))
				   .ForMember(dto => dto.Body, m => m.MapFrom(news => news.DetailText));

				cfg.CreateMap<NewsDto, News>()
				   .ForMember(news => news.Title, m => m.MapFrom(dto => dto.Head))
				   .ForMember(news => news.DetailPictureSources, m => m.MapFrom(dto => dto.DetailPictureSources))
				   .ForMember(news => news.DetailText, m => m.MapFrom(dto => dto.Body))
				   .ForMember(news => news.Image, m => m.MapFrom(dto => DomainUri + dto.PreviewPicture));
			}));
		}
		#endregion

		#region INewsService members
		public async Task<List<News>> GetAll()
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{_token.Type} {_token.Body}");

				var response = await client.GetAsync(GetAllUri);

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return null;
				}

				var data = JsonConvert.DeserializeObject<GeneralDto<List<NewsDto>>>(json);

				if (data.Success)
				{
					return _mapper.Map<List<News>>(data.Data);
				}

				return null;
			}
		}

		public async Task<News> GetNews(Guid guid)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{_token.Type} {_token.Body}");

				var response = await client.PostAsync(GetNewsDetailUri,
													  new FormUrlEncodedContent(new Dictionary<string, string>
													  {
														  {
															  "uuid", guid.ToString()
														  }
													  }));
				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return null;
				}

				var data = JsonConvert.DeserializeObject<GeneralDto<NewsDto>>(json);

				if (data.Success)
				{
					var pictures = new string[data.Data.DetailPictureSources.Length];
					for (var i = 0; i < data.Data.DetailPictureSources.Length; i++)
					{
						pictures[i] = DomainUri +
									  data.Data.DetailPictureSources[i]
										  .Picture;
					}

					var news = _mapper.Map<News>(data.Data);
					news.DetailPictureSources = pictures;
					return news;
				}

				return null;
			}
		}
		#endregion
	}
}
