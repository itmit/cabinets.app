using System;
using System.Threading.Tasks;
using Android.Gms.Extensions;
using Android.OS;
using cabinets.Core.Services;
using cabinets.Droid.Services;
using Firebase.Iid;
using Firebase.Messaging;
using Xamarin.Forms;
using cabinets.Core.Helpers;

[assembly: Dependency(typeof(AndroidFireBaseService))]
namespace cabinets.Droid.Services
{
	public class AndroidFireBaseService : IFireBaseService
	{
		public Task<string> CreateToken(string senderId, string scope = "")
		{
			if (string.IsNullOrEmpty(scope))
			{
				scope = FirebaseMessaging.InstanceIdScope;
			}

			return Task.Run(() => FirebaseInstanceId.Instance.GetToken(senderId, scope));
		}

		public void DeleteInstance(string senderId, string scope = "")
		{
			if (string.IsNullOrEmpty(scope))
			{
				scope = FirebaseMessaging.InstanceIdScope;
			}

			Task.Run(() =>
			{
				FirebaseInstanceId.Instance.DeleteToken(senderId, scope);

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
