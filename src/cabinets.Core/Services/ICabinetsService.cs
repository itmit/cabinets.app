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
	}
}
