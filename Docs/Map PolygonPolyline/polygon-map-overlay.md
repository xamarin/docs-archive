---
title: "Highlighting a Region on a Map"
description: "This article explains how to add a polygon overlay to a map, to highlight a region on the map. Polygons are a closed shape and have their interiors filled in."
ms.prod: xamarin
ms.assetid: E79EB2CF-8DD6-44A8-B47D-5F0A94FB0A63
ms.technology: xamarin-forms
author: davidbritch
ms.author: dabritch
ms.date: 11/29/2017
---

# Highlighting a Region on a Map

[![Download Sample](~/media/shared/download.png) Download the sample](https://docs.microsoft.com/samples/xamarin/xamarin-forms-samples/customrenderers-map-polygon)

_This article explained how to add a polygon overlay to a map, to highlight a region on the map. Polygons are a closed shape and have their interiors filled in._

## Overview

An overlay is a layered graphic on a map. Overlays support drawing graphical content that scales with the map as it is zoomed. The following screenshots show the result of adding a polygon overlay to a map:

![](polygon-map-overlay-images/screenshots.png)

When a [`Map`](xref:Xamarin.Forms.Maps.Map) control is rendered by a Xamarin.Forms application, in iOS the `MapRenderer` class is instantiated, which in turn instantiates a native `MKMapView` control. On the Android platform, the `MapRenderer` class instantiates a native `MapView` control. On the Universal Windows Platform (UWP), the `MapRenderer` class instantiates a native `MapControl`. The rendering process can be taken advantage of to implement platform-specific map customizations by creating a custom renderer for a `Map` on each platform. The process for doing this is as follows:

1. [Create](#Creating_the_Custom_Map) a Xamarin.Forms custom map.
1. [Consume](#Consuming_the_Custom_Map) the custom map from Xamarin.Forms.
1. [Customize](#Customizing_the_Map) the map by creating a custom renderer for the map on each platform.

> [!NOTE]
> [`Xamarin.Forms.Maps`](xref:Xamarin.Forms.Maps) must be initialized and configured before use. For more information, see [`Maps Control`](~/xamarin-forms/user-interface/map/index.md).

For information about customizing a map using a custom renderer, see [Customizing a Map Pin](~/xamarin-forms/app-fundamentals/custom-renderer/map/customized-pin.md).

<a name="Creating_the_Custom_Map" />

### Creating the Custom Map

Create a subclass of the [`Map`](xref:Xamarin.Forms.Maps.Map) class, that adds a `ShapeCoordinates` property:

```csharp
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

<a name="Consuming_the_Custom_Map" />

### Consuming the Custom Map

Consume the `CustomMap` control by declaring an instance of it in the XAML page instance:

```xaml
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

```csharp
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

```csharp
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

This initialization specifies a series of latitude and longitude coordinates, to define the region of the map to be highlighted. It then positions the map's view with the [`MoveToRegion`](xref:Xamarin.Forms.Maps.Map.MoveToRegion*) method, which changes the position and zoom level of the map by creating a [`MapSpan`](xref:Xamarin.Forms.Maps.MapSpan) from a [`Position`](xref:Xamarin.Forms.Maps.Position) and a [`Distance`](xref:Xamarin.Forms.Maps.Distance).

<a name="Customizing_the_Map" />

### Customizing the Map

A custom renderer must now be added to each application project to add the polygon overlay to the map.

#### Creating the Custom Renderer on iOS

Create a subclass of the `MapRenderer` class and override its `OnElementChanged` method to add the polygon overlay:

```csharp
[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapOverlay.iOS
{
    public class CustomMapRenderer : MapRenderer
    {
        MKPolygonRenderer polygonRenderer;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null) {
                var nativeMap = Control as MKMapView;
                if (nativeMap != null) {
                    nativeMap.RemoveOverlays(nativeMap.Overlays);
                    nativeMap.OverlayRenderer = null;
                    polygonRenderer = null;
                }
            }

            if (e.NewElement != null) {
                var formsMap = (CustomMap)e.NewElement;
                var nativeMap = Control as MKMapView;

                nativeMap.OverlayRenderer = GetOverlayRenderer;

                CLLocationCoordinate2D[] coords = new CLLocationCoordinate2D[formsMap.ShapeCoordinates.Count];

                int index = 0;
                foreach (var position in formsMap.ShapeCoordinates)
                {
                    coords[index] = new CLLocationCoordinate2D(position.Latitude, position.Longitude);
                    index++;
                }

                var blockOverlay = MKPolygon.FromCoordinates(coords);
                nativeMap.AddOverlay(blockOverlay);
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

Then, implement the `GetOverlayRenderer` method to customize the rendering of the overlay:

```csharp
public class CustomMapRenderer : MapRenderer
{
  MKPolygonRenderer polygonRenderer;
  ...

  MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlayWrapper)
  {
      if (polygonRenderer == null && !Equals(overlayWrapper, null)) {
          var overlay = Runtime.GetNSObject(overlayWrapper.Handle) as IMKOverlay;
          polygonRenderer = new MKPolygonRenderer(overlay as MKPolygon) {
              FillColor = UIColor.Red,
              StrokeColor = UIColor.Blue,
              Alpha = 0.4f,
              LineWidth = 9
          };
      }
      return polygonRenderer;
  }
}
```

#### Creating the Custom Renderer on Android

Create a subclass of the `MapRenderer` class and override its `OnElementChanged` and `OnMapReady` methods to add the polygon overlay:

```csharp
[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapOverlay.Droid
{
    public class CustomMapRenderer : MapRenderer
    {
        List<Position> shapeCoordinates;

        public CustomMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                shapeCoordinates = formsMap.ShapeCoordinates;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(Android.Gms.Maps.GoogleMap map)
        {
            base.OnMapReady(map);

            var polygonOptions = new PolygonOptions();
            polygonOptions.InvokeFillColor(0x66FF0000);
            polygonOptions.InvokeStrokeColor(0x660000FF);
            polygonOptions.InvokeStrokeWidth(30.0f);

            foreach (var position in shapeCoordinates)
            {
                polygonOptions.Add(new LatLng(position.Latitude, position.Longitude));
            }
            NativeMap.AddPolygon(polygonOptions);
        }
    }
}
```

The `OnElementChanged` method retrieves the collection of latitude and longitude coordinates from the `CustomMap.ShapeCoordinates` property and stores them in a member variable. It then calls the `MapView.GetMapAsync` method, which gets the underlying `GoogleMap` that is tied to the view, provided that the custom renderer is attached to a new Xamarin.Forms element. Once the `GoogleMap` instance is available, the `OnMapReady` method will be invoked, where the polygon is created by instantiating a `PolygonOptions` object that specifies the latitude and longitude of each point. The polygon is then added to the map by calling the `NativeMap.AddPolygon` method. This method automatically closes the polygon by drawing a line that connects the first and last points.

#### Creating the Custom Renderer on the Universal Windows Platform

Create a subclass of the `MapRenderer` class and override its `OnElementChanged` method to add the polygon overlay:

```csharp
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

## Summary

This article explained how to add a polygon overlay to a map, to highlight a region of the map. Polygons are a closed shape and have their interiors filled in.

## Related Links

- [Polygon Map Overlay (sample)](https://docs.microsoft.com/samples/xamarin/xamarin-forms-samples/customrenderers-map-polygon)
- [Customizing a Map Pin](~/xamarin-forms/app-fundamentals/custom-renderer/map/customized-pin.md)
- [Xamarin.Forms.Maps](xref:Xamarin.Forms.Maps)
