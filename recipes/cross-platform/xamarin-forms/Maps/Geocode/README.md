---
id:{5995f84c-8039-49d9-bea6-7bf74d91c45e}
title:Geocode a street address
subtitle:How to convert a street address to latitude and longitude coordinates
brief:This recipe shows how to geocode a user supplied street address to latitude and longitude coordinates by using the `Geocoder` class included in Xamarin.Forms.Maps.
samplecode:[Browse on Github](https://github.com/xamarin/recipes/tree/master/cross-platform/xamarin-forms/Maps/Geocode/)  
article:[Working with Maps in Xamarin.Forms](/guides/xamarin-forms/working-with/maps/)
api:[Geocoder](http://iosapi.xamarin.com/?link=T:Xamarin.Forms.Maps.Geocoder)
---

# Overview

The Xamarin.Forms.Maps NuGet package is used to add maps to a Xamarin.Forms app, and uses the native map APIs on each platform. This NuGet package provides the `Geocoder` class that converts between string addresses and latitude and longitudes.

<div class="note">
By using the native map APIs on each platform Xamarin.Forms.Maps provides a fast, familiar maps experience for users, but means that some configuration steps are required to adhere to each platforms specific API requirements. For information about these configuration steps see <a href="http://developer.xamarin.com/guides/xamarin-forms/working-with/maps/">Working with Maps in Xamarin.Forms</a>.
</div>

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

Then for a user supplied street address call the `GetPositionsForAddressAsync` method on the `Geocoder` instance in order to asynchronously get a list of positions for an address.

```
var address = inputEntry.Text;
var approximateLocations = await geoCoder.GetPositionsForAddressAsync (address);
foreach (var position in approximateLocations) {
    geocodedOutputLabel.Text += position.Latitude + ", " + position.Longitude + "\n";
```

# Summary

This recipe shows how to geocode a user supplied street address to latitude and longitude coordinates by using by the `Geocoder` class included in Xamarin.Forms.Maps.