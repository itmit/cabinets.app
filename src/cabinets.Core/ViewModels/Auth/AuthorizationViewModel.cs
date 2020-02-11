using System;
using System.Collections.Generic;
using cabinets.Core.Models;
using cabinets.Core.Pages;
using cabinets.Core.Repositories;
using cabinets.Core.Services;
using cabinets.Core.Views;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace cabinets.Core.ViewModels.Auth
{
	public class AuthorizationViewModel : MvxNavigationViewModel
	{
		private MvxCommand _authorizationCommand;
		private MvxCommand _openRegistrationCommand;
		private readonly IAuthService _authService;
		private readonly IUserRepository _userRepository;

		public AuthorizationViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IAuthService authService, IUserRepository userRepository)
			: base(logProvider, navigationService)
		{
			_authService = authService;
			_userRepository = userRepository;
		}

		public string Login
		{
			get;
			set;
		}

		public Dictionary<string, string> ErrorsDictionary
		{
			get;
			set;
		} = new Dictionary<string, string>();

		public string Password
		{
			get;
			set;
		}

		public IMvxCommand AuthorizationCommand
		{
			get
			{
				_authorizationCommand = _authorizationCommand ?? new MvxCommand(AuthorizationCommandExecute);
				return _authorizationCommand;
			}
		}

		private async void AuthorizationCommandExecute()
		{
			bool needRaise = false;
			var login = Login?.Trim();
			if (string.IsNullOrEmpty(login))
			{
				ErrorsDictionary[nameof(Login)] = "Поле логин не заполнено.";
				needRaise = true;
			}

			var pass = Password?.Trim();
			if (string.IsNullOrEmpty(pass))
			{
				ErrorsDictionary[nameof(Password)] = "Поле пароль не заполнено.";
				needRaise = true;
			}

			if (needRaise)
			{
				await RaisePropertyChanged(nameof(ErrorsDictionary));
				return;
			}

			User user = null;
			try
			{
				user = await _authService.Login(login, pass);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			if (user == null)
			{
				if (_authService.Errors == null)
				{
					await Application.Current.MainPage.DisplayAlert("Внимание", "Ошибка сервера", "Ок");
					return;
				}

				if (_authService.Errors.ContainsKey("Fatal") && !string.IsNullOrEmpty(_authService.Errors["Fatal"]))
				{
					await Application.Current.MainPage.DisplayAlert("Внимание", _authService.Errors["Fatal"], "Ок");
					return;
				}

				ErrorsDictionary = _authService.Errors;
				await RaisePropertyChanged(nameof(ErrorsDictionary));
				return;
			}
			_userRepository.Add(user);

			await NavigationService.Navigate<MainViewModel>();
		}

		public IMvxCommand OpenRegistrationCommand
		{
			get
			{
				_openRegistrationCommand = _openRegistrationCommand ?? new MvxCommand(() =>
				{
					NavigationService.Navigate<RegistrationViewModel>();
				});
				return _openRegistrationCommand;
			}
		}
	}
}
