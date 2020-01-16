using cabinets.iOS;
using customEntry = cabinets.Controls.CustomEntry;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(customEntry), typeof(CustomEntry))]
namespace cabinets.iOS
{
    public class CustomEntry : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BackgroundColor = UIColor.FromName("Transparent");
            }
        }
    }
}