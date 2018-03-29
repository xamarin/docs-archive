---
id: B05B5D58-E25F-A234-45BF-BB9BF3604AC8
title: "Start an Activity"
brief: "This recipe shows how to start an activity defined within an application."
article:
  - title: "Activity Lifecycle" 
    url: https://developer.xamarin.com/guides/android/application_fundamentals/activity_lifecycle
  - title: "Hello, Multiscreen Applications" 
    url: https://developer.xamarin.com/guides/android/getting_started/hello,_multi-screen_applications
sdk:
  - title: "Activity Class Reference" 
    url: http://developer.android.com/reference/android/app/Activity.html
---

<a name="Recipe" class="injected"></a>


# Recipe

To start an Activity, follow these steps.

1.  Create a new Xamarin.Android application. The project template will create a single activity named  `MainActivity` (MainActivity.cs), which contains a button.
2.  Add a second activity class named  `Activity2` to the project. This class must inherit from  `Android.App.Activity`
3.  From the  `button.Click` handler in MainActivity.cs, call  `StartActivity` , passing the type of the activity to start,  `Activity2` in this case.


```
button.Click += delegate {
       StartActivity(typeof(Activity2));
};
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

Each screen in an application is represented by an activity. For more
information on activities see the [Getting Started](https://developer.xamarin.com/guides/android/getting_started) series and the [Activity Lifecycle](https://developer.xamarin.com/guides/android/application_fundamentals/activity_lifecycle) in the Xamarin.Android documentation.

