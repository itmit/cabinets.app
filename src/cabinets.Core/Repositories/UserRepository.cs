using System.Collections.Generic;
using AutoMapper;
using cabinets.Core.Models;
using cabinets.Core.RealmObjects;
using Realms;

namespace cabinets.Core.Repositories
{
	public class UserRepository : IUserRepository
	{
		#region Data
		#region Fields
		private readonly IMapper _mapper;
		#endregion
		#endregion

		#region .ctor
		public UserRepository()
		{
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<AccessToken, AccessTokenRealmObject>();
				cfg.CreateMap<AccessTokenRealmObject, AccessToken>();

				cfg.CreateMap<User, UserRealmObject>()
				   .ForPath(m => m.AccessToken.Body, o => o.MapFrom(q => q.AccessToken.Body))
				   .ForPath(m => m.AccessToken.Type, o => o.MapFrom(q => q.AccessToken.Type))
				   .ForPath(m => m.AccessToken.ExpiresAt, o => o.MapFrom(q => q.AccessToken.ExpiresAt))
				   .ForPath(m => m.Birthday, o => o.MapFrom(q => q.Birthday));
				cfg.CreateMap<UserRealmObject, User>()
				   .ForPath(m => m.AccessToken.Body, o => o.MapFrom(q => q.AccessToken.Body))
				   .ForPath(m => m.AccessToken.Type, o => o.MapFrom(q => q.AccessToken.Type))
				   .ForPath(m => m.AccessToken.ExpiresAt, o => o.MapFrom(q => q.AccessToken.ExpiresAt.DateTime))
				   .ForPath(m => m.Birthday, o => o.MapFrom(q => q.Birthday.DateTime));
			}));
		}
		#endregion

		#region IUserRepository members
		public void Add(User user)
		{
			using (var realm = Realm.GetInstance())
			{
				var userRealm = _mapper.Map<UserRealmObject>(user);
				using (var transaction = realm.BeginWrite())
				{
					realm.Add((RealmObject) userRealm, true);
					transaction.Commit();
				}
			}
		}

		public IEnumerable<User> GetAll()
		{
			using (var realm = Realm.GetInstance())
			{
				var users = realm.All<UserRealmObject>();
				var userList = new List<User>();
				foreach (var user in users)
				{
					userList.Add(_mapper.Map<User>(user));
				}

				return userList;
			}
		}

		public void Remove(User user)
		{
			using (var realm = Realm.GetInstance())
			{
				using (var transaction = realm.BeginWrite())
				{
					var userRealm = realm.Find<UserRealmObject>(user.Guid.ToString());
					realm.Remove(userRealm);
					transaction.Commit();
				}
			}
		}

		public void Update(User user)
		{
			Remove(user);
			Add(user);
		}
		#endregion
	}
}
