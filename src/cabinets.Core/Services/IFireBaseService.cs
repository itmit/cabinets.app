namespace cabinets.Core.Services
{
	public interface IFireBaseService
	{
		void CreateInstance();

		void DeleteInstance();

		void SubscribeToAllTopic();
		
		void UnsubscribeToAllTopic();
	}
}
