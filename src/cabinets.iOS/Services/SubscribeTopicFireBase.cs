using cabinets.Core.Services;
using cabinets.iOS.Services;
using Firebase.CloudMessaging;
using Xamarin.Forms;

[assembly: Dependency(typeof(SubscribeTopicFireBase))]
namespace cabinets.iOS.Services
{
	public class SubscribeTopicFireBase : ISubscribeTopicFireBase
	{
		private const string AllTopicName = "All";

		public void SubscribeToAllTopic()
		{
			Messaging.SharedInstance.Subscribe(AllTopicName);
		}
	}
}
