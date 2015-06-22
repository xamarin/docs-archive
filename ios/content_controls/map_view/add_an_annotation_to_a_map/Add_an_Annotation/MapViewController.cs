using System;
using CoreGraphics;
using CoreLocation;
using MapKit;
using UIKit;

namespace MapView {

	public class MapViewController : UIViewController {
		
		MKMapView mapView;
		UISegmentedControl mapTypes;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			Title = "MapView Annotation";
			
			mapView = new MKMapView(View.Bounds);	
			mapView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			View.AddSubview(mapView);
			
			// create our location and zoom for los angeles
			var coords = new CLLocationCoordinate2D(48.857, 2.351); // paris
			var span = new MKCoordinateSpan(MilesToLatitudeDegrees (2), MilesToLongitudeDegrees (2, coords.Latitude));

			// set the coords and zoom on the map
			mapView.Region = new MKCoordinateRegion (coords, span);
			
			// add a basic annotation
			var annotation = new BasicMapAnnotation (new CLLocationCoordinate2D (48.857, 2.351), "Paris", "City of Light");
			mapView.AddAnnotation (annotation);


			#region Not related to this sample
			int typesWidth=260, typesHeight=30, distanceFromBottom=60;
			mapTypes = new UISegmentedControl(new CGRect((View.Bounds.Width-typesWidth)/2, View.Bounds.Height-distanceFromBottom, typesWidth, typesHeight));
			mapTypes.InsertSegment("Road", 0, false);
			mapTypes.InsertSegment("Satellite", 1, false);
			mapTypes.InsertSegment("Hybrid", 2, false);
			mapTypes.SelectedSegment = 0; // Road is the default
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


		protected class BasicMapAnnotation : MKAnnotation
		{
			/// <summary>
			/// The location of the annotation
			/// </summary>
			private CLLocationCoordinate2D coord;
			protected string title;
			protected string subtitle;
			public override CLLocationCoordinate2D Coordinate { get { return coord; } }


			/// <summary>
			/// The title text
			/// </summary>
			public override string Title
			{ get { return title; } }
			
			
			/// <summary>
			/// The subtitle text
			/// </summary>
			public override string Subtitle
			{ get { return subtitle; } }
			
			public BasicMapAnnotation (CLLocationCoordinate2D coordinate, string title, string subTitle)
				: base()
			{
				this.coord = coordinate;
				this.title = title;
				this.subtitle = subTitle;
			}
		}
	}
}