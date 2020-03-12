﻿using System;
using FFImageLoading.Forms.Platform;
using Firebase.CloudMessaging;
using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using Rg.Plugins.Popup;
using UIKit;
using UserNotifications;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace cabinets.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : MvxFormsApplicationDelegate, IUNUserNotificationCenterDelegate, IMessagingDelegate
	{
		public void DidRefreshRegistrationToken(Messaging messaging, string fcmToken)
		{
			System.Diagnostics.Debug.WriteLine($"FCM Token: {fcmToken}");
		}

		#region Overrided
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Popup.Init();
			Forms.Init();
			CachedImageRenderer.Init();
			Firebase.Core.App.Configure();

			// Register your app for remote notifications.
			if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
			{
				// iOS 10 or later
				var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
				UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
					Console.WriteLine(granted);
				});

				// For iOS 10 display notification (sent via APNS)
				UNUserNotificationCenter.Current.Delegate = this;

				// For iOS 10 data message (sent via FCM)
				Messaging.SharedInstance.Delegate = this;

				UNUserNotificationCenter.Current.GetNotificationSettings((settings) =>
				{
					var alertsAllowed = (settings.AlertSetting == UNNotificationSetting.Enabled);
				});
			}
			else
			{
				// iOS 9 or before
				var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
				var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
				UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
			}

			UIApplication.SharedApplication.RegisterForRemoteNotifications();

			return true;
		}
		#endregion
	}
}
