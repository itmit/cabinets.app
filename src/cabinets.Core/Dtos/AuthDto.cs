using Newtonsoft.Json;

namespace cabinets.Core.Dtos
{
	public class AuthDto
	{
		#region Properties
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
		#endregion
	}
}
