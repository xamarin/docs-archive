---
title: "Workbooks Installation and Requirements"
description: "This document describes how to download and install Xamarin Workbooks, discussing supported platforms and system requirements."
ms.prod: xamarin
ms.assetid: 9D4E10E8-A288-4C6C-9475-02969198C119
author: davidortinau
ms.author: daortin
ms.date: 06/19/2018
---

# Workbooks Installation and Requirements

<a name="install"></a>

## Download and Install

<!-- markdownlint-disable MD001 -->

# [Windows](#tab/windows)

1. Check the [requirements](#requirements) below.
2. Download and install [Xamarin Workbooks for Windows](https://dl.xamarin.com/interactive/XamarinInteractive.msi).
3. Start [playing around](~/tools/workbooks/workbook.md) with workbooks.

# [macOS](#tab/macos)

1. Check the [requirements](#requirements) below.
2. Download and install [Xamarin Workbooks for Mac](https://dl.xamarin.com/interactive/XamarinInteractive.pkg).
3. Start [playing around](~/tools/workbooks/workbook.md).

-----

## Requirements

#### Supported Operating Systems

- **Mac** - OS X 10.11 or greater
- **Windows** - Windows 7 or greater (with Internet Explorer 11 or greater and
  .NET 4.6.1 or greater)

#### Supported App Platforms

|App Platform|OS Support|Notes|
|--- |--- |--- |
|Mac|Only supported on Mac|
|iOS|Supported on Mac and Windows|Xamarin.iOS 11.0 and Xcode 9.0 or greater must be installed on Mac. Running iOS workbooks on Windows requires a Mac build host running all of the above, and the [Remoted iOS Simulator](~/tools/ios-simulator/index.md) installed on Windows.|
|Android|Supported on Mac and Windows|Must use Google, Visual Studio or Xamarin Android emulator, with a virtual device >= 5.0|
|WPF|Only supported on Windows|
|Console (.NET Framework)|Supported on Mac and Windows|
|Console (.NET Core)|Supported on Mac and Windows|

## Reporting Bugs

Please [report issues on GitHub][bugs], and include all of the following information:

### Log Files

Always attach Workbooks client log files:

- Mac: `~/Library/Logs/Xamarin/Workbooks/Xamarin Workbooks {date}.log`
- Windows: `%LOCALAPPDATA%\Xamarin\Workbooks\logs\Xamarin Workbooks {date}.log`

1.4.x also features the ability to select the log file in Finder (macOS) or
Explorer (Windows) directly from the main menu:

- **Help > Reveal Log File**

#### Log paths for Workbooks 1.3 and earlier:

- Mac: `~/Library/Logs/Xamarin/Inspector/Xamarin Inspector {date}.log`
- Windows: `%LOCALAPPDATA%\Xamarin\Inspector\logs\Xamarin Inspector {date}.log`

### Platform Version Information

It is very helpful to know details about your Operating System and installed Xamarin products.

From the main menu in Workbooks:

- **Help > Copy Version Information**

#### Instructions for Workbooks 1.3 and earlier:

Visual Studio For Mac

- **Visual Studio > About Visual Studio > Show Details > Copy Information**
- Paste into bug report

Visual Studio

- **Help > About Visual Studio > Copy Info**
- Let us know your Operating System version and whether you are running 32-bit or 64-bit Windows.

### Samples

If you can attach or link to the **.workbooks** file you are having trouble with,
that might help solve your bug more quickly.

### Devices

If you are having trouble connecting your iOS or Android workbook, and have
already checked [our troubleshooting page](~/tools/workbooks/troubleshooting/index.md),
we'll need to know:

- Name of device you are trying to connect to
- OS version of your device
- Android: Verify that you are using an x86 emulator
- Android: What emulator platform are you using? Google Emulator?
  Visual Studio Android Emulator? Xamarin Android Player?
- iOS on Windows: What version of the Xamarin Remote iOS Simulator do you have
  installed (check **Add/Remove Programs** in **Control Panel**)?
- iOS on Windows: Please also provide Platform Version Information for your Mac
  build host
- Does the device have network connectivity (check via web browser)?

[bugs]: https://github.com/Microsoft/workbooks/issues/new

## Uninstall

### Windows

Depending on how you acquired Workbooks, you may have to perform
two uninstallation procedures. Please check both of these to completely
uninstall the software.

#### Visual Studio Installer

If you have Visual Studio 2017, open **Visual Studio Installer**, and look in
**Individual Components** for **Xamarin Workbooks**. If it is checked, uncheck it
and then click **Modify** to uninstall.

#### System Uninstall

If you installed Workbooks yourself with a downloaded installer,
it will need to be uninstalled via the **Apps & features**
system settings page on Windows 10 or via **Add/Remove Programs** in the
Control Panel on older versions of Windows.

> **Start > Settings > System > Apps & features**

![Xamarin Workbooks as listed in &quot;Apps &amp; features&quot;](install-images/windows-remove.png)

**You should still follow the procedure for the Visual Studio Installer to make
sure Workbooks does not get reinstalled without your knowledge.**

<a name="uninstall-macos"></a>

### macOS

Starting with [1.2.2](https://github.com/xamarin/release-notes-archive/blob/master/release-notes/interactive/interactive-1.2.md),
Xamarin Workbooks can be uninstalled from a terminal by running:

```bash
sudo /Library/Frameworks/Xamarin.Interactive.framework/Versions/Current/uninstall
```

The uninstaller will detail the files and directories it will remove and
ask for confirmation before proceeding.

Pass the `-help` argument to the `uninstall` script for more advanced
scenarios.

For older versions, you will need to manually remove the following:

1. Delete the Workbooks app at `"/Applications/Xamarin Workbooks.app"`
2. Delete the Inspector app at `"Applications/Xamarin Inspector.app"`
3. Delete the add-ins: `"~/Library/Application Support/XamarinStudio-6.0/LocalInstall/Addins/Xamarin.Interactive"` and `"~/Library/Application Support/XamarinStudio-6.0/LocalInstall/Addins/Xamarin.Inspector"`
4. Delete Inspector and supporting files here: `/Library/Frameworks/Xamarin.Interactive.framework` and `/Library/Frameworks/Xamarin.Inspector.framework`

## Downgrading

The bundle identifier for **/Applications/Xamarin Workbooks.app** changed from
`com.xamarin.Inspector` to `com.xamarin.Workbooks` in the 1.4 release, as
Workbooks and Inspector are now fully split.

Because of a bug in older installers, it is not possible to downgrade 1.4 or
newer releases using the 1.3.2 or older installers.

To downgrade from 1.4 or newer to 1.3.2 or older:

1. [Uninstall Workbooks & Inspector manually](#uninstall-macos)
2. Run the 1.3.2 or older `.pkg` installer
