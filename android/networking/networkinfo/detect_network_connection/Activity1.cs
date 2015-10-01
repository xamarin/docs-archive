using Android.Telephony;
using Android.Renderscripts;
using Android.Util;
using Android.Nfc;

namespace NetworkDetection
{
    using Android.App;
    using Android.Net;
    using Android.OS;
    using Android.Widget;

    [Activity(Label = "Network Detection", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
		static readonly string TAG = typeof(Activity1).FullName;

        private ImageView _isConnectedImage;
        private ImageView _roamingImage;
        private ImageView _wifiImage;
		TextView _connectionType;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            _wifiImage = FindViewById<ImageView>(Resource.Id.wifi_image);
            _roamingImage = FindViewById<ImageView>(Resource.Id.roaming_image);
            _isConnectedImage = FindViewById<ImageView>(Resource.Id.is_connected_image);
			_connectionType = FindViewById<TextView>(Resource.Id.connection_type_text);
        }

		protected override void OnResume()
		{
			base.OnResume();
			DetectNetwork();
		}

        private void DetectNetwork()
        {
			ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;

			bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
			Log.Debug(TAG, "IsOnline = {0}", isOnline);

			if (isOnline)
			{
				_isConnectedImage.SetImageResource(Resource.Drawable.green_square);

				// Display the type of connection
				NetworkInfo.State activeState = activeConnection.GetState();
				_connectionType.Text = activeConnection.TypeName;

				// Check for a WiFi connection
				NetworkInfo wifiInfo = connectivityManager.GetNetworkInfo(ConnectivityType.Wifi);
				if(wifiInfo.IsConnected)
				{
					Log.Debug(TAG, "Wifi connected.");
					_wifiImage.SetImageResource(Resource.Drawable.green_square);
				} else
				{
					Log.Debug(TAG, "Wifi disconnected.");
					_wifiImage.SetImageResource(Resource.Drawable.red_square);
				}

				// Check if roaming
				NetworkInfo mobileInfo = connectivityManager.GetNetworkInfo(ConnectivityType.Mobile);
				if(mobileInfo.IsRoaming && mobileInfo.IsConnected)
				{
					Log.Debug(TAG, "Roaming.");
					_roamingImage.SetImageResource(Resource.Drawable.green_square);
				} else
				{
					Log.Debug(TAG, "Not roaming.");
					_roamingImage.SetImageResource(Resource.Drawable.red_square);
				}				
			} else
			{
				_isConnectedImage.SetImageResource(Resource.Drawable.red_square);
				_wifiImage.SetImageResource(Resource.Drawable.red_square);
				_roamingImage.SetImageResource(Resource.Drawable.red_square);
				_connectionType.Text = "N/A";
			}
        }
    }
}
