using System.Collections.Generic;
using cabinets.Core.Models;

namespace cabinets.Core.Repositories
{
	public interface IUserRepository
	{
		#region Overridable
		void Add(User user);

		IEnumerable<User> GetAll();

		void Remove(User user);

		void Update(User user);
		#endregion
	}
}
