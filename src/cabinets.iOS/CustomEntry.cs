using cabinets.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntry))]

namespace cabinets.iOS
{
	public class CustomEntry : EntryRenderer
	{
		#region Overrided
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.BackgroundColor = UIColor.FromName("Transparent");
			}
		}
		#endregion
	}
}
