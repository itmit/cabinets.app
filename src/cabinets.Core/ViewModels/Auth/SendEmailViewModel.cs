using cabinets.Core.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace cabinets.Core.ViewModels.Auth
{
	public class SendEmailViewModel : MvxViewModel
	{
		private string _email;
		private MvxCommand _sendCodeCommand;
		private readonly IAuthService _authService;

		public SendEmailViewModel(IAuthService authService)
		{
			_authService = authService;
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

		private void SendCodeCommandExecute()
		{

		}
	}
}
