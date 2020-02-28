using Newtonsoft.Json;

namespace cabinets.Core.Models
{
	public class CabinetTime
	{
		#region Properties
		[JsonProperty("key")]
		public int Key
		{
			get;
			set;
		}

		[JsonProperty("value")]
		public string Value
		{
			get;
			set;
		}
		#endregion
	}
}
