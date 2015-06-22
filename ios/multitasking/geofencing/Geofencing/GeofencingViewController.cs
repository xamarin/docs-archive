using System;

using UIKit;
using CoreLocation;

namespace Geofencing
{
	public partial class GeofencingViewController : UIViewController
	{
		CLGeocoder geocoder = new CLGeocoder ();
		CLCircularRegion region;
		CLLocationManager locMan;
		public GeofencingViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			locMan = new CLLocationManager();
			locMan.RequestWhenInUseAuthorization();
			locMan.RequestAlwaysAuthorization();
			// Geocode a city to get a CLCircularRegion,
			// and then use our location manager to set up a geofence
			button.TouchUpInside += (o, e) => {

				// clean up monitoring of old region so they don't pile up
				if(region != null)
				{
					locMan.StopMonitoring(region);
				}

				// Geocode city location to create a CLCircularRegion - what we need for geofencing!
				var taskCoding = geocoder.GeocodeAddressAsync ("Cupertino");
				taskCoding.ContinueWith ((addresses) => {
					CLPlacemark placemark = addresses.Result [0];
					region = (CLCircularRegion)placemark.Region;
					locMan.StartMonitoring(region);

				});
			};

			
			// This gets called even when the app is in the background - try it!
			locMan.RegionEntered += (sender, e) => {
				Console.WriteLine("You've entered the region");
			};

			locMan.RegionLeft += (sender, e) => {
				Console.WriteLine("You've left the region");
			};
		}

		#endregion
	}
}

