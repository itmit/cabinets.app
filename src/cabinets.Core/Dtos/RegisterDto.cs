using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace cabinets.Core.Dtos
{
	public class RegisterDto
	{
		[JsonProperty("email")]
		public string Email
		{
			get;
			set;
		}
		
		[JsonProperty("name")]
		public string Name
		{
			get;
			set;
		}

		[JsonProperty("phone")]
		public string Phone
		{
			get;
			set;
		}

		[JsonProperty("birthday")]
		public string Birthday
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

		[JsonProperty("password_confirmation")]
		public string PasswordConfirm
		{
			get;
			set;
		}
	}
}
