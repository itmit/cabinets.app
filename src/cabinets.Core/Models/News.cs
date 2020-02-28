using System;

namespace cabinets.Core.Models
{
	public class News
	{
		#region Properties
		/// <summary>
		/// Возвращает дату новости
		/// </summary>
		public string Date
		{
			get;
			set;
		}

		public string[] DetailPictureSources
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает детальный текст новостей
		/// </summary>
		public string DetailText
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает картинку новости
		/// </summary>
		public string Image
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает заголовок новости
		/// </summary>
		public string Title
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
