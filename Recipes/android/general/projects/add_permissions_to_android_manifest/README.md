---
id: 88F5827B-0B37-5B34-8685-E4D55B53E6B3
title: "Add Permissions to Android Manifest"
brief: "This recipe shows how to add permissions to the Android Manifest (Manifest.xml)."
---


# Recipe

To edit Android Manifest permissions for your project:

## Visual Studio

1.  Right-click on your android project and select <span class="UIItem">**Properties**</span>.
2.  Select <span class="UIItem">**Android Manifest**</span> in the window that opens.
3.  Check the permissions that you want to require in the list of permissions.

![](Images/vis.png)

## Visual Studio for Mac

1.  Right-click on your android project and select <span class="UIItem">**Options**</span>.
2.  Select <span class="UIItem">**Android Application**</span> in the window that opens.
3.  Check the permissions that you want to require in the list of permissions.

![](Images/xam.png)



# Additional Information

You should only request permissions that your application requires to run.
Users will be prompted to allow these permissions when they download your
application from the Google Play Store.

Common permissions include:

 `INTERNET` – for accessing network resources.

 `ACCESS_COARSE_LOCATION and ACCESS_FINE_LOCATION` – for location
services.

 `CAMERA` – to access the camera.

There are many more permissions relating to contacts, phone operations,
operating system information, time and alarm settings and hardware features.

