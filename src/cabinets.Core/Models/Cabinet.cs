using System;

namespace cabinets.Core.Models
{
    public class Cabinet
    {
        /// <summary>
        /// Возвращает номер кабинета
        /// </summary>
        public int Number 
        { 
            get; 
            set; 
        }

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

		/// <summary>
		/// Возвращает цвет флажка
		/// </summary>
		public string BoxColor 
        { 
            get; 
            set; 
        }

		public string PhotoSource
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
    }
}
