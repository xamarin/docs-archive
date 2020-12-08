---
title: "Getting started with iOS"
description: "This document describes how to get started using .NET Embedding with iOS. It discusses requirements and presents a sample app to demonstrate how to bind a managed assembly and use the output in an Xcode project."
ms.prod: xamarin
ms.assetid: D5453695-69C9-44BC-B226-5B86950956E2
author: davidortinau
ms.author: daortin
ms.date: 11/14/2017
---

# Getting started with iOS

## Requirements

In addition to the requirements from our [Getting started with Objective-C](~/tools/dotnet-embedding/get-started/objective-c/index.md) guide, you'll also need:

* [Xamarin.iOS 10.11](https://visualstudio.microsoft.com/xamarin/) or later

## Hello world

First, build a simple hello world example in C#.

### Create C# sample

Open Visual Studio for Mac, create a new iOS Class Library project, name it **hello-from-csharp**, and save it to **~/Projects/hello-from-csharp**.

Replace the code in the **MyClass.cs** file with the following snippet:

```csharp
using UIKit;
public class MyUIView : UITextView
{
    public MyUIView ()
    {
        Text = "Hello from C#";
    }
}
```

Build the project, and the resulting assembly will be saved as **~/Projects/hello-from-csharp/hello-from-csharp/bin/Debug/hello-from-csharp.dll**.

### Bind the managed assembly

Once you have a managed assembly, bind it by invoking .NET Embedding.

As described in the
[installation](~/tools/dotnet-embedding/get-started/install/install.md)
guide, this can be done as post-build step in your project, with a
custom MSBuild target, or manually:

```shell
cd ~/Projects/hello-from-csharp
objcgen ~/Projects/hello-from-csharp/hello-from-csharp/bin/Debug/hello-from-csharp.dll --target=framework --platform=iOS --outdir=output -c --debug
```

The framework will be placed in **~/Projects/hello-from-csharp/output/hello-from-csharp.framework**.

### Use the generated output in an Xcode project

Open Xcode, create a new iOS Single View Application, name it **hello-from-csharp**, and select the **Objective-C** language.

Open the **~/Projects/hello-from-csharp/output** directory in Finder, select **hello-from-csharp.framework**, drag it to the Xcode project and drop it just above the **hello-from-csharp** folder in the project.

![Drag and drop framework](ios-images/hello-from-csharp-ios-drag-drop-framework.png)

Make sure **Copy items if needed** is checked in the dialog that pops up, and click **Finish**.

![Copy items if needed](ios-images/hello-from-csharp-ios-copy-items-if-needed.png)

Select the **hello-from-csharp** project and navigate to the **hello-from-csharp** target's **General tab**. In the **Embedded Binary** section, add **hello-from-csharp.framework**.

![Embedded binaries](ios-images/hello-from-csharp-ios-embedded-binaries.png)

Open **ViewController.m**, and replace the contents with:

```objective-c
#import "ViewController.h"
#include "hello-from-csharp/hello-from-csharp.h"

@interface ViewController ()
@end

@implementation ViewController
- (void)viewDidLoad {
    [super viewDidLoad];

    MyUIView *view = [[MyUIView alloc] init];
    view.frame = CGRectMake(0, 200, 200, 200);
    [self.view addSubview: view];
}
@end
```

.NET Embedding does not currently support bitcode on iOS, which is enabled for some Xcode project templates. 

Disable it in your project settings:

![Bitcode Option](../../images/ios-bitcode-option.png)

Finally, run the Xcode project, and something like this will show up:

![Hello from C# sample running in the simulator](ios-images/hello-from-csharp-ios.png)
