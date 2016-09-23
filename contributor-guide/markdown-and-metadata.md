Markdown and Metadata
=====================

All guides should be written in markdown, as specified via [Daring Fireball](https://daringfireball.net/projects/markdown/).

Some useful markdown that you may wish to use is shown below:

Zoomable images (where you click to see a more detailed, larger version) 

`[ ![](Images/small.png)](Images/large.png)`

Metadata
--------

Metadata is placed at the top of on your document, and should be written following YAML guidelines. 

The very minimum amount of data that needs to be added is the follows: 

* id: {Any GUID}  
* title: The title of your document 
* brief: A short paragraph explaining what the recipe is about. This is useful for users that land on your recipes through a search engine 

Other _optional_ metadata: 

* article: [name](urn) Link to other relevant Xamarin guides. 
* video: [name](urn) Link to relevant videos 
* sample: [name](urn) Link to Xamarin samples. 
* api: [name](urn) Link to Xamarin API docs 
* link: [name](urn) Link to any relevant external page 

### Resource/Related Item Example 

Each resource or related item can have multiple entries with the same name, with absolute links to particular content. Each item is required to be in markdown link format `[Name](URN)`

URN - This is an link to content. Where possible, try and link to the original content on developer.xamarin.com.

Name - This is the friendly name of the item as it will be shown to the user. 
For example, the following snippet illustrates two related articles links: 

article:[Introduction to Mobile Developments](/ios/guides/getting-started/introduction-to-mobile-development) 
Link:[Channel 9](https://channel9.msdn.com/)