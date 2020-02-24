using System.Collections.Generic;

namespace cabinets.Core.Models
{
	public class CalendarDay
	{
		public Cabinet Cabinet
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
