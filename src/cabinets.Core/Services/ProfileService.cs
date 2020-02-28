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
using Newtonsoft.Json;

namespace cabinets.Core.Services
{
	public class ProfileService : IProfileService
	{
		#region Data
		#region Consts
		private const string GetAmountUri = "http://cabinets.itmit-studio.ru/api/user/getAmount";

		private const string GetReservationDetailUri = "http://cabinets.itmit-studio.ru/api/user/myReservations/detail";

		private const string GetReservationsUri = "http://cabinets.itmit-studio.ru/api/user/myReservations";
		#endregion

		#region Fields
		private Mapper _mapper;
		private readonly AccessToken _token;
		#endregion
		#endregion

		#region .ctor
		public ProfileService(IUserRepository userRepository)
		{
			_token = userRepository.GetAll()
								   .Single()
								   .AccessToken;

			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				// cfg.CreateMap<Cabinet, CabinetDto>();
				// cfg.CreateMap<CabinetDto, Cabinet>()
				//.ForMember(cab => cab.PhotoSource, m => m.MapFrom(dto => DomainUri + dto.Photo));
			}));
		}
		#endregion

		#region IProfileService members
		public async Task<int> GetAmount()
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{_token.Type} {_token.Body}");

				var response = await client.GetAsync(GetAmountUri);

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return 0;
				}

				var data = JsonConvert.DeserializeObject<GeneralDto<AmountDto>>(json);

				return data.Data.Amount ?? 0;
			}
		}

		public async Task<Reservation> GetReservationDetail(Guid uuid)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{_token.Type} {_token.Body}");

				var response = await client.PostAsync(GetReservationDetailUri,
													  new FormUrlEncodedContent(new Dictionary<string, string>
													  {
														  {
															  "uuid", uuid.ToString()
														  }
													  }));

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return null;
				}

				var data = JsonConvert.DeserializeObject<GeneralDto<Reservation>>(json);

				if (data.Success)
				{
					return data.Data;
				}

				return null;
			}
		}

		public async Task<List<Reservation>> GetReservations()
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{_token.Type} {_token.Body}");

				var response = await client.GetAsync(GetReservationsUri);

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return null;
				}

				var data = JsonConvert.DeserializeObject<GeneralDto<List<Reservation>>>(json);

				if (data.Success)
				{
					return data.Data;
				}

				return null;
			}
		}
		#endregion
	}
}
