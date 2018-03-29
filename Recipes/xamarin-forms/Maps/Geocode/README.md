---
id: 5995F84C-8039-49D9-BEA6-7BF74D91C45E
title: "Geocode a street address"
subtitle: "How to convert a street address to latitude and longitude coordinates"
brief: "This recipe shows how to geocode a user supplied street address to latitude and longitude coordinates by using the `Geocoder` class included in Xamarin.Forms.Maps."
article:
  - title: "Working with Maps in Xamarin.Forms" 
    url: https://developer.xamarin.com/guides/xamarin-forms/working-with/maps/
api:
  - title: "Geocoder" 
    url: https://developer.xamarin.com/api/type/Xamarin.Forms.Maps.Geocoder/
---

# Overview

The Xamarin.Forms.Maps NuGet package is used to add maps to a Xamarin.Forms app, and uses the native map APIs on each platform. This NuGet package provides the `Geocoder` class that converts between string addresses and latitude and longitudes.

> ℹ️ **Note**: By using the native map APIs on each platform Xamarin.Forms.Maps provides a fast, familiar maps experience for users, but means that some configuration steps are required to adhere to each platforms specific API requirements. For information about these configuration steps see [Working with Maps in Xamarin.Forms](https://developer.xamarin.com/guides/xamarin-forms/working-with/maps/).

## Geocoding a street address

In the code building the user interface for a page, import the Xamarin.Forms.Maps namespace and create an instance of the `Geocoder` class.

```
using Xamarin.Forms.Maps;
// ...
Geocoder geoCoder;

public GeocodePage ()
{
  InitializeComponent ();
  geoCoder = new Geocoder ();
}
```

Then for a user supplied street address call the `GetPositionsForAddressAsync` method on the `Geocoder` instance to asynchronously get a list of positions for an address.

```
var address = inputEntry.Text;
var approximateLocations = await geoCoder.GetPositionsForAddressAsync (address);
foreach (var position in approximateLocations) {
    geocodedOutputLabel.Text += position.Latitude + ", " + position.Longitude + "\n";
```

# Summary

This recipe shows how to geocode a user supplied street address to latitude and longitude coordinates by using by the `Geocoder` class included in Xamarin.Forms.Maps.

