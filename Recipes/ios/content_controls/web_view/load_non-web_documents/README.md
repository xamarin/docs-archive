---
id: 5747077E-47CC-CAED-B07F-48668FBA0AAF
title: "Load Non-Web Documents"
brief: "This recipe shows how to load a web page in a UIWebView control."
sdk:
  - title: "UIWebView Class Reference" 
    url: https://developer.apple.com/library/ios/#documentation/UIKit/Reference/UIWebView_Class/Reference/Reference.html
  - title: "Using UIWebView to display select document types" 
    url: https://developer.apple.com/library/ios/#qa/qa1630/_index.html
---

<a name="Recipe" class="injected"></a>


# Recipe

To show a document type other than HTML in a UIWebView:

<ol>
  <li>Add the document (for example, a PDF) to your Xamarin.iOS project. Set the Build Action to <strong>BundleResource</strong>. You can set the build action for a file by right-clicking on that file and and choosing Build Action in the menu that opens.</li>
</ol>
<ol start="2"><li>Create a <code>UIWebView</code> and add it to a view:</li></ol>


```
webView = new UIWebView (View.Bounds);
View.AddSubview(webView);
```

<ol start="3"><li>Load the file using <code>NSUrl</code> and <code>NSUrlRequest</code> classes: </li></ol>


```
string fileName = "Loading a Web Page.pdf"; // remember case-sensitive
string localDocUrl = Path.Combine (NSBundle.MainBundle.BundlePath, fileName);
webView.LoadRequest(new NSUrlRequest(new NSUrl(localDocUrl, false)));
webView.ScalesPageToFit = true;
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

iOS can display the following document types:

-  Excel (.xls &amp; .xlsx)
-  Keynote (.key.zip)
-  Numbers (.numbers.zip)
-  Pages (.pages.zip)
-  PDF (.pdf)
-  PowerPoint (.ppt &amp; .pptx)
-  Rich Text Format (.rtf)
-  Word (.doc &amp; .docx)


See [Technical Q&amp;A 1630](https://developer.apple.com/library/ios/#qa/qa1630/_index.html) for details on other supported file
types.

