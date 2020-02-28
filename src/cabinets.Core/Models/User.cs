using System;

namespace cabinets.Core.Models
{
	public class User
	{
		#region Properties
		public AccessToken AccessToken
		{
			get;
			set;
		}

		public DateTime Birthday
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает электронную почту пользователя
		/// </summary>
		public string Email
		{
			get;
			set;
		}

		public Guid Guid
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает полное имя пользователя
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает номер телефона пользователя
		/// </summary>
		public string Phone
		{
			get;
			set;
		}
		#endregion
	}
}
