using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cabinets.Core.Models;

namespace cabinets.Core.Services
{
	public interface ICabinetsService
	{
		Task<List<Cabinet>> GetAll();

		Task<Cabinet> GetCabinet(Guid guid);

		Task<Cabinet> GetCabinetDetail(Guid guid);

		Task<bool> MakeReservation(Cabinet cabinet, DateTime date, IEnumerable<string> times);

		Dictionary<string, string> Errors
		{
			get;
		}

		Task<string[]> CheckCabinetByDate(Cabinet cabinet, DateTime date);
	}
}
