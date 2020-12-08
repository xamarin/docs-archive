---
title: "Getting started with macOS"
description: "This document describes how get started using .NET Embedding with macOS. It discusses requirements, and presents a sample application to demonstrate how to bind the managed assembly and use the generated output in an Xcode project."
ms.prod: xamarin
ms.assetid: AE51F523-74F4-4EC0-B531-30B71C4D36DF
author: davidortinau
ms.author: daortin
ms.date: 11/14/2017
---

# Getting started with macOS

## What you will need

* Follow instructions in the [Getting started with Objective-C](~/tools/dotnet-embedding/get-started/objective-c/index.md) guide.

## Hello world

First, build a simple hello world example in C#.

### Create C# sample

Open Visual Studio for Mac, create a new Mac Class Library project named **hello-from-csharp**, and save it to **~/Projects/hello-from-csharp**.

Replace the code in the **MyClass.cs** file with the following snippet:

```csharp
using AppKit;
public class MyNSView : NSTextView
{
    public MyNSView ()
    {
        Value = "Hello from C#";
    }
}
```

Build the project. The resulting assembly will be saved as **~/Projects/hello-from-csharp/hello-from-csharp/bin/Debug/hello-from-csharp.dll**.

### Bind the managed assembly

Once you have a managed assembly, bind it by invoking .NET Embedding.

As described in the
[installation](~/tools/dotnet-embedding/get-started/install/install.md)
guide, this can be done as post-build step in your project, with a
custom MSBuild target, or manually:

```shell
cd ~/Projects/hello-from-csharp
objcgen ~/Projects/hello-from-csharp/hello-from-csharp/bin/Debug/hello-from-csharp.dll --target=framework --platform=macOS-modern --abi=x86_64 --outdir=output -c --debug
```

The framework will be placed in **~/Projects/hello-from-csharp/output/hello-from-csharp.framework**.

### Use the generated output in an Xcode project

Open Xcode and create a new Cocoa Application. Name it **hello-from-csharp** and select the **Objective-C** language.

Open the **~/Projects/hello-from-csharp/output** directory in Finder, select **hello-from-csharp.framework**, drag it to the Xcode project and drop it just above the **hello-from-csharp** folder in the project.

![Drag and drop framework](macos-images/hello-from-csharp-mac-drag-drop-framework.png)

Make sure **Copy items if needed** is checked in the dialog that pops up, and click **Finish**.

![Copy items if needed](macos-images/hello-from-csharp-mac-copy-items-if-needed.png)

Select the **hello-from-csharp** project and navigate to the **hello-from-csharp** target's **General** tab. In the **Embedded Binary** section, add **hello-from-csharp.framework**.

![Embedded binaries](macos-images/hello-from-csharp-mac-embedded-binaries.png)

Open **ViewController.m**, and replace the contents with:

```objc
#import "ViewController.h"

#include "hello-from-csharp/hello-from-csharp.h"

@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];

    MyNSView *view = [[MyNSView alloc] init];
    view.frame = CGRectMake(0, 200, 200, 200);
    [self.view addSubview: view];
}

@end
```

Finally, run the Xcode project, and something like this will show up:

![Hello from C# sample running in the simulator](macos-images/hello-from-csharp-mac.png)
