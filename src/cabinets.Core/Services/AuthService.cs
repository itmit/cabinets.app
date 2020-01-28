using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using cabinets.Core.Dtos;
using cabinets.Core.Models;
using Newtonsoft.Json;

namespace cabinets.Core.Services
{
	/// <summary>
	/// Представляет 
	/// </summary>
	public class AuthService : IAuthService
	{
		/// <summary>
		/// Адрес для авторизации.
		/// </summary>
		private const string LoginUri = "http://cabinets.itmit-studio.ru/api/login";

		/// <summary>
		/// <see cref="IMapper"/> для преобразования ДТО в модели.
		/// </summary>
		private readonly IMapper _mapper;

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="AuthService" />.
		/// </summary>
		public AuthService()
		{
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<AccessToken, UserDto>();

				cfg.CreateMap<UserDto, User>()
				   .ForPath(m => m.AccessToken.Body, o => o.MapFrom(q => q.Body))
				   .ForPath(m => m.AccessToken.ExpiresAt, o => o.MapFrom(q => q.ExpiresAt))
				   .ForPath(m => m.AccessToken.Type, o => o.MapFrom(q => q.Type))
				   .ForPath(m => m.Birthday, o => o.MapFrom(q => q.Client.Birthday))
				   .ForPath(m => m.Email, o => o.MapFrom(q => q.Client.Email))
				   .ForPath(m => m.Guid, o => o.MapFrom(q => q.Client.Guid))
				   .ForPath(m => m.Name, o => o.MapFrom(q => q.Client.Name))
				   .ForPath(m => m.Phone, o => o.MapFrom(q => q.Client.Phone));
			}));
		}

		/// <summary>
		/// Проводит авторизацию пользователя.
		/// </summary>
		/// <param name="login">Логин пользователя.</param>
		/// <param name="password">Пароль пользователя.</param>
		/// <returns>Авторизованный пользователь.</returns>
		public async Task<User> Login(string login, string password)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var requestBody = JsonConvert.SerializeObject(new AuthDto
				{
					Login = login,
					Password = password
				});

				Debug.WriteLine(requestBody);

				var response = await client.PostAsync(LoginUri, 
													  new StringContent(requestBody, Encoding.UTF8, "application/json"));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (string.IsNullOrEmpty(jsonString))
				{
					return null;
				}

				if (response.IsSuccessStatusCode)
				{
					var jsonData = JsonConvert.DeserializeObject<GeneralDto<UserDto>>(jsonString);
					return await Task.FromResult(_mapper.Map<User>(jsonData.Data));
				}

				var error = JsonConvert.DeserializeObject<ErrorDto>(jsonString);
				Errors = error.Errors;
				Errors["Fatal"] = error.Error;

				return null;
			}
		}

		public Dictionary<string, string> Errors
		{
			get;
			private set;
		} = new Dictionary<string, string>();
	}
}
