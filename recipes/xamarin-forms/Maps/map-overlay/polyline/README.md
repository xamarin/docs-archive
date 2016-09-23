---
id: fbfdc715-1654-4188-82a0-fc522548bcff
title: Highlight a Route on a Map
subtitle: How to add a polyline overlay to a map
brief: This recipe shows how to add a polyline overlay to a map. A polyline overlay is a series of connected line segments that are typically used to show a route on a map, or form any shape that's required.
samplecode: [Browse on Github](https: //github.com/xamarin/recipes/tree/master/cross-platform/xamarin-forms/Maps/MapOverlay/Polyline/)
article: [Customizing a Map](/guides/xamarin-forms/custom-renderer/map/)
api: [Xamarin.Forms.Maps](/api/namespace/Xamarin.Forms.Maps/)
dateupdated: 2016-04-27
---

# Overview

An overlay is a layered graphic on a map. Overlays support drawing graphical content that scales with the map as it is zoomed. The following screenshots show the result of adding a polyline overlay to a map: 

![](Images/screenshots.png)

When a [`Map`](/api/type/Xamarin.Forms.Maps.Map/) control is rendered by a Xamarin.Forms application, in iOS the `MapRenderer` class is instantiated, which in turn instantiates a native `MKMapView` control. On the Android platform, the `MapRenderer` class instantiates a native `MapView` control. On the Universal Windows Platform (UWP), the `MapRenderer` class instantiates a native `MapControl`. The rendering process can be taken advantage of to implement platform-specific map customizations by creating a custom renderer for a `Map` on each platform. The process for doing this is as follows: 

1. [Create](#Creating_the_Custom_Map) a Xamarin.Forms custom map.
1. [Consume](#Consuming_the_Custom_Map) the custom map from Xamarin.Forms.
1. [Customize](#Customizing_the_Map) the map by creating a custom renderer for the map on each platform.

<div class="note"><p><a href="/api/namespace/Xamarin.Forms.Maps/"><code>Xamarin.Forms.Maps</code></a> must be initialized and configured before use. For more information, see <a href="/guides/xamarin-forms/user-interface/map/"><code>Maps Control</code></a>.</p></div>

For information about customizing a map using a custom renderer, see [Customizing a Map](/guides/xamarin-forms/custom-renderer/map/).

## Creating the Custom Map

Create a subclass of the [`Map`](/api/type/Xamarin.Forms.Maps.Map/) class, that adds a `RouteCoordinates` property: 

```
public class CustomMap :  Map
{
  public List<Position> RouteCoordinates { get; set; }

  public CustomMap ()
  {
    RouteCoordinates = new List<Position> ();
  }
}
```

The `RouteCoordinates` property will store a collection of coordinates that define the route to be highlighted.

## Consuming the Custom Map

Consume the `CustomMap` control by declaring an instance of it in the XAML page instance: 

```
<ContentPage xmlns="http: //xamarin.com/schemas/2014/forms"
			 xmlns: x="http: //schemas.microsoft.com/winfx/2009/xaml"
			 xmlns: local="clr-namespace: MapOverlay;assembly=MapOverlay"
			 x: Class="MapOverlay.MapPage">
	<ContentPage.Content>
		<local: CustomMap x: Name="customMap" MapType="Street" WidthRequest="{x: Static local: App.ScreenWidth}" HeightRequest="{x: Static local: App.ScreenHeight}" />
	</ContentPage.Content>
</ContentPage>
```

Alternatively, consume the `CustomMap` control by declaring an instance of it in the C# page instance: 

```
public class MapPageCS :  ContentPage
{
	public MapPageCS ()
	{
		var customMap = new CustomMap {
			MapType = MapType.Street,
			WidthRequest = App.ScreenWidth,
			HeightRequest = App.ScreenHeight
		};
        ...
		Content = customMap;
	}
}
```

Initialize the `CustomMap` control as required: 

```
public partial class MapPage :  ContentPage
{
  public MapPage ()
  {
    ...
    customMap.RouteCoordinates.Add (new Position (37.785559, -122.396728));
    customMap.RouteCoordinates.Add (new Position (37.780624, -122.390541));
    customMap.RouteCoordinates.Add (new Position (37.777113, -122.394983));
    customMap.RouteCoordinates.Add (new Position (37.776831, -122.394627));

    customMap.MoveToRegion (MapSpan.FromCenterAndRadius (new Position (37.79752, -122.40183), Distance.FromMiles (1.0)));
  }
}
```

This initialization specifies a series of latitude and longitude coordinates in order to define the route on the map to be highlighted. It then positions the map's view with the [`MoveToRegion`](/api/member/Xamarin.Forms.Maps.Map.MoveToRegion(Xamarin.Forms.Maps.MapSpan)/) method, which changes the position and zoom level of the map by creating a [`MapSpan`](/api/type/Xamarin.Forms.Maps.MapSpan/) from a [`Position`](/api/type/Xamarin.Forms.Maps.Position/) and a [`Distance`](/api/type/Xamarin.Forms.Maps.Distance/).

## Customizing the Map

A custom renderer must now be added to each application project in order to add the polyline overlay to the map.

### Creating the Custom Renderer on iOS

Create a subclass of the `MapRenderer` class and override its `OnElementChanged` method in order to add the polyline overlay: 

```
[assembly: ExportRenderer (typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapOverlay.iOS
{
	public class CustomMapRenderer :  MapRenderer
	{
		MKPolylineRenderer polylineRenderer;

		protected override void OnElementChanged (ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null) {
				var nativeMap = Control as MKMapView;
				nativeMap.OverlayRenderer = null;
			}

			if (e.NewElement != null) {
				var formsMap = (CustomMap)e.NewElement;
				var nativeMap = Control as MKMapView;

				nativeMap.OverlayRenderer = GetOverlayRenderer;

				CLLocationCoordinate2D[] coords = new CLLocationCoordinate2D[formsMap.RouteCoordinates.Count];

				int index = 0;
				foreach (var position in formsMap.RouteCoordinates) {
					coords [index] = new CLLocationCoordinate2D (position.Latitude, position.Longitude);
					index++;
				}

				var routeOverlay = MKPolyline.FromCoordinates (coords);
				nativeMap.AddOverlay (routeOverlay);
			}
		}
        ...
	}
}
```

This method performs the following configuration, provided that the custom renderer is attached to a new Xamarin.Forms element: 

- The `MKMapView.OverlayRenderer` property is set to a corresponding delegate.
- The collection of latitude and longitude coordinates are retrieved from the `CustomMap.RouteCoordinates` property and stored as an array of `CLLocationCoordinate2D` instances.
- The polyline is created by calling the static `MKPolyline.FromCoordinates` method, which specifies the latitude and longitude of each point.
- The polyline is added to the map by calling the `MKMapView.AddOverlay` method.

Then, implement the `GetOverlayRenderer` method in order to customize the rendering of the overlay: 

```
public class CustomMapRenderer :  MapRenderer
{
  MKPolylineRenderer polylineRenderer;
  ...

  MKOverlayRenderer GetOverlayRenderer (MKMapView mapView, IMKOverlay overlay)
  {
    if (polylineRenderer == null) {
      polylineRenderer = new MKPolylineRenderer (overlay as MKPolyline);
      polylineRenderer.FillColor = UIColor.Blue;
      polylineRenderer.StrokeColor = UIColor.Red;
      polylineRenderer.LineWidth = 3;
      polylineRenderer.Alpha = 0.4f;
    }
    return polylineRenderer;
  }
}
```

### Creating the Custom Renderer on Android

Create a subclass of the `MapRenderer` class and override its `OnElementChanged` method in order to add the polyline overlay: 

```
[assembly: ExportRenderer (typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapOverlay.Droid
{
	public class CustomMapRenderer :  MapRenderer, IOnMapReadyCallback
	{
		GoogleMap map;
		List<Position> routeCoordinates;

		protected override void OnElementChanged (Xamarin.Forms.Platform.Android.ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null) {
				// Unsubscribe
			}

			if (e.NewElement != null) {
				var formsMap = (CustomMap)e.NewElement;
				routeCoordinates = formsMap.RouteCoordinates;

				((MapView)Control).GetMapAsync (this);
			}
		}
        ...
	}
}

```

This method retrieves the collection of latitude and longitude coordinates from the `CustomMap.RouteCoordinates` property and stores them in a member variable. It then calls the `MapView.GetMapAsync` method, which gets the underlying `GoogleMap` that is tied to the view, provided that the custom renderer is attached to a new Xamarin.Forms element. Once the `GoogleMap` instance is available, the `OnMapReady` method will be invoked, with the `IOnMapReadyCallback` interface specifying that this method must be provided: 

```
public class CustomMapRenderer :  MapRenderer, IOnMapReadyCallback
{
  GoogleMap map;
  List<Position> routeCoordinates;
  ...

  public void OnMapReady (GoogleMap googleMap)
  {
    map = googleMap;

    var polylineOptions = new PolylineOptions ();
    polylineOptions.InvokeColor (0x66FF0000);

    foreach (var position in routeCoordinates) {
      polylineOptions.Add (new LatLng (position.Latitude, position.Longitude));
    }

    map.AddPolyline (polylineOptions);
  }
}
```

The polyline is created by instantiating a `PolylineOptions` object that specifies the latitude and longitude of each point. The polyline is then added to the map by calling the `GoogleMap.AddPolyline` method.



### Creating the Custom Renderer on the Universal Windows Platform

Create a subclass of the `MapRenderer` class and override its `OnElementChanged` method in order to add the polyline overlay: 

```
[assembly:  ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapOverlay.UWP
{
    public class CustomMapRenderer :  MapRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                var nativeMap = Control as MapControl;

                var coordinates = new List<BasicGeoposition>();
                foreach (var position in formsMap.RouteCoordinates)
                {
                    coordinates.Add(new BasicGeoposition() { Latitude = position.Latitude, Longitude = position.Longitude });
                }

                var polyline = new MapPolyline();
                polyline.StrokeColor = Windows.UI.Color.FromArgb(128, 255, 0, 0);
                polyline.StrokeThickness = 5;
                polyline.Path = new Geopath(coordinates);
                nativeMap.MapElements.Add(polyline);
            }
        }
    }
}
```

This method performs the following operations, provided that the custom renderer is attached to a new Xamarin.Forms element: 

- The collection of latitude and longitude coordinates are retrieved from the `CustomMap.RouteCoordinates` property and converted into a `List` of `BasicGeoposition` coordinates.
- The polyline is created by instantiating a `MapPolyline` object. The `MapPolygon` class is used to display a line on the map by setting its `Path` property to a `Geopath` object that contains the line coordinates.
- The polyline is rendered on the map by adding it to the `MapControl.MapElements` collection.

# Summary

This recipe showed how to add a polyline overlay to a map in order to show a route on a map, or form any shape that's required.