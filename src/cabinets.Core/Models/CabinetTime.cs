using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace cabinets.Core.Models
{
	public class CabinetTime
	{
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
	}
}
