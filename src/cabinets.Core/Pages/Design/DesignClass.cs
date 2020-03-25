using Xamarin.Forms;

namespace cabinets.Core.Pages.Design
{
	static class DesignClass
	{
		public static string FontRegular, FontBold;

		static DesignClass()
		{
			if (Device.iOS == Device.RuntimePlatform)
			{
				FontRegular = "Lato-Regular";
				FontBold = "Lato-Bold";
			}
			else if(Device.Android == Device.RuntimePlatform)
			{
				FontRegular = "Lato-Regular.ttf#Lato-Regular";
				FontBold = "Lato-Bold.ttf#Lato-Bold";
			}
		}
	}
}
