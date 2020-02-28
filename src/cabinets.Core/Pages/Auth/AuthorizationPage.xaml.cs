﻿using cabinets.Core.ViewModels.Auth;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace cabinets.Core.Pages.Auth
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxContentPagePresentation(NoHistory = true, Animated = false)]
	public partial class AuthorizationPage : MvxContentPage<AuthorizationViewModel>
	{
		#region .ctor
		public AuthorizationPage()
		{
			InitializeComponent();
		}
		#endregion
	}
}
