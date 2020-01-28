using System.Collections.Generic;
using System.Threading.Tasks;
using cabinets.Core.Models;

namespace cabinets.Core.Services
{
	/// <summary>
	/// Представляет интерфейс для авторизации.
	/// </summary>
	public interface IAuthService
	{
		/// <summary>
		/// Проводит авторизацию пользователя.
		/// </summary>
		/// <param name="login">Логин пользователя.</param>
		/// <param name="password">Пароль пользователя.</param>
		/// <returns>Авторизованный пользователь.</returns>
		Task<User> Login(string login, string password);

		Dictionary<string, string> Errors
		{
			get;
		}
	}
}
