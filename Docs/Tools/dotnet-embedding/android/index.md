---
title: ".NET Embedding on Android"
ms.prod: xamarin
ms.assetid: EB2F967A-6D95-4448-994B-6D5C7BFAC2C7
author: davidortinau
ms.author: daortin
ms.date: 06/15/2018
---

# .NET Embedding on Android

In some cases, you may want to add a Xamarin .NET library to an
existing native Android project. To do this, you can use the
[Embeddinator-4000](https://www.nuget.org/packages/Embeddinator-4000/)
tool to turn your .NET library into a native library that can be
incorporated into a native Java-based Android app.

# [Visual Studio](#tab/windows)

## Xamarin.Android Requirements

For Xamarin.Android to work with .NET Embedding, you need the following:

- **Xamarin.Android** &ndash;
    [Xamarin.Android 7.5](https://visualstudio.microsoft.com/xamarin/)
    or later must be installed.

- **Android Studio** &ndash;
    [Android Studio 3.x](https://developer.android.com/studio/) or
    later must be installed.

- **Java Developer Kit** &ndash;
    [Java 1.8](https://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html)
    or later must be installed.

## Using Embeddinator-4000

To consume a .NET library in a native Android project, use the
following steps:

1. Create a C# Android Library project.

2. Install [Embeddinator-4000](https://www.nuget.org/packages/Embeddinator-4000/).

3. Locate **Embeddinator-4000.exe** and add it to your **PATH**. For example:

    ```cmd
    set PATH=%PATH%;C:\Users\USERNAME\.nuget\packages\embeddinator-4000\0.4.0\tools
    ```

4. Run Embeddinator-4000 on the library assembly. For example:

    ```cmd
    Embeddinator-4000.exe -gen=Java -out=foo Xamarin.Foo.dll
    ```

5. Use the generated AAR file in a Java project in Android Studio.

# [Visual Studio for Mac](#tab/macos)

## Xamarin.Android Requirements

For Xamarin.Android to work with .NET Embedding, you need the following:

- **Xamarin.Android** &ndash;
    [Xamarin.Android 7.5](https://visualstudio.microsoft.com/xamarin/)
    or later must be installed.

- **Android Studio** &ndash;
    [Android Studio 3.x](https://developer.android.com/studio/) or
    later must be installed.

- **Java Developer Kit** &ndash;
    [Java 1.8](https://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html)
    or later must be installed.

- **Mono** &ndash;
    [Mono 5.0](https://www.mono-project.com/download/) or later must be
    installed (mono is installed with Visual Studio for Mac).

## Using Embeddinator-4000

To consume a .NET library in a native Android project, use the following steps:

1. Create a C# Android Library project.

2. Install [Embeddinator-4000](https://www.nuget.org/packages/Embeddinator-4000/).

3. Locate **Embeddinator-4000.exe** and add **mono** to your path. For example:

    ```bash
    export TOOLS=~/.nuget/packages/embeddinator-4000/0.4.0/tools
    export PATH=$PATH:/Library/Frameworks/Mono.framework/Commands
    ```

4. Run Embeddinator-4000 on the library assembly. For example:

    ```bash
    mono $TOOLS/Embeddinator-4000.exe -gen=Java -out=foo Xamarin.Foo.dll
    ```

5. Use the generated AAR file in a Java project in Android Studio.

-----

Usage and command line options are described in the
[Embeddinator-4000](https://github.com/mono/Embeddinator-4000/blob/master/Usage.md#java--c)
documentation.

## Callbacks

Learn about [making calls between C# and Java](callbacks.md).

## Samples

- [Weather sample app](https://github.com/jamesmontemagno/embeddinator-weather)
