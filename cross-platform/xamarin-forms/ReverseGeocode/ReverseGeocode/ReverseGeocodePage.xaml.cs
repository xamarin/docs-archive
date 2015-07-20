using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ReverseGeocode
{
	public partial class ReverseGeocodePage : ContentPage
	{
		Geocoder geoCoder;

		public ReverseGeocodePage ()
		{
			InitializeComponent ();
			geoCoder = new Geocoder ();
		}

		async void OnReverseGeocodeButtonClicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace (inputEntry.Text)) {
				var coordinates = inputEntry.Text.Split (',');
				double? latitude = Convert.ToDouble (coordinates.FirstOrDefault ());
				double? longitude = Convert.ToDouble (coordinates.Skip (1).FirstOrDefault ());

				if (latitude != null && longitude != null) {
					var position = new Position (latitude.Value, longitude.Value);
					var possibleAddresses = await geoCoder.GetAddressesForPositionAsync (position);
					foreach (var address in possibleAddresses)
						reverseGeocodedOutputLabel.Text += address + "\n";
				}
			}
		}
	}
}
