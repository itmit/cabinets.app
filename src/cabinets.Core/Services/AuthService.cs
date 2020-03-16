using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using cabinets.Core.Dtos;
using cabinets.Core.Models;
using cabinets.Core.Repositories;
using MvvmCross;
using Newtonsoft.Json;

namespace cabinets.Core.Services
{
	/// <summary>
	/// Представляет сервис для авторизации пользователей.
	/// </summary>
	public class AuthService : IAuthService
	{
		#region Data
		#region Consts
		/// <summary>
		/// Адрес для авторизации.
		/// </summary>
		private const string LoginUri = "http://lk.arendapsy.ru/api/login";

		private const string LogoutUri = "http://lk.arendapsy.ru/api/logout";

		private const string RegistrationUri = "http://lk.arendapsy.ru/api/register";

		private const string SendDeviceTokenUri = "http://lk.arendapsy.ru/api/user/updateDeviceToken";
		#endregion

		#region Fields
		/// <summary>
		/// <see cref="IMapper" /> для преобразования ДТО в модели.
		/// </summary>
		private readonly IMapper _mapper;
		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="AuthService" />.
		/// </summary>
		public AuthService(IUserRepository userRepository)
		{
			User = userRepository.GetAll().SingleOrDefault();

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
		#endregion

		#region IAuthService members
		public Dictionary<string, string[]> Errors
		{
			get;
			private set;
		} = new Dictionary<string, string[]>();

		public User User
		{
			get;
			private set;
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

				var response = await client.PostAsync(LoginUri, new StringContent(requestBody, Encoding.UTF8, "application/json"));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (string.IsNullOrEmpty(jsonString))
				{
					return null;
				}

				var jsonData = JsonConvert.DeserializeObject<GeneralDto<UserDto>>(jsonString);

				if (jsonData.Success)
				{
					User = await Task.FromResult(_mapper.Map<User>(jsonData.Data));
					return User;
				}

				Errors = jsonData.Errors;
				if (!string.IsNullOrEmpty(jsonData.Error))
				{
					Errors.Add("Fatal", new[] { jsonData.Error });
				}

				return null;
			}
		}

		public async void Logout(User user = null)
		{
			user = user ?? User;

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{user.AccessToken.Type} {user.AccessToken.Body}");

				var response = await client.PostAsync(LogoutUri, null);

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (response.IsSuccessStatusCode)
				{
					User = null;
				}
			}
		}

		public async Task<User> Registration(User user, string password, string confirmPassword)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var requestBody = JsonConvert.SerializeObject(new RegisterDto
				{
					Email = user.Email,
					Name = user.Name,
					Birthday = user.Birthday.ToString("yyyy-MM-dd"),
					Password = password,
					PasswordConfirm = confirmPassword,
					Phone = user.Phone
				});

				Debug.WriteLine(requestBody);

				var response = await client.PostAsync(RegistrationUri, new StringContent(requestBody, Encoding.UTF8, "application/json"));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				var jsonData = JsonConvert.DeserializeObject<GeneralDto<UserDto>>(jsonString);

				if (jsonData.Success)
				{
					User = await Task.FromResult(_mapper.Map<User>(jsonData.Data));
					return User;
				}

				Errors = jsonData.Errors;
				if (!string.IsNullOrEmpty(jsonData.Error))
				{
					Errors.Add("Fatal", new[] { jsonData.Error });
				}

				return null;
			}
		}

		public async Task<bool> SendDeviceToken(string token)
		{
			if (User?.AccessToken == null)
			{
				return false;
			}

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{User.AccessToken.Type} {User.AccessToken.Body}");

				var response = await client.PostAsync(SendDeviceTokenUri, new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{"device_token", token}
				}));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				return response.IsSuccessStatusCode;
			}
		}
		#endregion
	}
}
