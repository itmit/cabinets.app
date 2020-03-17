using System;
using System.Diagnostics;
using cabinets.Core.Services;
using Firebase.CloudMessaging;
using Foundation;

namespace cabinets.iOS.Services
{
	public class IosFireBaseService : IFireBaseService
	{
		public void CreateInstance()
		{
			Messaging.SharedInstance.RetrieveFcmToken("IosFireBaseService", Completion);
			Debug.WriteLine(Messaging.SharedInstance.FcmToken);
		}

		private void Completion(string fcmToken, NSError error)
		{
			Console.WriteLine($"FCM Token: {fcmToken}");
			Console.WriteLine(error);
		}

		public void DeleteInstance()
		{
			Messaging.SharedInstance.DeleteFcmToken("IosFireBaseService", Completion);
		}

		private void Completion(NSError error)
		{
			Console.WriteLine(error);
		}

		public void SubscribeToAllTopic()
		{
			Messaging.SharedInstance.Subscribe(AllTopicName);

		}

		private const string AllTopicName = "all";

		public void UnsubscribeToAllTopic()
		{
			Messaging.SharedInstance.Unsubscribe(AllTopicName);

		}
	}
}
