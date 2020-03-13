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
	public class ProfileService : IProfileService
	{
		private readonly IAuthService _authService;

		#region Data
		#region Consts
		private const string GetAmountUri = "http://lk.arendapsy.ru/api/user/getAmount";

		private const string GetReservationDetailUri = "http://lk.arendapsy.ru/api/user/myReservations/detail";

		private const string GetReservationsUri = "http://lk.arendapsy.ru/api/user/myReservations";

		private const string CancelReservationUri = "http://lk.arendapsy.ru/api/cabinets/cancelReservation";
		#endregion
		#endregion

		#region .ctor
		public ProfileService(IAuthService authService)
		{
			_authService = authService;
		}
		#endregion

		#region IProfileService members
		public async Task<int> GetAmount()
		{
			using (var client = new HttpClient())
			{
				var token = _authService.User.AccessToken;
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{token.Type} {token.Body}");

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
				var token = _authService.User.AccessToken;
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{token.Type} {token.Body}");

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
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{_authService.User.AccessToken.Type} {_authService.User.AccessToken.Body}");

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

		public async Task<bool> CancelReservation(Guid uuid)
		{
			using (var client = new HttpClient())
			{
				var token = _authService.User.AccessToken;
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{token.Type} {token.Body}");

				var response = await client.PostAsync(CancelReservationUri, new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{"uuid", uuid.ToString()}
				}));

				var json = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(json);

				if (string.IsNullOrEmpty(json))
				{
					return false;
				}

				var data = JsonConvert.DeserializeObject<GeneralDto<List<Reservation>>>(json);

				return data.Success;
			}
		}
		#endregion
	}
}
