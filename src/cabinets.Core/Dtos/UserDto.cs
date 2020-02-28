using System;
using Newtonsoft.Json;

namespace cabinets.Core.Dtos
{
	public class UserDto
	{
		#region Properties
		[JsonProperty("access_token")]
		public string Body
		{
			get;
			set;
		}

		public ClientDto Client
		{
			get;
			set;
		}

		[JsonProperty("expires_at")]
		public DateTime? ExpiresAt
		{
			get;
			set;
		}

		[JsonProperty("token_type")]
		public string Type
		{
			get;
			set;
		}
		#endregion
	}
}
