using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cabinets.Core.Models;

namespace cabinets.Core.Services
{
	public interface IProfileService
	{
		#region Overridable
		Task<int> GetAmount();

		Task<Reservation> GetReservationDetail(Guid uuid);

		Task<List<Reservation>> GetReservations();
		#endregion
	}
}
