---
title: "Getting started with Objective-C"
description: "This document describes how get started using .NET Embedding with Objective-C. It discusses requirements, installing .NET Embedding from NuGet, and supported platforms."
ms.prod: xamarin
ms.assetid: 4ABC0247-B608-42D4-89CB-D2E598097142
author: davidortinau
ms.author: daortin
ms.date: 11/14/2017
---

# Getting started with Objective-C

This is the getting started page for Objective-C, which covers the basics for all supported platforms.

## Requirements

To use .NET Embedding with Objective-C, you'll need a Mac running:

- macOS 10.12 (Sierra) or later
- Xcode 8.3.2 or later
- [Mono 5.0](https://www.mono-project.com/download/)

You can install [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/) to edit and compile your C# code.

> [!NOTE]
>
> - Earlier versions of macOS, Xcode, and Mono _might_ work, but are
>   untested and unsupported
> - Code generation can be done on Windows, but it is only possible to
>   compile it on a Mac computer where Xcode is installed

## Installing .NET Embedding from NuGet

Follow these [instructions](~/tools/dotnet-embedding/get-started/install/install.md) to install and configure .NET Embedding for your project.

A sample command invocation is listed in the [macOS](~/tools/dotnet-embedding/get-started/objective-c/macos.md)
and 
[iOS](~/tools/dotnet-embedding/get-started/objective-c/ios.md) getting started guides.

## Platforms

Objective-C is a language that is most commonly used to write applications for macOS, iOS, tvOS and watchOS; .NET Embedding supports all of those platforms. Working with each platform implies some [key differences and these are explained here](~/tools/dotnet-embedding/objective-c/platforms.md).

### macOS

[Creating a macOS application](~/tools/dotnet-embedding/get-started/objective-c/macos.md) is easiest since it does not involve as many additional steps, like setting up identity, provisining profiles, simulators and devices. You are encouraged to start with the macOS document before the one for iOS.

### iOS / tvOS

Please make sure you are already set up to develop iOS applications before trying to create one using .NET Embedding. The [following instructions](~/tools/dotnet-embedding/get-started/objective-c/ios.md) assume that you have already successfully built and deployed an iOS application from your computer.

Support for tvOS is analogous to how iOS works, by just using tvOS projects in the IDEs (both Visual Studio and Xcode) instead of iOS projects.

> [!NOTE]
> Support for watchOS will be available in a future release and will be
> very similar to iOS/tvOS.

## Further reading

- [.NET Embedding features specific to Objective-C](~/tools/dotnet-embedding/objective-c/index.md)
- [Best Practices for Objective-C](~/tools/dotnet-embedding/objective-c/best-practices.md)
- [.NET Embedding Limitations](~/tools/dotnet-embedding/limitations.md)
- [Contributing to the open source project](https://github.com/mono/Embeddinator-4000/blob/master/Contributing.md)
- [Error codes and descriptions](~/tools/dotnet-embedding/errors.md)
- [Target platforms](~/tools/dotnet-embedding/objective-c/platforms.md)

## Related links

- [Weather Sample (iOS & macOS)](https://github.com/jamesmontemagno/embeddinator-weather)
