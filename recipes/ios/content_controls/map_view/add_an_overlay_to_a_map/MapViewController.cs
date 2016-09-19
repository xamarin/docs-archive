using System;
using CoreGraphics;
using CoreLocation;
using MapKit;
using UIKit;

namespace MapView {

	public class MapViewController : UIViewController {
		
		MKMapView mapView;
		UISegmentedControl mapTypes;
		
		MKCircle circleOverlay;
		MKCircleRenderer circleRenderer;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			Title = "Pyramids of Giza";
			
			mapView = new MKMapView(View.Bounds);	
			mapView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;		
			View.AddSubview(mapView);

			var coords = new CLLocationCoordinate2D(29.976111, 31.132778); // pyramids of giza, egypt
			var span = new MKCoordinateSpan(MilesToLatitudeDegrees(.75), MilesToLongitudeDegrees(.75, coords.Latitude));
			// set the coords and zoom on the map
			mapView.MapType = MKMapType.Satellite;
			mapView.Region = new MKCoordinateRegion(coords, span);

			mapView.OverlayRenderer = (m, o) => {
				if(circleRenderer == null)
				{
					circleRenderer = new MKCircleRenderer(o as MKCircle);
					circleRenderer.FillColor = UIColor.Purple;
					circleRenderer.Alpha = 0.5f;
				}
				return circleRenderer;
			};

			circleOverlay = MKCircle.Circle (coords, 400);
			mapView.AddOverlay (circleOverlay);

			#region Not related to this sample
			int typesWidth=260, typesHeight=30, distanceFromBottom=60;
			mapTypes = new UISegmentedControl(new CGRect((View.Bounds.Width-typesWidth)/2, View.Bounds.Height-distanceFromBottom, typesWidth, typesHeight));
			mapTypes.InsertSegment("Road", 0, false);
			mapTypes.InsertSegment("Satellite", 1, false);
			mapTypes.InsertSegment("Hybrid", 2, false);
			mapTypes.SelectedSegment = 1; // Road is the default
			mapTypes.AutoresizingMask = UIViewAutoresizing.FlexibleTopMargin;
			mapTypes.ValueChanged += (s, e) => {
				switch(mapTypes.SelectedSegment) {
				case 0:
					mapView.MapType = MKMapType.Standard;
					break;
				case 1:
					mapView.MapType = MKMapType.Satellite;
					break;
				case 2:
					mapView.MapType = MKMapType.Hybrid;
					break;
				}
			};
			View.AddSubview(mapTypes);
			#endregion
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