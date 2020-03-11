using System.Diagnostics;
using cabinets.Core.Services;
using Firebase.CloudMessaging;

namespace cabinets.iOS.Services
{
	public class IosFireBaseService : IFireBaseService
	{
		public void CreateInstance()
		{
			//Messaging.SharedInstance.
		}

		public void DeleteInstance()
		{
			Messaging.SharedInstance.DeleteFcmToken("IosFireBaseService", null);
			Debug.WriteLine(Messaging.SharedInstance.FcmToken);
		}
	}
}
