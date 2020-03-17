using System.Threading.Tasks;

namespace cabinets.Core.Services
{
	public interface IFireBaseService
	{
		Task<string> CreateToken(string senderId, string scope = "");

		void DeleteInstance(string senderId, string scope = "");

		void SubscribeToAllTopic();
		
		void UnsubscribeToAllTopic();
	}
}
