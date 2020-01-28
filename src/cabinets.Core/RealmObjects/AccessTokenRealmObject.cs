using System;
using Realms;

namespace cabinets.Core.RealmObjects
{
	public class AccessTokenRealmObject : RealmObject
	{
		/// <summary>
		/// Возвращает или устанавливает тело token.
		/// </summary>
		public string Body
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает тип token.
		/// </summary>
		public string Type
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает строковое представление даты, до которой действует токен.
		/// </summary>
		public DateTimeOffset ExpiresAt
		{
			get;
			set;
		}
	}
}
