---
id: A522BA96-61C4-684A-BBE8-48A8812F9973
title: "Display An Image"
brief: "This recipe shows how to display an image using an ImageView."
article:
  - title: "MultiResolution Sample" 
    url: /samples/monodroid/MultiResolution/
sdk:
  - title: "ImageView Class Reference" 
    url: http://developer.android.com/reference/android/widget/ImageView.html
---

<a name="Recipe" class="injected"></a>


# Recipe

 [ ![](Images/DisplayImage.png)](Images/DisplayImage.png)

<ol>
  <li>Create a new Xamarin.Android application named DisplayAnImage.</li>
  <li>Add two images named *sample1.png* and *sample2.png* respectively under the Resources -&gt; drawable folder in your IDE.</li>
  <li>Replace the contents of Main.axml with the following:</li>
</ol>

```
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
    android:orientation="vertical"
    android:layout_width="fill_parent"
    android:layout_height="fill_parent"&gt;
    &lt;Button
        android:id="@+id/myButton"
        android:layout_width="fill_parent"
        android:layout_height="wrap_content"
        android:text="@string/changeImage"/&gt;  
    &lt;ImageView
        android:layout_width="fill_parent"
        android:layout_height="fill_parent"
        android:id="@+id/demoImageView"
        android:src="@drawable/sample1"
        android:scaleType="fitCenter"/&gt;
&lt;/LinearLayout&gt;
```

<ol start="4">
  <li>Add the following string to Strings.xml:</li>
</ol>


```
<string name="changeImage">Change Image</string>
```

<ol start="5">
  <li>In MainActivity.cs, add code to set the <code>ImageView</code>’s image in the <code>button.Click</code> event handler.</li>
</ol>


```
button.Click += delegate {
       var imageView =
              FindViewById<ImageView> (Resource.Id.demoImageView);
       imageView.SetImageResource (Resource.Drawable.sample2);
}            };
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

The `ImageView` class allows you to display an image either declaratively in
XML using the `android:src` attribute, or in code by calling `SetImageResource`.

