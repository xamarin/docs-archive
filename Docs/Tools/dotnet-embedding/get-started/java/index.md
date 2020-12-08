---
title: "Getting started with Java"
description: "This document describes how to get started using .NET Embedding with Java. It discusses system requirements, installation, and supported platforms."
ms.prod: xamarin
ms.assetid: B9A25E9B-3EC2-489A-8AD3-F78287609747
author: davidortinau
ms.author: daortin
ms.date: 03/28/2018
---

# Getting started with Java

This is the getting started page for Java, which covers the basics for all supported platforms.

## Requirements

To use .NET Embedding with Java you will need:

* Java 1.8 or later
* [Mono 5.0](https://www.mono-project.com/download/)

For Mac:

* Xcode 8.3.2 or later

For Windows:

* Visual Studio 2017 with C++ support
* Windows 10 SDK

For Android:

* [Xamarin.Android 7.5](https://visualstudio.microsoft.com/xamarin/) or later
* [Android Studio 3.x](https://developer.android.com/studio/index.html) with Java 1.8

You can use [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/) to edit and compile your C# code.

> [!NOTE]
> Earlier versions of Xcode, Visual Studio, Xamarin.Android, Android Studio, and Mono _might_ work, but are untested and unsupported.

## Installation

.NET Embedding is currently available on [NuGet](https://www.nuget.org/packages/Embeddinator-4000/):

```shell
nuget install Embeddinator-4000
```

This will place **Embeddinator-4000.exe** into the **packages/Embeddinator-4000/tools** directory.

Additionally, you can build .NET Embedding from source, see our [git repository](https://github.com/mono/Embeddinator-4000/) and the [contributing](https://github.com/mono/Embeddinator-4000/blob/master/Contributing.md) document for instructions.

## Platforms

Java is currently in a preview state for macOS, Windows, and Android.

The platform is selected by passing the `--platform=<platform>` command-line argument to the .NET Embedding tool. Currently `macOS`, `Windows`, and `Android` are supported.

### macOS and Windows

For development, you should be able to use any Java IDE that supports Java 1.8. You can even use Android Studio for this if desired, [see here](https://stackoverflow.com/questions/16626810/can-android-studio-be-used-to-run-standard-java-projects). You can use the JAR file output as you would any standard Java jar file.

### Android

Please make sure you are already set up to develop Android applications before trying to create one using .NET Embedding. The [following instructions](~/tools/dotnet-embedding/get-started/java/android.md) assume that you have already successfully built and deployed an Android application from your computer.

Android Studio is recommended for development, but other IDEs should work as long as there is support for the [AAR file format](https://developer.android.com/studio/projects/android-library.html).

## Further reading

* [Getting Started on Android](~/tools/dotnet-embedding/get-started/java/android.md)
* [Callbacks on Android](~/tools/dotnet-embedding/android/callbacks.md)
* [Preliminary Android Research](~/tools/dotnet-embedding/android/index.md)
* [.NET Embedding Limitations](~/tools/dotnet-embedding/limitations.md)
* [Contributing to the open source project](https://github.com/mono/Embeddinator-4000/blob/master/Contributing.md)
* [Error codes and descriptions](~/tools/dotnet-embedding/errors.md)
