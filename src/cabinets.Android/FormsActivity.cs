using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using FFImageLoading.Forms.Platform;
using MvvmCross.Forms.Platforms.Android.Views;
using Rg.Plugins.Popup;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace cabinets.Droid
{
	[Activity(Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class FormsActivity : MvxFormsAppCompatActivity
	{
		#region Overrided
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
		{
			Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			CachedImageRenderer.Init(true);
			Platform.Init(this, savedInstanceState);
			//global::Xamarin.Forms.Forms.SetFlags("CarouselView_Experimental");
			Popup.Init(this, savedInstanceState);
			Forms.Init(this, savedInstanceState);

			base.OnCreate(savedInstanceState);

			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;
		}
		#endregion
	}
}
