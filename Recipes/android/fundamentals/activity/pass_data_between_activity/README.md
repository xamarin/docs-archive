---
id: 9057932F-7CFB-FBF7-C558-DB8B8BEED81C
title: "Passing Data Between Activities"
brief: "This recipe shows how to use intents to pass data between activities."
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

To pass data to an activity, follow these steps.

1.  Create a new Xamarin.Android application. The project template will create a single activity named  `MainActivity` (MainActivity.cs), which contains a button.
2.  Add a second activity class named  `Activity2` to the project. This class must inherit from  `Android.App.Activity`.
3.  From the  `button.Click` handler in  `MainActivity.cs`, create an intent for  `Activity2`, and add data to the intent by calling  `PutExtra`.


```
button.Click += delegate {
       var activity2 = new Intent (this, typeof(Activity2));
       activity2.PutExtra ("MyData", "Data from Activity1");
       StartActivity (activity2);
};
```

<ol start="4">
  <li>In <code>Activity2.OnCreate</code>, retrieve the data by calling <code>Intent.GetStringExtra</code>.</li>
</ol>


```
string text = Intent.GetStringExtra ("MyData") ?? "Data not available";
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

Each screen in an application is represented by an activity. Sending
asynchronous messages called intents, which can include data payloads, as shown
in this recipe, starts activities. For more information, see the [Getting Started](https://developer.xamarin.com/guides/android/getting_started) series and the [Activity Lifecycle](https://developer.xamarin.com/guides/android/application_fundamentals/activity_lifecycle) in the Xamarin.Android documentation.

