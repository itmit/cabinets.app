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
using cabinets.Models;
using Newtonsoft.Json;

namespace cabinets.Core.Services
{
	public class NewsService : INewsService
	{
		private readonly AccessToken _token;
		private Mapper _mapper;

		private const string GetAllUri = "http://cabinets.itmit-studio.ru/api/news/index";

		private const string StorageUri = "http://cabinets.itmit-studio.ru/";

		public NewsService(IUserRepository userRepository)
		{
			_token = userRepository.GetAll()
								   .Single().AccessToken; 
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<News, NewsDto>()
				   .ForMember(dto => dto.Head, m => m.MapFrom(news => news.Title))
				   .ForMember(dto => dto.PreviewPicture, m => m.MapFrom(news => news.Image))
				   .ForMember(dto => dto.Body, m => m.MapFrom(news => news.DetailText));

				cfg.CreateMap<NewsDto, News>()
				   .ForMember(news => news.Title, m => m.MapFrom(dto => dto.Head))
				   .ForMember(news => news.DetailText, m => m.MapFrom(dto => dto.Body))
				   .ForMember(news => news.Image, m => m.MapFrom(dto => StorageUri + dto.PreviewPicture));
			}));
		}

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
					Errors = new Dictionary<string, string>()
					{
						{"Fatal", "Нет ответа от сервера"}
					};
					return null;
				}

				var data = JsonConvert.DeserializeObject<GeneralDto<List<NewsDto>>>(json);

				if (data.Success)
				{
					return _mapper.Map<List<News>>(data.Data);
				}

				if (!string.IsNullOrEmpty(data.Error))
				{
					Errors["Fatal"] = data.Error;
				}

				Errors = data.Errors;
				return null;
			}
		}
		public Dictionary<string, string> Errors
		{
			get;
			private set;
		}
		public Task<News> GetNews(Guid guid) => throw new NotImplementedException();
	}
}
