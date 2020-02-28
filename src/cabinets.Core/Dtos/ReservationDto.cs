using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace cabinets.Core.Dtos
{
	public class ReservationDto
	{
		#region Properties
		[JsonProperty("date")]
		public string Date
		{
			get;
			set;
		}

		[JsonProperty("times")]
		public IEnumerable<string> Times
		{
			get;
			set;
		}

		[JsonProperty("uuid")]
		public Guid Uuid
		{
			get;
			set;
		}
		#endregion
	}
}
