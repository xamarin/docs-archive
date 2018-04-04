---
id: D6D866A3-95F9-9005-A018-A6A1345D5D5C
title: "Save Documents"
brief: "This recipe shows how to save a text file to the Documents folder."
article:
  - title: "Load a File" 
    url: https://github.com/xamarin/recipes/tree/master/Recipes/ios/general/file_system/load_a_file
  - title: "Enumerate Directories" 
    url: https://github.com/xamarin/recipes/tree/master/Recipes/ios/general/file_system/enumerate_directories
  - title: "Load an Image" 
    url: https://github.com/xamarin/recipes/tree/master/Recipes/ios/standard_controls/image_view/load_an_image
---

<a name="Recipe" class="injected"></a>


# Recipe

To save a string value to a text file

<ol><li>Make sure you add a using statement for <code>System.IO</code>.</li></ol>


```
using System.IO;
```

<ol start="2"><li>Determine the path for the **Documents** folder:</li></ol>


```
var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
```

<ol start="3"><li>Construct a filename to save the document to:</li></ol>


```
var filename = Path.Combine (documents, "Write.txt");
```

<ol start="4"><li>Write to the file:</li></ol>


```
File.WriteAllText(filename, "Write this text into a file!");
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

 <a name="iTunes_File_Access" class="injected"></a>


## iTunes File Access

Users can access files in the Documents folder via iTunes if you add the
following key to your **Info.plist** file:

```
<key>UIFileSharingEnabled</key>  <true />
```

When the device is plugged in to iTunes, the Documents folder is accessible
for users to upload or download files (they can also download directories, but
they cannot “browse” directories via the iTunes user interface).

 <a name="Read_the_File" class="injected"></a>


## Read the File

To read the same text file from the Documents folder:

<ol start="1"><li>Determine the path for the Documents folder:</li></ol>


```
var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
```

<ol start="2"><li>Construct the filename to read from:</li></ol>


```
var filename = Path.Combine (documents, "Write.txt");
```

<ol start="3"><li>Read the file’s contents into a variable:</li></ol>


```
var text = File.ReadAllText(filename);
```

