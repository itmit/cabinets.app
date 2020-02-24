using System;

namespace cabinets.Core.Models
{
	public class Reservation
	{
		public Guid Uuid
		{
			get;
			set;
		}

		public DateTime Date
		{
			get;
			set;
		}

		public int? Amount 
		{ 
			get; 
			set;
		}

		public Cabinet Cabinet
		{
			get;
			set;
		}

		public string[] Times
		{
			get;
			set;
		}
	}
}
