using System;

namespace cabinets.Core.Dtos
{
	public class CabinetDto
	{
		public Guid Uuid
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public int Capacity
		{
			get;
			set;
		}

		public float Area
		{
			get;
			set;
		}

		public string Photo
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public int Price
		{
			get;
			set;
		}
	}
}
