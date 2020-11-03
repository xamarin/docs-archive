---
title: "UrhoSharp iOS and tvOS Support"
description: "This document discusses iOS and tvOS support for UrhoSharp. It describes how to create a project, configure and launch Urho, and perform a custom embed of Urho."
ms.prod: xamarin
ms.assetid: 7B06567E-E789-4EA1-A2A9-F3B2212EDD23
author: conceptdev
ms.author: crdun
ms.date: 03/29/2017
---

# UrhoSharp iOS and tvOS support

While Urho is a portable class library, and allows the same API to be
used across the various platform for your game logic, you still need
to initialize Urho in your platform specific driver, and in some
cases, you will want to take advantage of platform specific features.

In the pages below, assume that `MyGame` is a sublcass of the
`Application` class.

## iOS and tvOS

**Supported architectures:** armv7, arm64, i386

## Creating a project

Create an iOS project, and then add Data to the Resources directory and make sure all files have **BundleResource** as the **Build Action**.

![Project Setup](ios-images/image-4.png "Add Data to the Resources directory")

## Configuring and launching Urho

Add using statements for the `Urho` and `Urho.iOS` namespaces, and then
add this code for initializing Urho, as well as launching your
application:

```csharp
new MyGame().Run();
```

Notice that since iOS expects `FinishedLaunching` to complete, you should queue
the call to `Run()` to run after the method completes, this is a common idiom:

```csharp
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    LaunchGame();
    return true;
}

async void LaunchGame()
{
    await Task.Yield();
    new SamplyGame().Run();
}
```

It is important that you disable PNG optimizations because the default
iOS PNG optimizer will generate images that Urho can not currently
properly consume

## Custom embedding of Urho

You can alternatively to having Urho take over the entire application
screen, and to use it as a component of your application, you can
create a `UrhoSurface` which is a `UIView` that you can embed in your
existing application.

This is what you would need to do:

```csharp
var view = new UrhoSurface () {
    Frame = new CGRect (100,100,200,200),
    BackgroundColor = UIColor.Red
}
window.AddSubview (view);
```

This will host your Urho class, so then you would do:

```csharp
new MyGame().Run ();
```
