using System;
using System.Net;
using Xamarin.Forms;

namespace MapNavigation
{
    public partial class MapNavigationPage : ContentPage
    {
        public MapNavigationPage()
        {
            InitializeComponent();
        }

        void OnNavigateButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(inputEntry.Text))
            {
                var address = inputEntry.Text;
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        Device.OpenUri(
                            new Uri(string.Format("http://maps.apple.com/?q={0}", WebUtility.UrlEncode(address))));
                        break;
                    case Device.Android:
                        Device.OpenUri(
                            new Uri(string.Format("geo:0,0?q={0}", WebUtility.UrlEncode(address))));
                        break;
                    case Device.Windows:
                    case Device.WinPhone:
                        Device.OpenUri(
                            new Uri(string.Format("bingmaps:?where={0}", Uri.EscapeDataString(address))));
                        break;
                }
            }
        }
    }
}

