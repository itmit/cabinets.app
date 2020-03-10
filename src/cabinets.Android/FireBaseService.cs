using System.Collections.Generic;
using System.Diagnostics;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Android.Util;
using cabinets.Core.Services;
using Firebase.Messaging;
using MvvmCross;

namespace cabinets.Droid
{
	[Service]
	[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
	public class FireBaseService : FirebaseMessagingService
	{
		private const string Tag = "CabinetsFireBaseService";

		public override void OnMessageReceived(RemoteMessage message)
		{
			Log.Debug(Tag, "From: " + message.From);

			var body = message.GetNotification().Body;
			Log.Debug(Tag, "Notification Message Body: " + body);
			SendNotification(body, message.Data);
		}

		private void SendNotification(string messageBody, IDictionary<string, string> data)
		{
			var intent = new Intent(this, typeof(FormsActivity));
			intent.AddFlags(ActivityFlags.ClearTop);
			foreach (var key in data.Keys)
			{
				intent.PutExtra(key, data[key]);
			}

			var pendingIntent = PendingIntent.GetActivity(this, FormsActivity.NotificationId, intent, PendingIntentFlags.OneShot);

			var notificationBuilder = new NotificationCompat.Builder(this, FormsActivity.ChannelId)
									  .SetSmallIcon(Resource.Drawable.baseline_hourglass_empty_24)
									  .SetContentTitle("Внимание")
									  .SetContentText(messageBody)
									  .SetAutoCancel(true)
									  .SetContentIntent(pendingIntent);

			var notificationManager = NotificationManagerCompat.From(this);
			notificationManager.Notify(FormsActivity.NotificationId, notificationBuilder.Build());
		}

		public override void OnNewToken(string p0)
		{
			base.OnNewToken(p0);
			Log.Debug(Tag, "Refreshed token: " + p0);
			Debug.WriteLine(p0);
			SendRegistrationToServer(p0);
		}

		void SendRegistrationToServer(string token)
		{
			Mvx.IoCProvider.Resolve<IAuthService>()
			   .SendDeviceToken(token);
		}
	}
}
