---
id: E43B67D6-25DA-87AF-1046-E9ACB6FE4110
title: "Selecting a Gallery Image"
brief: "This recipe shows how to read an image from the gallery and display it in an ImageView."
sdk:
  - title: "Activity onActivtyResult" 
    url: http://developer.android.com/reference/android/app/Activity.html
---

<a name="Recipe" class="injected"></a>


# Recipe

 [ ![](Images/PickGalleryImage.png)](Images/PickGalleryImage.png)

1. Add an ImageView to Main.axml:

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
    android:text="Pick an image from the gallery"/&gt;
 &lt;ImageView
    android:id="@+id/myImageView"
    android:layout_width="wrap_content"
    android:layout_height="wrap_content"/&gt;
&lt;/LinearLayout&gt;</code></pre>
```
<ol start="2">
  <li>In an Activity subclass, create an Intent with a MIME type set to “image/*” and an action set to <code>ActionGetContent</code>. Pass the intent to a
  <code>StartActivityForResult</code> method call.</li>
</ol>

```
button.Click += delegate {
    var imageIntent = new Intent ();
    imageIntent.SetType ("image/*");
    imageIntent.SetAction (Intent.ActionGetContent);
    StartActivityForResult (
        Intent.CreateChooser (imageIntent, "Select photo"), 0);
};
```
<ol start="3">
  <li>Override <code>OnActivityResult</code> and set the image Uri of the <code>ImageView</code> to the
  Uri of the selected image.</li>
</ol>

```
protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
{
    base.OnActivityResult (requestCode, resultCode, data);

    if (resultCode == Result.Ok) {
        var imageView =
            FindViewById&lt;ImageView&gt; (Resource.Id.myImageView);
        imageView.SetImageURI (data.Data);
    }
}
```
Note that no image is shown initially until it is selected from the gallery. Once selected, the image appears on the screen as shown above.

 <a name="Additional_Information" class="injected"></a>


# Additional Information

The Data property of the Intent returned to OnActivityResult will contain the
Uri of the selected image. We check the result in case the user cancelled the
selection.

