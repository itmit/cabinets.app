using System;
using System.Net.Mail;
using System.Net.Mime;
using cabinets.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace cabinets.Core.ViewModels.Auth
{
	public class SendRecoveryCodeViewModel : MvxViewModel
	{
		private string _email;
		private MvxCommand _sendCodeCommand;
		private readonly IAuthService _authService;
		private readonly IMvxNavigationService _navigationService;

		public SendRecoveryCodeViewModel(IAuthService authService, IMvxNavigationService navigationService)
		{
			_authService = authService;
			_navigationService = navigationService;
		}

		public string Email
		{
			get => _email;
			set => SetProperty(ref _email, value);
		}

		public MvxCommand SendCodeCommand
		{
			get
			{
				_sendCodeCommand = _sendCodeCommand ?? new MvxCommand(SendCodeCommandExecute);
				return _sendCodeCommand;
			}
		}

		public bool IsEmailValid(string emailAddress)
		{
			try
			{
				MailAddress m = new MailAddress(emailAddress);
				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}

		private async void SendCodeCommandExecute()
		{
			if (!IsEmailValid(Email))
			{
				Device.BeginInvokeOnMainThread(() =>
				{
					Application.Current.MainPage.DisplayAlert("Внимание", "Не корректный email.", "Ок");
				});
			}

			bool res = false;
			try
			{
				res = await _authService.SendRecoveryCode(Email);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			if (res)
			{
				await _navigationService.Navigate<RecoveryViewModel, string>(Email);
			}
		}
	}
}
