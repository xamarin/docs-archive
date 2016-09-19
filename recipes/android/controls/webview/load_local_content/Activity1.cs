using Android.App;
using Android.OS;
using Android.Webkit;  // Webkit required for WebView

namespace LoadLocalContent {
    [Activity (Label = "LoadLocalContent", MainLauncher = true)]
    public class Activity1 : Activity {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Get our webview and load the local file in for display
            WebView localWebView = FindViewById<WebView>(Resource.Id.LocalWebView);
            localWebView.LoadUrl("file:///android_asset/Content/Home.html");
        }
    }
}


