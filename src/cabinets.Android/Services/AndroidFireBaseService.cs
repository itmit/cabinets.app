using System.Threading.Tasks;
using Android.Gms.Extensions;
using cabinets.Core.Services;
using cabinets.Droid.Services;
using Firebase.Iid;
using Firebase.Messaging;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidFireBaseService))]
namespace cabinets.Droid.Services
{
	public class AndroidFireBaseService : IFireBaseService
	{
		public void CreateInstance()
		{
			Task.Run(() =>
			{
				FirebaseInstanceId.Instance.DeleteInstanceId();
			});
		}

		public void DeleteInstance()
		{
			Task.Run(() =>
			{
				FirebaseInstanceId.Instance.DeleteInstanceId();
			});
		}
		private const string AllTopicName = "all";

		public async void SubscribeToAllTopic()
		{
			await FirebaseMessaging.Instance.SubscribeToTopic(AllTopicName);
		}

		public async void UnsubscribeToAllTopic()
		{
			await FirebaseMessaging.Instance.UnsubscribeFromTopic(AllTopicName);
		}
	}

}
