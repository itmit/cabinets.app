using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cabinets.Core.Models;

namespace cabinets.Core.Services
{
	public interface ICabinetsService
	{
		#region Properties
		Dictionary<string, string> Errors
		{
			get;
		}
		#endregion

		/// <summary>
		/// Происходит после обновлений списка избранных.
		/// </summary>
		event EventHandler MakeReservationSuccesed;

		#region Overridable
		Task<CabinetTime[]> CheckCabinetByDate(Cabinet cabinet, DateTime date);

		Task<List<Cabinet>> GetAll();

		Task<IEnumerable<CalendarDay>> GetBusyCabinetsByDate(DateTime date);

		Task<Cabinet> GetCabinet(Guid guid);

		Task<Cabinet> GetCabinetDetail(Guid guid);

		Task<bool> MakeReservation(Cabinet cabinet, DateTime date, IEnumerable<CabinetTime> times);
		#endregion
	}
}
