

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cabinets.Core.Models;

namespace cabinets.Core.Services
{
	public interface INewsService
	{
		Task<List<News>> GetAll();

		Dictionary<string, string> Errors
		{
			get;
		}

		Task<News> GetNews(Guid guid);
	}
}
