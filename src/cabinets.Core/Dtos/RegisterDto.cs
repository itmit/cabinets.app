using Newtonsoft.Json;

namespace cabinets.Core.Dtos
{
	public class RegisterDto
	{
		#region Properties
		[JsonProperty("birthday")]
		public string Birthday
		{
			get;
			set;
		}

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

		[JsonProperty("phone")]
		public string Phone
		{
			get;
			set;
		}
		#endregion
	}
}
