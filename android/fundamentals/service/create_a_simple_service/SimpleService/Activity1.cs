using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace SimpleService
{
    [Activity(Label = "SimpleService", MainLauncher = true)]
    public class Activity1 : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button start = FindViewById<Button>(Resource.Id.startService);

            start.Click += delegate { StartService(new Intent(this, typeof (SimpleService))); };

            Button stop = FindViewById<Button>(Resource.Id.stopService);

            stop.Click += delegate { StopService(new Intent(this, typeof (SimpleService))); };
        }
    }
}