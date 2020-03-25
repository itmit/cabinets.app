using System;
using cabinets.Core.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace cabinets.Core.ViewModels.Auth
{
	public class RecoveryViewModel : MvxViewModel<string>
	{
		private MvxCommand _recoverCommand;
		private IAuthService _authService;
		private string _email;
		private string _code;
		private string _password;
		private string _passwordConfirm;
		private MvxCommand _sendCodeCommand;

		public RecoveryViewModel(IAuthService authService)
		{
			_authService = authService;
		}

		public string Code
		{
			get => _code;
			set => SetProperty(ref _code, value);
		}

		public string Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}

		public string PasswordConfirm
		{
			get => _passwordConfirm;
			set => SetProperty(ref _passwordConfirm, value);
		}

		public MvxCommand RecoverCommand
		{
			get
			{
				_recoverCommand = _recoverCommand ?? new MvxCommand(async () =>
				{
					if (string.IsNullOrEmpty(Code) && string.IsNullOrEmpty(Password) && string.IsNullOrEmpty(PasswordConfirm))
					{
						Device.BeginInvokeOnMainThread(() =>
						{
							Application.Current.MainPage.DisplayAlert("Внимание", "Все поля должны быть заполнены.", "Ок");
						});
					}

					var res = false;

					try
					{
						res = await _authService.Recovery(_email, Code, Password, PasswordConfirm);
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
					if (res)
					{
						await Application.Current.MainPage.Navigation.PopToRootAsync();
						
						Device.BeginInvokeOnMainThread(() =>
						{
							Application.Current.MainPage.DisplayAlert("Внимание", "Пароль сброшен", "Ок");
						});
					}
				});
				return _recoverCommand;
			}
		}

		public MvxCommand SendCodeCommand
		{
			get
			{
				_sendCodeCommand = _sendCodeCommand ?? new MvxCommand(async () => 
				{

					bool res = false;
					try
					{
						res = await _authService.SendRecoveryCode(_email);
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}

					if (res)
					{
						Device.BeginInvokeOnMainThread(() =>
						{
							Application.Current.MainPage.DisplayAlert("Внимание", $"Код на {_email} отправлен повторно.", "Ок");
						});
					}
				});
				return _sendCodeCommand;
			}
		}

		public override void Prepare(string parameter)
		{
			_email = parameter;
		}
	}
}
