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

		public List<string> Times
		{
			get;
			set;
		}
	}
}
