---
title: "Inspector Installation and Requirements"
description: "This document describes how to install the Xamarin Inspector and discusses supported operating system, IDEs, and app platforms."
ms.prod: xamarin
ms.assetid: 81174493-02D3-4FF5-AD57-04F3288A7F94
author: davidortinau
ms.author: daortin
ms.date: 06/19/2018
---

# Inspector Installation and Requirements

## Download and Installation

# [Windows](#tab/windows)

1. Download and install [Visual Studio Enterprise](https://visualstudio.microsoft.com/vs/)
   and select the **Mobile development with .NET** workload.
1. [Sign in](/visualstudio/ide/signing-in-to-visual-studio)
   to enable your Enterprise subscription.
1. [Inspect](~/tools/inspector/inspect.md) your own app!

# [macOS](#tab/macos)

1. Download and install [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/).
1. [Sign in](/visualstudio/mac/activation)
   to enable your Enterprise subscription.
1. [Inspect](~/tools/inspector/inspect.md) your own app!

-----

## Requirements

### Supported Operating Systems

- **Mac** - OS X 10.11 or greater
- **Windows** - Windows 7 or greater (with Internet Explorer 11 or greater and
  .NET 4.6.1 or greater)

### Supported IDEs

- Visual Studio for Mac
- Visual Studio 2017 with **Mobile development with .NET** workload

Live app inspection is available for enterprise customers.

<a name="supported-platforms"></a>

### Supported App Platforms

|App Platform|IDE Support|Notes|
|--- |--- |--- |
|Mac|Only supported in Visual Studio for Mac|
|iOS|Supported in Visual Studio 2017 and Visual Studio for Mac| Linker behavior must be set to [**Don't Link**](~/ios/deploy-test/linker.md) (under **iOS Build** Project options) |
|Android|Supported in Visual Studio 2017 and Visual Studio for Mac|Must target Android >= 4.0.3, with **fastdev** enabled.<br />Must use Google, Visual Studio, or Xamarin Android emulators. Android 7 emulators may not allow inspection at this time.|
|WPF|Only supported in Visual Studio 2017|

<a name="reporting-bugs"></a>

## Reporting Bugs

Bugs should be reported directly via Visual Studio:

- **Help > Send Feedback > Report a Problem**

Please include all of the following information:

### Platform Version Information

This information is vital.

Visual Studio For Mac

- **Visual Studio > About Visual Studio > Show Details > Copy Information**
- Paste into bug report

Visual Studio

- **Help > About Visual Studio > Copy Info**
- Let us know your Operating System version and whether you are running 32-bit or 64-bit Windows.

### Log Files

Always attach both IDE and Inspector client log files.

Inspector client

- Mac: `~/Library/Logs/Xamarin/Inspector/Xamarin Inspector {date}.log`
- Windows: `%LOCALAPPDATA%\Xamarin\Inspector\logs\Xamarin Inspector {date}.log`

1.4.x also features the ability to select the log file in Finder (macOS) or
Explorer (Windows) directly from the main menu:

- **Help > Reveal Log File**

Visual Studio For Mac

- `~/Library/Logs/VisualStudio/7.0/Ide.log`

Visual Studio

- `%LOCALAPPDATA%\Xamarin\Logs\{VS version}\Inspector {date}.log`
- The contents of the Visual Studio **Output** pane may also be informative.

### Project Settings

If you can attach the **.csproj** for the project you are trying to inspect,
it would be extremely helpful. This is easier than asking about individual settings.

Also please confirm that you are in Debug configuration.

### Selected Devices

For Android and iOS, it is vital that we know what device you are debugging on when
you want to inspect. We need to know:

- Name of device as shown in your IDE
- OS version of your device
- Android: Verify that you are using an x86 emulator
- Android: What emulator platform are you using? Google Emulator? Visual Studio Android Emulator? Xamarin Android Player?
- Does the app you are debugging properly appear and function in the device?
- Does the device have network connectivity (check via web browser)?

[client-bugs]: https://github.com/Microsoft/workbooks/issues/new