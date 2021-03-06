﻿using System.Collections.Generic;
using System.Threading.Tasks;
using cabinets.Core.Models;

namespace cabinets.Core.Services
{
	/// <summary>
	/// Представляет интерфейс для авторизации.
	/// </summary>
	public interface IAuthService
	{
		#region Properties
		Dictionary<string, string[]> Errors
		{
			get;
		}

		User User
		{
			get;
		}
		#endregion

		/// <summary>
		/// Проводит авторизацию пользователя.
		/// </summary>
		/// <param name="login">Логин пользователя.</param>
		/// <param name="password">Пароль пользователя.</param>
		/// <returns>Авторизованный пользователь.</returns>
		Task<User> Login(string login, string password);

		void Logout(User user);

		Task<User> Registration(User user, string password, string confirmPassword);

		Task<bool> SendDeviceToken(string token);

		Task<bool> SendRecoveryCode(string email);

		Task<bool> Recovery(string email, string code, string password, string passwordConfirmation);
	}
}
