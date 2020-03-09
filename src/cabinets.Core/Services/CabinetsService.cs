using System;
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
	public class CabinetsService : ICabinetsService
	{
		#region Data
		#region Consts
		private const string CheckCabinetByDateUri = "http://cabinets.itmit-studio.ru/api/cabinets/selectDate";
		private const string DomainUri = "http://cabinets.itmit-studio.ru/";
		private const string GetAllUri = "http://cabinets.itmit-studio.ru/api/cabinets/index";
		private const string GetBusyCabinetsByDateUri = "http://cabinets.itmit-studio.ru/api/cabinets/getBusyCabinetsByDate";
		private const string GetCabinetDetailUri = "http://cabinets.itmit-studio.ru/api/cabinets/show";
		private const string MakeReservationUri = "http://cabinets.itmit-studio.ru/api/cabinets/makeReservation";
		#endregion

		#region Fields
		private readonly Mapper _mapper;
		private IAuthService _authService;
		#endregion
		#endregion

		#region .ctor
		public CabinetsService(IAuthService authService)
		{
			_authService = authService;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Cabinet, CabinetDto>();

				cfg.CreateMap<CabinetDto, Cabinet>()
				   .ForMember(cab => cab.PriceMorning, m => m.MapFrom(dto => dto.PriceMorning ?? 0))
				   .ForMember(cab => cab.PriceEvening, m => m.MapFrom(dto => dto.PriceEvening ?? 0))
				   .ForMember(cab => cab.Price, m => m.MapFrom(dto => dto.Price ?? 0))
				   .ForMember(cab => cab.PhotoSource, m => m.MapFrom(dto => DomainUri + dto.Photo));

				cfg.CreateMap<CalendarDto, CalendarDay>()
				   .ForMember(day => day.Times, m => m.MapFrom(dto => dto.Times))
				   .ForMember(day => day.Cabinet, m => m.MapFrom(dto => dto.Cabinet));
			}));
		}
		#endregion

		#region ICabinetsService members
		public async Task<CabinetTime[]> CheckCabinetByDate(Cabinet cabinet, DateTime date)
		{
			using (var client = new HttpClient())
			{
				var token = _authService.User.AccessToken;
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{token.Type} {token.Body}");

				var response = await client.PostAsync(CheckCabinetByDateUri,
													  new FormUrlEncodedContent(new Dictionary<string, string>
													  {
														  {
															  "uuid", cabinet.Uuid.ToString()
														  },
														  {
															  "date", date.ToString("yyyy-MM-dd")
														  }
													  }));

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return null;
				}

				var data = JsonConvert.DeserializeObject<GeneralDto<CabinetTime[]>>(json);

				if (data.Success)
				{
					return data.Data;
				}

				return null;
			}
		}

		public Dictionary<string, string> Errors
		{
			get;
		} = new Dictionary<string, string>();

		/// <summary>
		/// Происходит после обновлений списка избранных.
		/// </summary>
		public event EventHandler MakeReservationSuccesed;

		public async Task<List<Cabinet>> GetAll()
		{
			using (var client = new HttpClient())
			{
				var token = _authService.User.AccessToken;
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{token.Type} {token.Body}");

				var response = await client.GetAsync(GetAllUri);

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return null;
				}

				var data = JsonConvert.DeserializeObject<GeneralDto<List<CabinetDto>>>(json);

				if (data.Success)
				{
					return _mapper.Map<List<Cabinet>>(data.Data);
				}

				return null;
			}
		}

		public async Task<IEnumerable<CalendarDay>> GetBusyCabinetsByDate(DateTime date)
		{
			using (var client = new HttpClient())
			{
				var token = _authService.User.AccessToken;
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{token.Type} {token.Body}");

				var response = await client.PostAsync(GetBusyCabinetsByDateUri,
													  new FormUrlEncodedContent(new Dictionary<string, string>
													  {
														  {
															  "date", date.ToString("yyyy-MM-dd")
														  }
													  }));

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return null;
				}

				var data = JsonConvert.DeserializeObject<GeneralDto<CalendarDto[]>>(json);

				if (data.Success)
				{
					return _mapper.Map<CalendarDay[]>(data.Data);
				}

				return null;
			}
		}

		public Task<Cabinet> GetCabinet(Guid guid) => throw new NotImplementedException();

		public async Task<Cabinet> GetCabinetDetail(Guid guid)
		{
			using (var client = new HttpClient())
			{
				var token = _authService.User.AccessToken;
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{token.Type} {token.Body}");

				var response = await client.PostAsync(GetCabinetDetailUri,
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

				var data = JsonConvert.DeserializeObject<GeneralDto<CabinetDto>>(json);

				if (data.Success)
				{
					var pictures = new string[data.Data.DetailPictureSources.Length];
					for (var i = 0; i < data.Data.DetailPictureSources.Length; i++)
					{
						pictures[i] = DomainUri +
									  data.Data.DetailPictureSources[i]
										  .Photo;
					}

					var cabinet = _mapper.Map<Cabinet>(data.Data);
					cabinet.DetailPictureSources = pictures;
					return cabinet;
				}

				return null;
			}
		}

		public async Task<bool> MakeReservation(Cabinet cabinet, DateTime date, IEnumerable<CabinetTime> times)
		{
			using (var client = new HttpClient())
			{
				var token = _authService.User.AccessToken;
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{token.Type} {token.Body}");

				var cabTimes = new List<string>();
				foreach (var time in times)
				{
					cabTimes.Add(time.Value);
				}

				var request = JsonConvert.SerializeObject(new ReservationDto
				{
					Date = date.ToString("yyyy-MM-dd"),
					Uuid = cabinet.Uuid,
					Times = cabTimes
				});

				var response = await client.PostAsync(MakeReservationUri, new StringContent(request, Encoding.UTF8, "application/json"));

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return false;
				}

				var data = JsonConvert.DeserializeObject<GeneralDto<List<CabinetDto>>>(json);

				if (data.Success)
				{
					MakeReservationSuccesed?.Invoke(this, EventArgs.Empty);
					return true;
				}

				Errors.Add("Fatal", data.Error);
				return false;
			}
		}
		#endregion
	}
}
