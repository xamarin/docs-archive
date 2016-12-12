using Android.App;
using Android.Hardware;
using Android.OS;
using Android.Widget;

namespace MotionDetector
{
    [Activity(Label = "MotionDetector", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity, ISensorEventListener
    {
		static readonly object syncLock = new object();
		SensorManager sensorManager;
		TextView sensorTextView;

        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
            // We don't want to do anything here.
        }

        public void OnSensorChanged(SensorEvent e)
        {
            lock (syncLock)
            {
                sensorTextView.Text = string.Format("x={0:f}, y={1:f}, y={2:f}", e.Values[0], e.Values[1], e.Values[2]);
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            sensorManager = (SensorManager) GetSystemService(SensorService);
            sensorTextView = FindViewById<TextView>(Resource.Id.accelerometer_text);
        }

        protected override void OnResume()
        {
            base.OnResume();
            sensorManager.RegisterListener(this,
                                            sensorManager.GetDefaultSensor(SensorType.Accelerometer),
                                            SensorDelay.Ui);
        }

        protected override void OnPause()
        {
            base.OnPause();
            sensorManager.UnregisterListener(this);
        }
    }
}