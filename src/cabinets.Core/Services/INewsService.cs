using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cabinets.Core.Models;

namespace cabinets.Core.Services
{
	public interface INewsService
	{
		#region Overridable
		Task<List<News>> GetAll();

		Task<News> GetNews(Guid guid);
		#endregion
	}
}
