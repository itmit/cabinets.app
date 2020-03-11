using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Util;
using FFImageLoading.Forms.Platform;
using MvvmCross.Forms.Platforms.Android.Views;
using Rg.Plugins.Popup;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace cabinets.Droid
{
	[Activity(Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class FormsActivity : MvxFormsAppCompatActivity
	{
		public static readonly string Tag = "CabinetsFormsActivity";

		public static readonly string ChannelId = "cabinets_notification_channel";
		public static readonly int NotificationId = 100;

		private void CreateNotificationChannel()
		{
			if (Build.VERSION.SdkInt < BuildVersionCodes.O)
			{
				// Notification channels are new in API 26 (and not a part of the
				// support library). There is no need to create a notification
				// channel on older versions of Android.
				return;
			}

			var channel = new NotificationChannel(ChannelId,
												  "FCM Notifications",
												  NotificationImportance.Default)
			{

				Description = "FireBase Cloud Messages appear in this channel"
			};

			var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
			notificationManager.CreateNotificationChannel(channel);
		}

		#region Overrided
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
		{
			Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			CachedImageRenderer.Init(true);
			Platform.Init(this, savedInstanceState);

			Popup.Init(this, savedInstanceState);
			Forms.Init(this, savedInstanceState);

			base.OnCreate(savedInstanceState);

			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			CreateNotificationChannel();

			var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);

			Log.Info(Tag, $"IsGooglePlayServicesAvailable code is {resultCode}");
		}
		#endregion
	}
}
