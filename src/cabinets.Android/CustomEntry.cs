using Android.Content;
using cabinets.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntry))]

namespace cabinets.Droid
{
	internal class CustomEntry : EntryRenderer
	{
		#region .ctor
		public CustomEntry(Context context)
			: base(context)
		{
		}
		#endregion

		#region Overrided
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.SetPadding(20, 25, 25, 25);
				Control.SetBackgroundColor(Color.Transparent);
			}
		}
		#endregion
	}
}
