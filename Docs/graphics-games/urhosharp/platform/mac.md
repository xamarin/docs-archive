---
title: "UrhoSharp Mac support"
description: "This document discusses macOS support for UrhoSharp. It describes how to create a project and provides a link to some sample code."
ms.prod: xamarin
ms.assetid: 95FFBD36-14E9-4C17-B1E8-9A04E81E824D
author: conceptdev
ms.author: crdun
ms.date: 03/29/2017
---
# UrhoSharp Mac support

_Mac Specific Setup and Features_

While Urho is a portable class library, and allows the same API to be
used across the various platform for your game logic, you still need
to initialize Urho in your platform specific driver, and in some
cases, you will want to take advantage of platform specific features.

In the pages below, assume that `MyGame` is a subclass of the
`Application` class.

## macOS

**Supported architectures:** x86/x86-64 for 32 bit and 64 bit.

## Creating a project

Create a console project, reference the Urho NuGet and then make sure
that you can locate the assets (the directories containing the Data
directory).

```csharp
DesktopUrhoInitializer.AssetsDirectory = "../Assets";
new MyGame().Run();
```

## Example

[Complete example](https://github.com/xamarin/urho-samples/tree/master/FeatureSamples/Cocoa)
