using System.Collections.Generic;

namespace cabinets.Core.Models
{
	public class CalendarDay
	{
		#region Properties
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
		#endregion
	}
}
