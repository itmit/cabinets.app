using System;
using Newtonsoft.Json;

namespace cabinets.Core.Models
{
	public class Reservation
	{
		#region Properties
		[JsonProperty("amount")]
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

		[JsonProperty("is_paid")]
		public bool IsPaid
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
