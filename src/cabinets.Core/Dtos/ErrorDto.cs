using System.Collections.Generic;

namespace cabinets.Core.Dtos
{
	public class ErrorDto
	{
		public Dictionary<string, string> Errors
		{
			get;
			set;
		}

		public string Error
		{
			get;
			set;
		}
	}
}
