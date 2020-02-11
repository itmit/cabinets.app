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

		Task<User> Registration(User user, string password, string confirmPassword);

		Dictionary<string, string> Errors
		{
			get;
		}

		void Logout(User user);
	}
}
