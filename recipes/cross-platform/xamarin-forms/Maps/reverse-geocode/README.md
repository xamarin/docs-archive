id:{8c78004a-54d6-4502-87fb-bd1301c1ff6f}
title:Reverse geocode a street address
subtitle:How to convert latitude and longitude coordinates to a street address
brief:This recipe shows how to reverse geocode user supplied latitude and longitude coordinates into a street address by using the `Geocoder` class included in Xamarin.Forms.Maps.
samplecode:[Browse in Github](https://github.com/xamarin/recipes/tree/master/cross-platform/xamarin-forms/Maps/ReverseGeocode/)
article:[Working with Maps in Xamarin.Forms](/guides/xamarin-forms/working-with/maps/)
api:[Geocoder](/api/type/Xamarin.Forms.Maps.Geocoder/)

# Overview

The Xamarin.Forms.Maps NuGet package is used to add maps to a Xamarin.Forms app, and uses the native map APIs on each platform. This NuGet package provides the `Geocoder` class that converts between string addresses and latitude and longitudes.

<div class="note">
By using the native map APIs on each platform Xamarin.Forms.Maps provides a fast, familiar maps experience for users, but means that some configuration steps are required to adhere to each platforms specific API requirements. For information about these configuration steps see <a href="http://developer.xamarin.com/guides/xamarin-forms/working-with/maps/">Working with Maps in Xamarin.Forms</a>.
</div>

## Reverse geocoding a street address

In the code building the user interface for a page, import the Xamarin.Forms.Maps namespace and create an instance of the `Geocoder` class.

```
using Xamarin.Forms.Maps;
// ...
Geocoder geoCoder;

public ReverseGeocodePage ()
{
  InitializeComponent ();
  geoCoder = new Geocoder ();
}
```

Then for user supplied latitude and longitude coordinates call the `GetAddressesForPositionAsync` method on the `Geocoder` instance in order to asynchronously get a list of addresses near the position.

```
var position = new Position (latitude.Value, longitude.Value);
var possibleAddresses = await geoCoder.GetAddressesForPositionAsync (position);
foreach (var address in possibleAddresses)
    reverseGeocodedOutputLabel.Text += address + "\n";
```

# Summary

This recipe shows how to reverse geocode user supplied latitude and longitude coordinates into a street address by using the `Geocoder` class included in Xamarin.Forms.Maps.
