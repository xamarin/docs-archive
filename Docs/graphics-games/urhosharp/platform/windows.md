---
title: "UrhoSharp Windows Support"
description: "This document discusses Windows support for UrhoSharp. It describes how to create a project, configure and launch Urho, integrate with WPF, and integrate with UWP."
ms.prod: xamarin
ms.assetid: A4F36014-AE4E-4F07-A1AC-F264AAA68ACF
author: conceptdev
ms.author: crdun
ms.date: 03/29/2017
---

# UrhoSharp Windows support

While Urho is a portable class library, and allows the same API to be
used across the various platform for your game logic, you still need
to initialize Urho in your platform specific driver, and in some
cases, you will want to take advantage of platform specific features.

In the pages below, assume that `MyGame` is a subclass of the
`Application` class.

**Supported architectures:** only 64bit Windows.

You can see complete examples showing how to use this in our [samples](https://github.com/xamarin/urho-samples/tree/master/FeatureSamples)

## Standalone project

### Creating a project

Create a Console project, reference the Urho NuGet and then make sure
that you can locate the assets (the directories containing the Data
directory).

### Configuring and launching Urho

To launch your application, do this:

```csharp
DesktopUrhoInitializer.AssetsDirectory = "../Assets";
new MyGame().Run();
```

### Example

[Complete example](https://github.com/xamarin/urho-samples/tree/master/FeatureSamples/Desktop)

## Integrated with WPF

### Creating a project

Create a WPF project, reference the Urho NuGet and then make sure
that you can locate the assets (the directories containing the Data
directory).

### Configuring and launching Urho from WPF

Create a subclass of `Window` and configure your assets like this:

```csharp
    public partial class MainWindow : Window
    {
        Application currentApplication;

        public MainWindow()
        {
            InitializeComponent();
            DesktopUrhoInitializer.AssetsDirectory = @"../../Assets";
            Loaded += (s,e) => RunGame (new MyGame ());
        }

        async void RunGame(MyGame game)
        {
            var urhoSurface = new Panel { Dock = DockStyle.Fill };
            WindowsFormsHost.Child = urhoSurface;
            WindowsFormsHost.Focus();
            urhoSurface.Focus();
            await Task.Yield();
            var appOptions = new ApplicationOptions(assetsFolder: "Data")
                {
                    ExternalWindow = RunInSdlWindow.IsChecked.Value ? IntPtr.Zero : urhoSurface.Handle,
                    LimitFps = false, //true means "limit to 200fps"
                };
            currentApplication = Urho.Application.CreateInstance(value.Type, appOptions);
            currentApplication.Run();
        }
    }
```

### Example

[Complete example](https://github.com/xamarin/urho-samples/tree/master/FeatureSamples/WPF)

## Integrated with UWP

### Creating a project

Create a UWP project, reference the Urho NuGet and then make sure
that you can locate the assets (the directories containing the Data
directory).

### Configuring and launching Urho from UWP

Create a subclass of `Window` and configure your assets like this:

```csharp
    {
        InitializeComponent();
        GameTypes = typeof(Sample).GetTypeInfo().Assembly.GetTypes()
            .Where(t => t.GetTypeInfo().IsSubclassOf(typeof(Application)) && t != typeof(Sample))
            .Select((t, i) => new TypeInfo(t, $"{i + 1}. {t.Name}", ""))
            .ToArray();
        DataContext = this;
        Loaded += (s, e) => RunGame (new MyGame ());
    }

    public void RunGame(TypeInfo value)
    {
        //at this moment, UWP supports assets only in pak files (see PackageTool)
        currentApplication = UrhoSurface.Run(value.Type, "Data.pak");
    }
}
```

### Example

[Complete example](https://github.com/xamarin/urho-samples/tree/master/FeatureSamples/UWP)

## Integrated with Windows Forms

### Creating a project

Create a Windows Forms project, reference the Urho NuGet and then make sure
that you can locate the assets (the directories containing the Data
directory).

### Configuring and launching Urho from Windows.Forms

Launch Urho from your form, see [Complete Sample](https://github.com/xamarin/urho-samples/blob/master/FeatureSamples/WinForms/SamplesForm.cs)
