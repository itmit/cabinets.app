using System;

namespace cabinets.Core.Models
{
	public class Cabinet
	{
		#region Properties
		public float Area
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает цвет флажка
		/// </summary>
		public string Colour
		{
			get;
			set;
		}

		public int Capacity
		{
			get;
			set;
		}


		public int PriceMorning
		{
			get;
			set;
		}

		public int PriceEvening
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает дату
		/// </summary>
		public string Date
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string[] DetailPictureSources
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string PhotoSource
		{
			get;
			set;
		}

		public int Price
		{
			get;
			set;
		}

		public Guid Uuid
		{
			get;
			set;
		}
		#endregion
	}
}
