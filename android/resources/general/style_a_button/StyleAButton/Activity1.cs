using Android.App;
using Android.OS;

namespace ButtonStyle
{
    [Activity(Label = "ButtonStyle", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
        }
    }
}