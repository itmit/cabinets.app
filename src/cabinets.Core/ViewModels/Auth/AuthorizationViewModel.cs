using System;
using System.Collections.Generic;
using System.Diagnostics;
using cabinets.Core.Helpers;
using cabinets.Core.Models;
using cabinets.Core.Repositories;
using cabinets.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace cabinets.Core.ViewModels.Auth
{
	public class AuthorizationViewModel : MvxNavigationViewModel
	{
		#region Data
		#region Fields
		private MvxCommand _authorizationCommand;
		private readonly IAuthService _authService;
		private MvxCommand _openRegistrationCommand;
		private readonly IUserRepository _userRepository;
		private readonly IFireBaseService _fireBaseService;
		private MvxCommand _openRecoveryCommand;
		#endregion
		#endregion

		#region .ctor
		public AuthorizationViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IAuthService authService, IUserRepository userRepository, IFireBaseService fireBaseService)
			: base(logProvider, navigationService)
		{
			_fireBaseService = fireBaseService;
			_authService = authService;
			_userRepository = userRepository;
		}
		#endregion

		#region Properties
		public Dictionary<string, string> ErrorsDictionary
		{
			get;
			set;
		} = new Dictionary<string, string>();

		public string Login
		{
			get;
			set;
		}

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

		public IMvxCommand OpenRegistrationCommand
		{
			get
			{
				_openRegistrationCommand = _openRegistrationCommand ??
										   new MvxCommand(() =>
										   {
											   NavigationService.Navigate<RegistrationViewModel>();
										   });
				return _openRegistrationCommand;
			}
		}

		public MvxCommand OpenRecoveryCommand
		{
			get
			{
				_openRecoveryCommand = _openRecoveryCommand ??
										   new MvxCommand(() =>
										   {
											   NavigationService.Navigate<SendRecoveryCodeViewModel>();
										   });
				return _openRecoveryCommand;
			}
		}
		#endregion

		#region Private
		private async void AuthorizationCommandExecute()
		{
			var needRaise = false;
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
				var errors = new Dictionary<string, string>();
				foreach (var detail in _authService.Errors)
				{
					errors[detail.Key] = string.Join("&#10;", detail.Value);
				}

				if (_authService.Errors.ContainsKey("Fatal"))
				{
					await Application.Current.MainPage.DisplayAlert("Внимание", errors["Fatal"], "Ок");
					return;
				}

				ErrorsDictionary = errors;
				await RaisePropertyChanged(nameof(ErrorsDictionary));
				return;
			}

			_userRepository.Add(user);
			try
			{
				string token = await _fireBaseService.CreateToken(Secrets.SenderId);
				if (await _authService.SendDeviceToken(token))
				{
					_fireBaseService.SubscribeToAllTopic();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			await NavigationService.Navigate<MainViewModel>();
		}
		#endregion
	}
}
