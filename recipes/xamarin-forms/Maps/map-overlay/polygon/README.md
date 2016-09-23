---
id:e79eb2cf-8dd6-44a8-b47d-5f0a94fb0a63
title:Highlight a Region on a Map
subtitle:How to add a polygon overlay to a map
brief:This recipe shows how to add a polygon overlay to a map in order to highlight a region on the map. Polygons are a closed shape and have their interiors filled in.
samplecode:[Browse on Github](https://github.com/xamarin/recipes/tree/master/cross-platform/xamarin-forms/Maps/MapOverlay/Polygon/)
article:[Customizing a Map](/guides/xamarin-forms/custom-renderer/map/)
api:[Xamarin.Forms.Maps](/api/namespace/Xamarin.Forms.Maps/)
dateupdated:2016-04-27
---

# Overview

An overlay is a layered graphic on a map. Overlays support drawing graphical content that scales with the map as it is zoomed. The following screenshots show the result of adding a polygon overlay to a map:

![](Images/screenshots.png)

When a [`Map`](/api/type/Xamarin.Forms.Maps.Map/) control is rendered by a Xamarin.Forms application, in iOS the `MapRenderer` class is instantiated, which in turn instantiates a native `MKMapView` control. On the Android platform, the `MapRenderer` class instantiates a native `MapView` control. On the Universal Windows Platform (UWP), the `MapRenderer` class instantiates a native `MapControl`. The rendering process can be taken advantage of to implement platform-specific map customizations by creating a custom renderer for a `Map` on each platform. The process for doing this is as follows:

1. [Create](#Creating_the_Custom_Map) a Xamarin.Forms custom map.
1. [Consume](#Consuming_the_Custom_Map) the custom map from Xamarin.Forms.
1. [Customize](#Customizing_the_Map) the map by creating a custom renderer for the map on each platform.

<div class="note"><p><a href="/api/namespace/Xamarin.Forms.Maps/"><code>Xamarin.Forms.Maps</code></a> must be initialized and configured before use. For more information, see <a href="/guides/xamarin-forms/user-interface/map/"><code>Maps Control</code></a>.</p></div>

For information about customizing a map using a custom renderer, see [Customizing a Map](/guides/xamarin-forms/custom-renderer/map/).

## Creating the Custom Map

Create a subclass of the [`Map`](/api/type/Xamarin.Forms.Maps.Map/) class, that adds a `ShapeCoordinates` property:

```
public class CustomMap : Map
{
  public List<Position> ShapeCoordinates { get; set; }

  public CustomMap ()
  {
    ShapeCoordinates = new List<Position> ();
  }
}
```

The `ShapeCoordinates` property will store a collection of coordinates that define the region to be highlighted.

## Consuming the Custom Map

Consume the `CustomMap` control by declaring an instance of it in the XAML page instance:

```
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
			 xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
			 xmlns:local="clr-namespace:MapOverlay;assembly=MapOverlay"
			 x:Class="MapOverlay.MapPage">
	<ContentPage.Content>
		<local:CustomMap x:Name="customMap" MapType="Street" WidthRequest="{x:Static local:App.ScreenWidth}" HeightRequest="{x:Static local:App.ScreenHeight}" />
	</ContentPage.Content>
</ContentPage>
```

Alternatively, consume the `CustomMap` control by declaring an instance of it in the C# page instance:

```
public class MapPageCS : ContentPage
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
public partial class MapPage : ContentPage
{
  public MapPage ()
  {
    ...
    customMap.ShapeCoordinates.Add (new Position (37.797513, -122.402058));
    customMap.ShapeCoordinates.Add (new Position (37.798433, -122.402256));
    customMap.ShapeCoordinates.Add (new Position (37.798582, -122.401071));
    customMap.ShapeCoordinates.Add (new Position (37.797658, -122.400888));

    customMap.MoveToRegion (MapSpan.FromCenterAndRadius (new Position (37.79752, -122.40183), Distance.FromMiles (0.1)));
  }
}
```

This initialization specifies a series of latitude and longitude coordinates in order to define the region of the map to be highlighted. It then positions the map's view with the [`MoveToRegion`](/api/member/Xamarin.Forms.Maps.Map.MoveToRegion(Xamarin.Forms.Maps.MapSpan)/) method, which changes the position and zoom level of the map by creating a [`MapSpan`](/api/type/Xamarin.Forms.Maps.MapSpan/) from a [`Position`](/api/type/Xamarin.Forms.Maps.Position/) and a [`Distance`](/api/type/Xamarin.Forms.Maps.Distance/).

## Customizing the Map

A custom renderer must now be added to each application project in order to add the polygon overlay to the map.

### Creating the Custom Renderer on iOS

Create a subclass of the `MapRenderer` class and override its `OnElementChanged` method in order to add the polygon overlay:

```
[assembly:ExportRenderer (typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapOverlay.iOS
{
	public class CustomMapRenderer : MapRenderer
	{
		MKPolygonRenderer polygonRenderer;

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

				CLLocationCoordinate2D[] coords = new CLLocationCoordinate2D[formsMap.ShapeCoordinates.Count];

				int index = 0;
				foreach (var position in formsMap.ShapeCoordinates) {
					coords [index] = new CLLocationCoordinate2D (position.Latitude, position.Longitude);
					index++;
				}

				var blockOverlay = MKPolygon.FromCoordinates (coords);
				nativeMap.AddOverlay (blockOverlay);
			}
		}
        ...
	}
}
```

This method performs the following configuration, provided that the custom renderer is attached to a new Xamarin.Forms element:

- The `MKMapView.OverlayRenderer` property is set to a corresponding delegate.
- The collection of latitude and longitude coordinates are retrieved from the `CustomMap.ShapeCoordinates` property and stored as an array of `CLLocationCoordinate2D` instances.
- The polygon is created by calling the static `MKPolygon.FromCoordinates` method, which specifies the latitude and longitude of each point.
- The polygon is added to the map by calling the `MKMapView.AddOverlay` method. This method automatically closes the polygon by drawing a line that connects the first and last points.

Then, implement the `GetOverlayRenderer` method in order to customize the rendering of the overlay:

```
public class CustomMapRenderer : MapRenderer
{
  MKPolygonRenderer polygonRenderer;
  ...

  MKOverlayRenderer GetOverlayRenderer (MKMapView mapView, IMKOverlay overlay)
  {
    if (polygonRenderer == null) {
      polygonRenderer = new MKPolygonRenderer (overlay as MKPolygon);
      polygonRenderer.FillColor = UIColor.Red;
      polygonRenderer.StrokeColor = UIColor.Blue;
      polygonRenderer.Alpha = 0.4f;
      polygonRenderer.LineWidth = 9;
    }
    return polygonRenderer;
  }
}
```

### Creating the Custom Renderer on Android

Create a subclass of the `MapRenderer` class and override its `OnElementChanged` method in order to add the polygon overlay:

```
[assembly:ExportRenderer (typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapOverlay.Droid
{
	public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
	{
		GoogleMap map;
		List<Position> shapeCoordinates;

		protected override void OnElementChanged (Xamarin.Forms.Platform.Android.ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null) {
				// Unsubscribe
			}

			if (e.NewElement != null) {
				var formsMap = (CustomMap)e.NewElement;
				shapeCoordinates = formsMap.ShapeCoordinates;

				((MapView)Control).GetMapAsync (this);
			}
		}
        ...
	}
}
```

This method retrieves the collection of latitude and longitude coordinates from the `CustomMap.ShapeCoordinates` property and stores them in a member variable. It then calls the `MapView.GetMapAsync` method, which gets the underlying `GoogleMap` that is tied to the view, provided that the custom renderer is attached to a new Xamarin.Forms element. Once the `GoogleMap` instance is available, the `OnMapReady` method will be invoked, with the `IOnMapReadyCallback` interface specifying that this method must be provided:

```
public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
{
  GoogleMap map;
  List<Position> shapeCoordinates;
  ...

  public void OnMapReady (GoogleMap googleMap)
  {
    map = googleMap;

    var polygonOptions = new PolygonOptions ();
    polygonOptions.InvokeFillColor (0x66FF0000);
    polygonOptions.InvokeStrokeColor (0x660000FF);
    polygonOptions.InvokeStrokeWidth (30.0f);

    foreach (var position in shapeCoordinates) {
      polygonOptions.Add (new LatLng (position.Latitude, position.Longitude));
    }

    map.AddPolygon (polygonOptions);
  }
}
```

The polygon is created by instantiating a `PolygonOptions` object that specifies the latitude and longitude of each point. The polygon is then added to the map by calling the `GoogleMap.AddPolygon` method. This method automatically closes the polygon by drawing a line that connects the first and last points.

### Creating the Custom Renderer on the Universal Windows Platform

Create a subclass of the `MapRenderer` class and override its `OnElementChanged` method in order to add the polygon overlay:

```
[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapOverlay.UWP
{
    public class CustomMapRenderer : MapRenderer
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
                foreach (var position in formsMap.ShapeCoordinates)
                {
                    coordinates.Add(new BasicGeoposition() { Latitude = position.Latitude, Longitude = position.Longitude });
                }

                var polygon = new MapPolygon();
                polygon.FillColor = Windows.UI.Color.FromArgb(128, 255, 0, 0);
                polygon.StrokeColor = Windows.UI.Color.FromArgb(128, 0, 0, 255);
                polygon.StrokeThickness = 5;
                polygon.Path = new Geopath(coordinates);
                nativeMap.MapElements.Add(polygon);
            }
        }
    }
}
```

This method performs the following operations, provided that the custom renderer is attached to a new Xamarin.Forms element:

- The collection of latitude and longitude coordinates are retrieved from the `CustomMap.ShapeCoordinates` property and converted into a `List` of `BasicGeoposition` coordinates.
- The polygon is created by instantiating a `MapPolygon` object. The `MapPolygon` class is used to display a multi-point shape on the map by setting its `Path` property to a `Geopath` object that contains the shape coordinates.
- The polygon is rendered on the map by adding it to the `MapControl.MapElements` collection. Note that the polygon will be automatically closed by drawing a line that connects the first and last points.

# Summary

This recipe showed how to add a polygon overlay to a map in order to highlight a region of the map. Polygons are a closed shape and have their interiors filled in.