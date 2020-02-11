using System;
using Realms;

namespace cabinets.Core.RealmObjects
{
	public class UserRealmObject : RealmObject
	{
		[PrimaryKey]
		public string Guid
		{
			get;
			set;
		} = System.Guid.NewGuid()
				.ToString();

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

		/// <summary>
		/// Возвращает электронную почту пользователя
		/// </summary>
		public string Email
		{
			get;
			set;
		}


		public DateTimeOffset Birthday
		{
			get;
			set;
		}

		public AccessTokenRealmObject AccessToken
		{
			get;
			set;
		}
	}
}
