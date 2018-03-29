---
id: 414D4465-3214-A78A-5AF2-CF6B37471AB5
title: "Download a File"
brief: "This recipe shows how to download a text file using WebClient in Xamarin.iOS."
article:
  - title: "Download an Image" 
    url: https://github.com/xamarin/recipes/tree/master/Recipes/ios/network/web_requests/download_an_image
---

<a name="Recipe" class="injected"></a>


# Recipe

To download a text file follow these steps:

-  Add a new using statements to your code:


```
using System.Net;
using System.IO;
using System.Text;
```

-  Create a WebClient object:


```
var webClient = new WebClient();
```

-  Add an event handler that will execute when the download is complete. First it retrieves the downloaded text to a variable, then it creates a local file path and finally it saves the file to local storage:


```
webClient.DownloadStringCompleted += (s, e) => {
    var text = e.Result; // get the downloaded text
    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
    string localFilename = "downloaded.txt";
    string localPath = Path.Combine (documentsPath, localFilename);
    File.WriteAllText (localpath, text); // writes to local storage
};
```

-  Create the Url for the file to download:


```
var url = new Uri("http://xamarin.com"); // Html home page
```

-  Set the encoding to match the expected encoding of the file”


```
webClient.Encoding = Encoding.UTF8;
```

-  Finally trigger the download itself (when complete, the handler we defined in step 3 will execute):


```
webClient.DownloadStringAsync(url);
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

 [ ![](Images/Downloaded.png)](Images/Downloaded.png)

The sample code looks like this screenshot when the file has been
successfully downloaded. The alert and text view are populated by adding the
following code to the completion handler defined in Step 3:

```
InvokeOnMainThread (() => {
    textView.Text = text;
    new UIAlertView ("Done", "File downloaded and saved", null, "OK", null).Show();
});
```

Notice that all interactions with UI controls is done within an
`InvokeOnMainThread` method call. This is because the file download occurs
asynchronously (in a background thread), and background threads aren’t allowed
to access the UI directly.

