using System;
using Newtonsoft.Json;

namespace cabinets.Core.Dtos
{
	public class ClientDto
	{
		#region Properties
		public DateTime? Birthday
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}

		[JsonProperty("uuid")]
		public Guid Guid
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Phone
		{
			get;
			set;
		}
		#endregion
	}
}
