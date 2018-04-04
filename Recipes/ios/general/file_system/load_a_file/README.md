---
id: 9EC744C2-E5A3-EC37-E413-1C8981E0F843
title: "Load a File"
brief: "This recipe shows how to load a file that you have bundled with your Xamarin.iOS application."
---

<a name="Recipe" class="injected"></a>


# Recipe

To load a file that is bundled in a Xamarin.iOS application:

-  Add the file to your Xamarin.iOS project and ensure the `Build Action` is set to BundleResource. The sample code has a file called **ReadMe.txt** in the **TestData** folder.
-  Load the file’s contents into a variable using `ReadAllText`. Notice the path that is passed to the method is relative to the project’s root.


```
var text = System.IO.File.ReadAllText("TestData/ReadMe.txt");
```

-  Do something with the text, such as write to the Console or show in a `UITextView`:


```
Console.WriteLine(text);
txtView.Text = text;
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

Remember that the iOS file system is case-sensitive (although the simulator
is not). Ensure that you type file and directory names correctly, otherwise your app may fail when running on a physical device.

