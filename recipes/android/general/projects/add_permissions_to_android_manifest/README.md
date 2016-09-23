id:{88F5827B-0B37-5B34-8685-E4D55B53E6B3}  
title:Add Permissions to Android Manifest  
brief:This recipe shows how to add permissions to the Android Manifest (Manifest.xml).  

<a name="Recipe" class="injected"></a>

# Recipe

To edit Android Manifest permissions for your project:
<ide name="vs">
  <ol>
    <li>Right-click on your android project and select <span class="UIItem">Properties</span>.</li>
    <li>Select <span class="UIItem">Android Manifest</span> in the window that opens.</li>
    <li>Check the permissions that you want to require in the list of permissions.</li>
  </ol>
  <img src="Images/vis.png" />
</ide>

<ide name="xs">
  <ol>
    <li>Right-click on your android project and select <span class="UIItem">Options</span>.</li>
    <li>Select <span class="UIItem">Android Application</span> in the window that opens.</li>
    <li>Check the permissions that you want to require in the list of permissions.</li>
  </ol>
  <img src="Images/xam.png" />
</ide>

<a name="Additional_Information" class="injected"></a>


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
