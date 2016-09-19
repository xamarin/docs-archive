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
            SetContentView(Resource.Layout.Main);

            Button start = FindViewById<Button>(Resource.Id.startService);
            start.Click += (sender, args) => { StartService(new Intent(this, typeof(SimpleService))); };

            Button stop = FindViewById<Button>(Resource.Id.stopService);
            stop.Click += (sender, args) => { StopService(new Intent(this, typeof(SimpleService))); };
        }

        protected override void OnStop()
        {
            base.OnStop();
            // Clean up: shut down the service when the Activity is no longer visible.
            StopService(new Intent(this, typeof (SimpleService)));
        }
    }
}