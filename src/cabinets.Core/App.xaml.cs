using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Application = Xamarin.Forms.Application;

namespace cabinets.Core
{
	public partial class App : Application
	{
		#region .ctor
		public App()
		{
			InitializeComponent();
			On<Android>()
				.UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
		}
		#endregion

		#region Overrided
		protected override void OnResume()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnStart()
		{
		}
		#endregion
	}
}
