using System.Threading.Tasks;
using cabinets.Core.Services;
using cabinets.Droid.Services;
using Firebase.Iid;
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
				FirebaseInstanceId.Instance.GetInstanceId();
			});
		}

		public void DeleteInstance()
		{
			Task.Run(() =>
			{
				FirebaseInstanceId.Instance.DeleteInstanceId();
			});
		}
	}

}
