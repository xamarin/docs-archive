Markdown and Metadata
=====================

All guides should be written in [GitHub Flavored Markdown](https://github.github.com/gfm/).

Metadata
--------

Metadata is placed at the top of on your document, and should be written following YAML guidelines. 

The very minimum amount of data that needs to be added is the follows: 

* id: {Any GUID}  
* title: The title of your document 
* brief: A short paragraph explaining what the recipe is about. This is useful for users that land on your recipes through a search engine 

Other _optional_ metadata: 

* article:
* video:
* sample:
* api:
* link:

For example:

```
---
id: 30D6D826-11A0-30FA-D781-077C26EA28D4
title: "Enumerate Directories"
brief: "This recipe shows how to list directories in the iOS file system using Xamarin.iOS."
samplecode:
  - title: XAML Basics
    url: /link/to/sample/
  - title: another sample
    url: /another/link/here
api:  
  - title: Xamarin.Forms  
    url: /api/namespace/Xamarin.Forms/

---
```