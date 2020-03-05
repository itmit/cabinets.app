using System;
using Newtonsoft.Json;

namespace cabinets.Core.Dtos
{
	public class CabinetDto
	{
		#region Properties
		public float Area
		{
			get;
			set;
		}

		public int Capacity
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		[JsonProperty("photos")]
		public PictureDto[] DetailPictureSources
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Colour
		{
			get;
			set;
		}

		public string Photo
		{
			get;
			set;
		}

		public int? Price
		{
			get;
			set;
		}

		public Guid Uuid
		{
			get;
			set;
		}

		[JsonProperty("price_morning")]
		public int? PriceMorning
		{
			get;
			set;
		}

		[JsonProperty("price_evening")]
		public int? PriceEvening
		{
			get;
			set;
		}
		#endregion
	}
}
