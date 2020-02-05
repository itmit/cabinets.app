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
	public class CabinetsService : ICabinetsService
	{
		private readonly AccessToken _token;
		private readonly Mapper _mapper;
		private const string DomainUri = "http://cabinets.itmit-studio.ru/";

		public CabinetsService(IUserRepository userRepository)
		{
			_token = userRepository.GetAll()
								   .Single().AccessToken;

			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Cabinet, CabinetDto>();
				cfg.CreateMap<CabinetDto, Cabinet>()
				   .ForMember(cab => cab.PhotoSource, m=> m.MapFrom(dto => DomainUri + dto.Photo));
			}));
		}

		private const string GetAllUri = "http://cabinets.itmit-studio.ru/api/cabinets/index";

		public async Task<List<Cabinet>> GetAll()
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

				var data = JsonConvert.DeserializeObject<GeneralDto<List<CabinetDto>>>(json);

				if (data.Success)
				{
					return _mapper.Map<List<Cabinet>>(data.Data);
				}

				return null;
			}
		}

		public Task<Cabinet> GetCabinet(Guid guid) => throw new NotImplementedException();
	}
}
