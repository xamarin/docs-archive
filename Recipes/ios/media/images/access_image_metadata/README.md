---
id: F459CB11-570C-F3EB-6445-F825AE580E44
title: "Access Image Metadata"
brief: "This recipe shows how to access image properties such as height, width, DPI, EXIF data, etc."
article:
  - title: "Save Photo to Album with Metadata" 
    url: https://github.com/xamarin/recipes/tree/master/Recipes/ios/media/video_and_photos/save_photo_to_album_with_metadata
sdk:
  - title: "Accessing image properties with ImageIO" 
    url: http://developer.apple.com/library/ios/#qa/qa1654/_index.html
---

<a name="Recipe" class="injected"></a>


# Recipe

To access the properties of an image file:

1. Add these using statements to your code:

```
using Foundation;
using UIKit;
using CoreFoundation;
using ImageIO;
```

<ol start="2">
  <li>Create an <code>NSUrl</code> to the image file location:</li>
</ol>
```
var url = new NSUrl("myPhoto.JPG", false);  // could be an NSUrl to asset lib...
```

<ol start="3">
  <li>Access the retrieved properties by specifying options from the <code>CGImageProperties</code> enumeration:</li>
</ol>

```
CGImageSource myImageSource;
myImageSource = CGImageSource.FromUrl (url, null);
var ns = new NSDictionary();
var imageProperties = myImageSource.CopyProperties(ns, 0);
var width = imageProperties[CGImageProperties.PixelWidth];
var height = imageProperties[CGImageProperties.PixelHeight];
Console.WriteLine("Dimensions: {0}x{1}", width, height);
```

<ol start="4">
  <li>Some values exist in sub-dictionaries, such as GPS, TIFF and EXIF properties. To access those properties first retrieve the relevant <code>NSDictionary</code> and then retrieve the properties from it:</li>
</ol>

```
var gps = imageProperties.ObjectForKey(CGImageProperties.GPSDictionary) as NSDictionary;
var lat = gps[CGImageProperties.GPSLatitude];
var latref = gps[CGImageProperties.GPSLatitudeRef];
var lon = gps[CGImageProperties.GPSLongitude];
var lonref = gps[CGImageProperties.GPSLongitudeRef];
var loc = String.Format ("GPS: {0} {1}, {2} {3}", lat, latref, lon, lonref);
Console.WriteLine(loc);
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

For debugging you can easily output all the image metadata using the
`DescriptionInStringsFileFormat` option:

```
Console.WriteLine(imageProperties.DescriptionInStringsFileFormat);
```

Sample output is shown here:

```
"{TIFF}" = {
    DateTime = "2012:08:12 09:39:19";
    Make = SONY;
    Model = "DSC-TX100V             ";
    Orientation = 1;
    ResolutionUnit = 2;
    XResolution = 72;
    YResolution = 72;
    "_YCbCrPositioning" = 2;
};
"ColorModel" = "RGB";
"{GPS}" = {
    Differential = 0;
    GPSVersion =     (
        2,
        3
    );
    ImgDirection = "106.5";
    ImgDirectionRef = M;
    MapDatum = "WGS-84";
    Status = V;
};
"DPIWidth" = 72;
"Depth" = 8;
"Orientation" = 1;
"DPIHeight" = 72;
"PixelHeight" = 3456;
"PixelWidth" = 4608;
"{Exif}" = {
    BrightnessValue = "7.915625";
    ColorSpace = 1;
    ComponentsConfiguration =     (
        1,
        2,
        3,
        0
    );
    CompressedBitsPerPixel = 1;
    Contrast = 0;
    CustomRendered = 0;
    DateTimeDigitized = "2012:08:12 09:39:19";
    DateTimeOriginal = "2012:08:12 09:39:19";
    DigitalZoomRatio = 1;
    ExifVersion =     (
        2,
        3
    );
    ExposureBiasValue = 0;
    ExposureMode = 0;
    ExposureProgram = 8;
    ExposureTime = "0.005";
    FNumber = "3.5";
    Flash = 16;
    FlashPixVersion =     (
        1,
        0
    );
    FocalLength = "4.43";
    ISOSpeedRatings =     (
        125
    );
    LensSpecification =     (
        "4.43",
        "17.7",
        "3.5",
        "4.6"
    );
    LightSource = 0;
    MaxApertureValue = "3.625";
    MeteringMode = 5;
    PixelXDimension = 4608;
    PixelYDimension = 3456;
    Saturation = 0;
    SceneCaptureType = 1;
    Sharpness = 0;
    WhiteBalance = 0;
};
```

