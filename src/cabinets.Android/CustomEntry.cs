using Android.Content;
using cabinets.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntry))]
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