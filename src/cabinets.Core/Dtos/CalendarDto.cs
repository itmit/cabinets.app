using System.Collections.Generic;
using cabinets.Core.Models;

namespace cabinets.Core.Dtos
{
	public class CalendarDto
	{
		public CabinetDto Cabinet
		{
			get;
			set;
		}

		public IEnumerable<string> Times
		{
			get;
			set;
		}

	}
}
