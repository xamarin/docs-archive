using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace SimpleService
{
    [Activity(Label = "Simple Service Demo", MainLauncher = true)]
    public class Activity1 : Activity
    {
		bool isServiceRunning = false;
		Button startButton;
		Button stopButton;
		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

			startButton = FindViewById<Button>(Resource.Id.startService);
			startButton.Click += Start_Click; 

			stopButton = FindViewById<Button>(Resource.Id.stopService);
			stopButton.Click += Stop_Click;
			stopButton.Enabled = false;
        }

		protected override void OnPause()
		{
			// Clean up: shut down the service when the Activity is no longer visible.
			StopService(new Intent(this, typeof(SimpleStartedService)));
			base.OnPause();
		}

		void Start_Click(object sender, System.EventArgs e)
		{
			StartService(new Intent(this, typeof(SimpleStartedService)));
			isServiceRunning = true;
			startButton.Enabled = false;
			stopButton.Enabled = true;
		}


		void Stop_Click(object sender, System.EventArgs e)
		{
			StopService(new Intent(this, typeof(SimpleStartedService)));
			isServiceRunning = false;
			startButton.Enabled = true;
			stopButton.Enabled = false;
		}

	
	}
}