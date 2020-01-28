using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace cabinets.Core.Dtos
{
	public class AuthDto
	{
		[JsonProperty("phone")]
		public string Login
		{
			get;
			set;
		}

		[JsonProperty("password")]
		public string Password
		{
			get;
			set;
		}
	}
}
