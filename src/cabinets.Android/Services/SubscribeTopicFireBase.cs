using Android.Gms.Extensions;
using cabinets.Core.Services;
using Firebase.Messaging;

namespace cabinets.Droid.Services
{
	public class SubscribeTopicFireBase : ISubscribeTopicFireBase
	{
		private const string AllTopicName = "All";

		public async void SubscribeToAllTopic()
		{
			await FirebaseMessaging.Instance.SubscribeToTopic(AllTopicName);
		}
	}
}
