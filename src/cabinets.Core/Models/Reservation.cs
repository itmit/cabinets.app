using System;

namespace cabinets.Core.Models
{
	public class Reservation
	{
		#region Properties
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

		public DateTime Date
		{
			get;
			set;
		}

		public string[] Times
		{
			get;
			set;
		}

		public Guid Uuid
		{
			get;
			set;
		}
		#endregion
	}
}
