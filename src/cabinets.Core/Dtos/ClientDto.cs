using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace cabinets.Core.Dtos
{
	public class ClientDto
	{
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

		public DateTime? Birthday
		{
			get;
			set;
		}

		public string Phone
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}
	}
}
