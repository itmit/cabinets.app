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

		public string Login
		{
			get;
			set;
		}

		public string Role
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
