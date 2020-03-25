using System;
using System.Collections.Generic;
using System.Globalization;
using cabinets.Core.Models;
using cabinets.Core.Repositories;
using cabinets.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace cabinets.Core.ViewModels.Auth
{
	public class RegistrationViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		private readonly IAuthService _authService;
		private DateTime _date = new DateTime(2000, 1, 1);
		private string _email;
		private Dictionary<string, string> _errorsDictionary = new Dictionary<string, string>();
		private string _firstName;
		private bool _isCheckedPolicy;
		private string _lastName;
		private readonly IMvxNavigationService _navigationService;
		private MvxCommand _openPolicyCommand;
		private string _password;
		private string _phone;
		private MvxCommand _registrationCommand;
		private string _repeatPass;
		private readonly IUserRepository _userRepository;
		#endregion
		#endregion

		#region .ctor
		public RegistrationViewModel(IAuthService authService, IMvxNavigationService navigationService, IUserRepository userRepository)
		{
			_authService = authService;
			_navigationService = navigationService;
			_userRepository = userRepository;
		}
		#endregion

		#region Properties
		public DateTime Date
		{
			get => _date;
			set => SetProperty(ref _date, value);
		}

		public string Email
		{
			get => _email;
			set => SetProperty(ref _email, value);
		}

		public Dictionary<string, string> ErrorsDictionary
		{
			get => _errorsDictionary;
			set => SetProperty(ref _errorsDictionary, value);
		}

		public string FirstName
		{
			get => _firstName;
			set => SetProperty(ref _firstName, value);
		}

		public bool IsCheckedPolicy
		{
			get => _isCheckedPolicy;
			set => SetProperty(ref _isCheckedPolicy, value);
		}

		public string LastName
		{
			get => _lastName;
			set => SetProperty(ref _lastName, value);
		}

		public MvxCommand OpenPolicyCommand
		{
			get
			{
				_openPolicyCommand = _openPolicyCommand ??
									 new MvxCommand(() =>
									 {
										 _navigationService.Navigate<PolicyViewModel>();
									 });
				return _openPolicyCommand;
			}
		}

		public string Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}

		public string Phone
		{
			get => _phone;
			set => SetProperty(ref _phone, value);
		}

		public IMvxCommand RegistrationCommand
		{
			get
			{
				_registrationCommand = _registrationCommand ?? new MvxCommand(RegistrationCommandExecute, () => IsCheckedPolicy);
				return _registrationCommand;
			}
		}

		public string RepeatPass
		{
			get => _repeatPass;
			set => SetProperty(ref _repeatPass, value);
		}
		#endregion

		#region Private
		private async void RegistrationCommandExecute()
		{
			var needRaise = false;
			var firstName = FirstName?.Trim();
			if (string.IsNullOrEmpty(firstName))
			{
				ErrorsDictionary[nameof(FirstName)] = "Поле имя не заполнено.";
				needRaise = true;
			}

			var lastName = LastName?.Trim();
			if (string.IsNullOrEmpty(lastName))
			{
				ErrorsDictionary[nameof(LastName)] = "Поле фамилия не заполнено.";
				needRaise = true;
			}

			if (Date < new DateTime(1900, 1, 1) || Date > DateTime.Now)
			{
				ErrorsDictionary[nameof(Date)] = "Не правильно указана дата.";
				needRaise = true;
			}

			DateTime zeroTime = new DateTime(1, 1, 1);
			var span = DateTime.Now - Date;
			if ((zeroTime + span).Year - 1 < 18)
			{
				ErrorsDictionary[nameof(Date)] = "Приложение доступно только для лиц старше 18 лет.";
				needRaise = true;
			}

			var pass = Password?.Trim();
			if (string.IsNullOrEmpty(pass))
			{
				ErrorsDictionary[nameof(Password)] = "Поле пароль не заполнено.";
				needRaise = true;
			}

			var confirmPass = RepeatPass?.Trim();
			if (string.IsNullOrEmpty(confirmPass))
			{
				ErrorsDictionary[nameof(RepeatPass)] = "Поле пароль не заполнено.";
				needRaise = true;
			}

			if (needRaise)
			{
				await RaisePropertyChanged(nameof(ErrorsDictionary));
				return;
			}

			var user = new User
			{
				Birthday = Date,
				Email = Email,
				Name = $"{firstName} {lastName}",
				Phone = Phone
			};
			try
			{
				user = await _authService.Registration(user, pass, confirmPass);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			if (user?.AccessToken == null)
			{
				var errors = new Dictionary<string, string>();
				TextInfo ti = new CultureInfo("en-US", false).TextInfo;
				foreach (var detail in _authService.Errors)
				{
					errors[ti.ToTitleCase(detail.Key)] = string.Join("&#10;", detail.Value);
				}

				if (_authService.Errors.ContainsKey("Fatal") && !string.IsNullOrEmpty(errors["Fatal"]))
				{
					await Application.Current.MainPage.DisplayAlert("Внимание", errors["Fatal"], "Ок");
					return;
				}

				ErrorsDictionary = errors;
				await RaisePropertyChanged(nameof(ErrorsDictionary));
				return;
			}

			_userRepository.Add(user);
			await _navigationService.Navigate<TwoButtonViewModel>();
		}
		#endregion
	}
}
