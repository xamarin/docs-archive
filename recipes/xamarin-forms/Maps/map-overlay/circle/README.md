---
id:6ff8bd15-074e-4e6a-9522-f9e2be32ef12
title:Highlight a Circular Area on a Map
subtitle:How to add a circular overlay to a map
brief:This recipe shows how to add a circular overlay to a map in order to highlight a circular area of the map.
samplecode:[Browse on Github](https://github.com/xamarin/recipes/tree/master/cross-platform/xamarin-forms/Maps/MapOverlay/Circle/)
article:[Customizing a Map](/guides/xamarin-forms/custom-renderer/map/)
api:[Xamarin.Forms.Maps](/api/namespace/Xamarin.Forms.Maps/)
dateupdated:2016-04-27
---

# Overview

An overlay is a layered graphic on a map. Overlays support drawing graphical content that scales with the map as it is zoomed. The following screenshots show the result of adding a circular overlay to a map:

![](Images/screenshots.png)

When a [`Map`](/api/type/Xamarin.Forms.Maps.Map/) control is rendered by a Xamarin.Forms application, in iOS the `MapRenderer` class is instantiated, which in turn instantiates a native `MKMapView` control. On the Android platform, the `MapRenderer` class instantiates a native `MapView` control. On the Universal Windows Platform (UWP), the `MapRenderer` class instantiates a native `MapControl`. The rendering process can be taken advantage of to implement platform-specific map customizations by creating a custom renderer for a `Map` on each platform. The process for doing this is as follows:

1. [Create](#Creating_the_Custom_Map) a Xamarin.Forms custom map.
1. [Consume](#Consuming_the_Custom_Map) the custom map from Xamarin.Forms.
1. [Customize](#Customizing_the_Map) the map by creating a custom renderer for the map on each platform.

<div class="note"><p><a href="/api/namespace/Xamarin.Forms.Maps/"><code>Xamarin.Forms.Maps</code></a> must be initialized and configured before use. For more information, see <a href="/guides/xamarin-forms/user-interface/map/"><code>Maps Control</code></a>.</p></div>

For information about customizing a map using a custom renderer, see [Customizing a Map](/guides/xamarin-forms/custom-renderer/map/).

## Creating the Custom Map

Create a `CustomCircle` class that has `Position` and `Radius` properties:

```
public class CustomCircle
{
  public Position Position { get; set; }
  public double Radius { get; set; }
}
```

Then, create a subclass of the [`Map`](/api/type/Xamarin.Forms.Maps.Map/) class, that adds a property of type `CustomCircle`:

```
public class CustomMap : Map
{
  public CustomCircle Circle { get; set; }
}
```

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
    var pin = new Pin {
      Type = PinType.Place,
      Position = new Position (37.79752, -122.40183),
      Label = "Xamarin San Francisco Office",
      Address = "394 Pacific Ave, San Francisco CA"
    };

    var position = new Position (37.79752, -122.40183);
    customMap.Circle = new CustomCircle {
      Position = position,
      Radius = 1000
    };

    customMap.Pins.Add (pin);
    customMap.MoveToRegion (MapSpan.FromCenterAndRadius (position, Distance.FromMiles (1.0)));
  }
}
```

This initialization adds [`Pin`](/api/type/Xamarin.Forms.Maps.Pin/) and `CustomCircle` instances to the custom map, and positions the map's view with the [`MoveToRegion`](/api/member/Xamarin.Forms.Maps.Map.MoveToRegion(Xamarin.Forms.Maps.MapSpan)/) method, which changes the position and zoom level of the map by creating a [`MapSpan`](/api/type/Xamarin.Forms.Maps.MapSpan/) from a [`Position`](/api/type/Xamarin.Forms.Maps.Position/) and a [`Distance`](/api/type/Xamarin.Forms.Maps.Distance/).

## Customizing the Map

A custom renderer must now be added to each application project in order to add the circular overlay to the map.

### Creating the Custom Renderer on iOS

Create a subclass of the `MapRenderer` class and override its `OnElementChanged` method in order to add the circular overlay:

```
[assembly:ExportRenderer (typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapOverlay.iOS
{
	public class CustomMapRenderer : MapRenderer
	{
		MKCircleRenderer circleRenderer;

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
				var circle = formsMap.Circle;

				nativeMap.OverlayRenderer = GetOverlayRenderer;

				var circleOverlay = MKCircle.Circle (new CoreLocation.CLLocationCoordinate2D (circle.Position.Latitude, circle.Position.Longitude), circle.Radius);
				nativeMap.AddOverlay (circleOverlay);
			}
		}
        ...
	}
}
```

This method performs the following configuration, provided that the custom renderer is attached to a new Xamarin.Forms element:

- The `MKMapView.OverlayRenderer` property is set to a corresponding delegate.
- The circle is created by setting a static `MKCircle` object that specifies the center of the circle, and the radius of the circle in meters.
- The circle is added to the map by calling the `MKMapView.AddOverlay` method.

Then, implement the `GetOverlayRenderer` method in order to customize the rendering of the overlay:

```
public class CustomMapRenderer : MapRenderer
{
  MKCircleRenderer circleRenderer;
  ...

  MKOverlayRenderer GetOverlayRenderer (MKMapView mapView, IMKOverlay overlay)
  {
    if (circleRenderer == null) {
      circleRenderer = new MKCircleRenderer (overlay as MKCircle);
      circleRenderer.FillColor = UIColor.Red;
      circleRenderer.Alpha = 0.4f;
    }
    return circleRenderer;
  }
}
```

### Creating the Custom Renderer on Android

Create a subclass of the `MapRenderer` class and override its `OnElementChanged` method in order to add the circular overlay:

```
[assembly:ExportRenderer (typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapOverlay.Droid
{
	public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
	{
		GoogleMap map;
		CustomCircle circle;

		protected override void OnElementChanged (Xamarin.Forms.Platform.Android.ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null) {
				// Unsubscribe
			}

			if (e.NewElement != null) {
				var formsMap = (CustomMap)e.NewElement;
				circle = formsMap.Circle;

				((MapView)Control).GetMapAsync (this);
			}
		}
        ...
	}
}
```

This method calls the `MapView.GetMapAsync` method, which gets the underlying `GoogleMap` that is tied to the view, provided that the custom renderer is attached to a new Xamarin.Forms element. Once the `GoogleMap` instance is available, the `OnMapReady` method will be invoked, with the `IOnMapReadyCallback` interface specifying that this method must be provided:

```
public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
{
  GoogleMap map;
  CustomCircle circle;
  ...

  public void OnMapReady (GoogleMap googleMap)
  {
    map = googleMap;

    var circleOptions = new CircleOptions ();
    circleOptions.InvokeCenter (new LatLng (circle.Position.Latitude, circle.Position.Longitude));
    circleOptions.InvokeRadius (circle.Radius);
    circleOptions.InvokeFillColor (0X66FF0000);
    circleOptions.InvokeStrokeColor (0X66FF0000);
    circleOptions.InvokeStrokeWidth (0);
    map.AddCircle (circleOptions);
  }
}
```

The circle is created by instantiating a `CircleOptions` object that specifies the center of the circle, and the radius of the circle in meters. The circle is then added to the map by calling the `GoogleMap.AddCircle` method.

### Creating the Custom Renderer on the Universal Windows Platform

Create a subclass of the `MapRenderer` class and override its `OnElementChanged` method in order to add the circular overlay:

```
[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapOverlay.UWP
{
    public class CustomMapRenderer : MapRenderer
    {
        const int EarthRadiusInMeteres = 6371000;

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
                var circle = formsMap.Circle;

                var coordinates = new List<BasicGeoposition>();
                var positions = GenerateCircleCoordinates(circle.Position, circle.Radius);
                foreach (var position in positions)
                {
                    coordinates.Add(new BasicGeoposition { Latitude = position.Latitude, Longitude = position.Longitude });
                }

                var polygon = new MapPolygon();
                polygon.FillColor = Windows.UI.Color.FromArgb(128, 255, 0, 0);
                polygon.StrokeColor = Windows.UI.Color.FromArgb(128, 255, 0, 0);
                polygon.StrokeThickness = 5;
                polygon.Path = new Geopath(coordinates);
                nativeMap.MapElements.Add(polygon);
            }
        }
        ...
    }
}
```

This method performs the following operations, provided that the custom renderer is attached to a new Xamarin.Forms element:

- The circle position and radius are retrieved from the `CustomMap.Circle` property and passed to the `GenerateCircleCoordinates` method, which generates latitude and longitude coordinates for the circle perimeter.
- The circle perimeter coordinates are converted into a `List` of `BasicGeoposition` coordinates.
- The circle is created by instantiating a `MapPolygon` object. The `MapPolygon` class is used to display a multi-point shape on the map by setting its `Path` property to a `Geopath` object that contains the shape coordinates.
- The polygon is rendered on the map by adding it to the `MapControl.MapElements` collection.

# Summary

This recipe showed how to add a circular overlay to a map in order to highlight a circular area of the map.