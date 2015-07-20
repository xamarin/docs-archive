using System;
using CoreGraphics;
using CoreLocation;
using MapKit;
using UIKit;
using Foundation;

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

			// assign the delegate, which handles annotation layout and clicking
			mapView.Delegate = new MapDelegate(this);

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
		
		// The map delegate is much like the table delegate.
		protected class MapDelegate : MKMapViewDelegate
		{
			protected string annotationIdentifier = "BasicAnnotation";
			UIButton detailButton;
			MapViewController parent;

			public MapDelegate(MapViewController parent)
			{
				this.parent = parent;
			}

			/// <summary>
			/// This is very much like the GetCell method on the table delegate
			/// </summary>
			public override MKAnnotationView GetViewForAnnotation (MKMapView mapView, IMKAnnotation annotation)
			{

				// try and dequeue the annotation view
				MKAnnotationView annotationView = mapView.DequeueReusableAnnotation(annotationIdentifier);
				
				// if we couldn't dequeue one, create a new one
				if (annotationView == null)
					annotationView = new MKPinAnnotationView(annotation, annotationIdentifier);
				else // if we did dequeue one for reuse, assign the annotation to it
					annotationView.Annotation = annotation;
		     
				// configure our annotation view properties
				annotationView.CanShowCallout = true;
				(annotationView as MKPinAnnotationView).AnimatesDrop = true;
				(annotationView as MKPinAnnotationView).PinColor = MKPinAnnotationColor.Green;
				annotationView.Selected = true;
				
				// you can add an accessory view, in this case, we'll add a button on the right, and an image on the left
				detailButton = UIButton.FromType(UIButtonType.DetailDisclosure);

				detailButton.TouchUpInside += (s, e) => { 
					Console.WriteLine ("Clicked");
					//Create Alert
					var detailAlert = UIAlertController.Create ("Annotation Clicked", "You clicked on " + 
						(annotation as MKAnnotation).Coordinate.Latitude.ToString() + ", " +
						(annotation as MKAnnotation).Coordinate.Longitude.ToString(), UIAlertControllerStyle.Alert);
					detailAlert.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
					parent.PresentViewController (detailAlert, true, null); 
				};
				annotationView.RightCalloutAccessoryView = detailButton;
				
				annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromBundle("29_icon.png"));
				
				return annotationView;
			}
			
			// as an optimization, you should override this method to add or remove annotations as the 
			// map zooms in or out.
			public override void RegionChanged (MKMapView mapView, bool animated) {}
		}

		protected class BasicMapAnnotation : MKAnnotation
		{
			/// <summary>
			/// The location of the annotation
			/// </summary>
			CLLocationCoordinate2D coord; 

			public override CLLocationCoordinate2D Coordinate {
				get {
					return coord;
				}
			}

			protected string title;
			protected string subtitle;
			
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
			
			public BasicMapAnnotation (CLLocationCoordinate2D coord, string title, string subTitle)
				: base()
			{
				this.coord = coord;
				this.title = title;
				this.subtitle = subTitle;
			}
		}
	}
}