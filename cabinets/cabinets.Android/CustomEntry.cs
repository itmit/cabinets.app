using Android.Content;
using customEntry = cabinets.Controls.CustomEntry;
using cabinets.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(customEntry), typeof(CustomEntry))]
namespace cabinets.Droid
{
    class CustomEntry : EntryRenderer
    {
        public CustomEntry(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.SetPadding(20,25,25,25);
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }

    }
}