using System;
using CoreLocation;
using MapKit;
using UIKit;

namespace MapView {

	public class MapViewController : UIViewController {
		
		MKMapView mapView;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			Title = "MapView";
			
			mapView = new MKMapView(View.Bounds);	
			mapView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;		
			//mapView.MapType = MKMapType.Standard;	// this is the default
			//mapView.MapType = MKMapType.Satellite;
			//mapView.MapType = MKMapType.Hybrid;
			View.AddSubview(mapView);

			// create our location and zoom 
			//CLLocationCoordinate2D coords = new CLLocationCoordinate2D(40.77, -73.98); // new york
			//CLLocationCoordinate2D coords = new CLLocationCoordinate2D(33.93, -118.40); // los angeles
			//CLLocationCoordinate2D coords = new CLLocationCoordinate2D(51.509, -0.1); // london
			CLLocationCoordinate2D coords = new CLLocationCoordinate2D(48.857, 2.351); // paris

			MKCoordinateSpan span = new MKCoordinateSpan(MilesToLatitudeDegrees(20), MilesToLongitudeDegrees(20, coords.Latitude));
			
			// set the coords and zoom on the map
			mapView.Region = new MKCoordinateRegion(coords, span);

		}

		/// <summary>
		/// Converts miles to latitude degrees
		/// </summary>
		public double MilesToLatitudeDegrees(double miles)
		{
			double earthRadius = 3960.0;
			double radiansToDegrees = 180.0/Math.PI;
			return (miles/earthRadius) * radiansToDegrees;
		}

		/// <summary>
		/// Converts miles to longitudinal degrees at a specified latitude
		/// </summary>
		public double MilesToLongitudeDegrees(double miles, double atLatitude)
		{
			double earthRadius = 3960.0;
			double degreesToRadians = Math.PI/180.0;
			double radiansToDegrees = 180.0/Math.PI;

			// derive the earth's radius at that point in latitude
			double radiusAtLatitude = earthRadius * Math.Cos(atLatitude * degreesToRadians);
    		return (miles / radiusAtLatitude) * radiansToDegrees;
		}
	}
}