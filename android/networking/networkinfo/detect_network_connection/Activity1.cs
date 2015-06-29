namespace NetworkDetection
{
    using Android.App;
    using Android.Net;
    using Android.OS;
    using Android.Widget;

    [Activity(Label = "NetworkDetection", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private ImageView _isConnectedImage;
        private ImageView _roamingImage;
        private ImageView _wifiImage;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            _wifiImage = FindViewById<ImageView>(Resource.Id.wifi_image);
            _roamingImage = FindViewById<ImageView>(Resource.Id.roaming_image);
            _isConnectedImage = FindViewById<ImageView>(Resource.Id.is_connected_image);
            DetectNetwork();
        }

        private void DetectNetwork()
        {
            var connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);

            var activeConnection = connectivityManager.ActiveNetworkInfo;

            if ((activeConnection != null) && activeConnection.IsConnected)
            {
                // we are connected to a network.
                _isConnectedImage.SetImageResource(Resource.Drawable.green_square);
            }

            var mobile = connectivityManager.GetNetworkInfo(ConnectivityType.Mobile).GetState();
            if (mobile == NetworkInfo.State.Connected)
            {
                // We are connected via WiFi
                _roamingImage.SetImageResource(Resource.Drawable.green_square);
            }

            var wifiState = connectivityManager.GetNetworkInfo(ConnectivityType.Wifi).GetState();
            if (wifiState == NetworkInfo.State.Connected)
            {
                _wifiImage.SetImageResource(Resource.Drawable.green_square);
            }
        }
    }
}
