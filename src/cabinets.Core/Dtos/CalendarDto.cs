using System.Collections.Generic;

namespace cabinets.Core.Dtos
{
	public class CalendarDto
	{
		#region Properties
		public CabinetDto Cabinet
		{
			get;
			set;
		}

		public List<string> Times
		{
			get;
			set;
		}
		#endregion
	}
}
