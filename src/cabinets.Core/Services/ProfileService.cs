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
		private readonly AccessToken _token;
		private Mapper _mapper;

		private const string GetReservationsUri = "http://cabinets.itmit-studio.ru/api/user/myReservations";

		public ProfileService(IUserRepository userRepository)
		{
			_token = userRepository.GetAll()
								   .Single().AccessToken;

			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				// cfg.CreateMap<Cabinet, CabinetDto>();
				// cfg.CreateMap<CabinetDto, Cabinet>()
				   //.ForMember(cab => cab.PhotoSource, m => m.MapFrom(dto => DomainUri + dto.Photo));
			}));
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
					return _mapper.Map<List<Reservation>>(data.Data);
				}

				return null;
			}
		}

		public Task<Reservation> GetReservationDetail() => throw new System.NotImplementedException();
	}
}
