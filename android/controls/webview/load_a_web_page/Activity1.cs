using Android.App;
using Android.OS;
using Android.Webkit;
using Android.Views;  // Webkit required for WebView

namespace LoadWebPage {
    [Activity(Label = "LoadWebPage", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Get our webview and load the local file in for display
            WebView webView = FindViewById<WebView>(Resource.Id.LocalWebView);
			webView.SetWebViewClient (new WebViewClient ());
			webView.LoadUrl("http://www.xamarin.com");

			// Some websites will require Javascript to be enabled
			webView.Settings.JavaScriptEnabled = true;
//			webView.LoadUrl("http://youtube.com");

            // allow zooming/panning            
            webView.Settings.BuiltInZoomControls = true;
            webView.Settings.SetSupportZoom(true);
            
            // scrollbar stuff            
            webView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay; 
            // so there's no 'white line'            
            webView.ScrollbarFadingEnabled = false;
        }
    }
}


