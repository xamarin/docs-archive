using System;

using CoreLocation;

namespace Geofencing
{
	public class LocationManager
	{

		// event to fire when user leaves the geofenced area, so we can communicate with the VC
		public event EventHandler<RegionChangedEventArgs> RegionLeft = delegate { };
		public event EventHandler<RegionChangedEventArgs> RegionEntered = delegate { };

		public LocationManager ()
		{
			locMgr = new CLLocationManager();
		}

		public CLLocationManager LocMgr
		{
			get { 
				return locMgr; 
			} 
		} protected CLLocationManager locMgr; 

		// We need to perform a lot of checks to make sure location data and region monitoring are available and enabled.
		// For simplicity, we're logging errors in the console.

		public void StartMonitoringRegion (CLCircularRegion region)
		{
			if (CLLocationManager.LocationServicesEnabled) {

				if (CLLocationManager.Status != CLAuthorizationStatus.Denied) {

					if (CLLocationManager.IsMonitoringAvailable (typeof(CLCircularRegion))) {

						//LocMgr.DesiredAccuracy = 1;

						LocMgr.RegionEntered += (o, e) => {
							Console.WriteLine ("Just entered " + e.Region.ToString ());
							RegionEntered (this, new RegionChangedEventArgs ((CLCircularRegion)e.Region));
						};

						LocMgr.RegionLeft += (o, e) => {
							Console.WriteLine ("Just left " + e.Region.ToString ());
							RegionLeft (this, new RegionChangedEventArgs ((CLCircularRegion)e.Region));
						};

						LocMgr.DidStartMonitoringForRegion += (o, e) => {
							Console.WriteLine ("Now monitoring region {0}", e.Region.ToString ());
						};
							
						LocMgr.StartMonitoring (region);

					} else {
						Console.WriteLine ("This app requires region monitoring, which is unavailable on this device");
					}

				} else {
					Console.WriteLine ("App is not authorized to use location data");
				}

				// Get some output from our manager in case of failure
				LocMgr.Failed += (o, e) => {
					Console.WriteLine (e.Error);
				}; 

			} else {

				//Let the user know that they need to enable LocationServices
				Console.WriteLine ("Location services not enabled, please enable this in your Settings");

			}
		}

		public void StopMonitoringRegion(CLCircularRegion region)
		{
			if (CLLocationManager.LocationServicesEnabled) {

				if (CLLocationManager.Status != CLAuthorizationStatus.Denied) {

					if (CLLocationManager.IsMonitoringAvailable (typeof(CLCircularRegion))) {

						LocMgr.StopMonitoring (region);
						Console.WriteLine ("Stopped monitoring region: {0}", region.ToString ());

					} else {

						Console.WriteLine ("This app requires region monitoring, which is unavailable on this device");
					}

					// Get some output from our manager in case of failure
					LocMgr.Failed += (o, e) => {
						Console.WriteLine (e.Error);
					}; 

				} else {
					Console.WriteLine ("App is not authorized to use location data");
				}

			} else {
				//Let the user know that they need to enable LocationServices
				Console.WriteLine ("Location services not enabled, please enable this in your Settings");
			}
		}
	}
}

