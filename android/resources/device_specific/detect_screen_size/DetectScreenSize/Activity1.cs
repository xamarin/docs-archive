using Android.App;
using Android.OS;
using Android.Widget;

namespace DetectScreenSize
{
    [Activity(Label = "Screen Size", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var metrics = Resources.DisplayMetrics;
            var widthInDp = ConvertPixelsToDp(metrics.WidthPixels);
            var heightInDp = ConvertPixelsToDp(metrics.HeightPixels);

            FindViewById<TextView>(Resource.Id.screenWidthDp).Text = "Screen Width: " + widthInDp + " dp.";
            FindViewById<TextView>(Resource.Id.screenHeightDp).Text = "Screen Height: " + heightInDp + " dp.";
        }

        private int ConvertPixelsToDp(float pixelValue)
        {
            var dp = (int) ((pixelValue)/Resources.DisplayMetrics.Density);
            return dp;
        }
    }
}