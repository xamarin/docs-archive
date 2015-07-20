using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Geocode
{
	public partial class GeocodePage : ContentPage
	{
		Geocoder geoCoder;

		public GeocodePage ()
		{
			InitializeComponent ();
			geoCoder = new Geocoder ();
		}

		async void OnGeocodeButtonClicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace (inputEntry.Text)) {
				var address = inputEntry.Text;
				var approximateLocations = await geoCoder.GetPositionsForAddressAsync (address);
				foreach (var position in approximateLocations) {
					geocodedOutputLabel.Text += position.Latitude + ", " + position.Longitude + "\n";
				}
			}
		}
	}
}