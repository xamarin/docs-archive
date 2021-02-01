---
title: "Creating the platform video players"
description: "This article explains how to implement a video player custom renderer on each platform, using Xamarin.Forms."
ms.prod: xamarin
ms.assetid: EEE2FB9B-EB73-4A3F-A859-7A1D4808E149
ms.technology: xamarin-forms
author: davidbritch
ms.author: dabritch
ms.date: 02/12/2018
no-loc: [Xamarin.Forms, Xamarin.Essentials]
---

# Creating the platform video players

[![Download Sample](~/media/shared/download.png) Download the sample](/samples/xamarin/xamarin-forms-samples/customrenderers-videoplayerdemos)

The [**VideoPlayerDemos**](/samples/xamarin/xamarin-forms-samples/customrenderers-videoplayerdemos) solution contains all the code to implement a video player for Xamarin.Forms. It also includes a series of pages that demonstrates how to use the video player within an application. All the `VideoPlayer` code and its platform renderers reside in project folders named `FormsVideoLibrary`, and also use the namespace `FormsVideoLibrary`. This should make it easy to copy the files into your own application and reference the classes.

## The video player

The [`VideoPlayer`](https://github.com/xamarin/xamarin-forms-samples/blob/master/CustomRenderers/VideoPlayerDemos/VideoPlayerDemos/VideoPlayerDemos/FormsVideoLibrary/VideoPlayer.cs) class is part of the **VideoPlayerDemos** .NET Standard library that is shared among the platforms. It derives from `View`:

```csharp
using System;
using Xamarin.Forms;

namespace FormsVideoLibrary
{
    public class VideoPlayer : View, IVideoPlayerController
    {
        ···
    }
}
```

The members of this class (and the `IVideoPlayerController` interface) are described in the articles that follow.

Each of the platforms contains a class named `VideoPlayerRenderer` that contains the platform-specific code to implement a video player. The primary task of this renderer is to create a video player for that platform.

### The iOS player view controller

Several classes are involved when implementing a video player in iOS. The application first creates an [`AVPlayerViewController`](xref:AVKit.AVPlayerViewController) and then sets the [`Player`](xref:AVKit.AVPlayerViewController.Player*) property to an object of type [`AVPlayer`](xref:AVFoundation.AVPlayer). Additional classes are required when the player is assigned a video source.

Like all renderers, the iOS [`VideoPlayerRenderer`](https://github.com/xamarin/xamarin-forms-samples/blob/master/CustomRenderers/VideoPlayerDemos/VideoPlayerDemos/VideoPlayerDemos.iOS/FormsVideoLibrary/VideoPlayerRenderer.csVideoPlayerRenderer.cs) contains an `ExportRenderer` attribute that identifies the `VideoPlayer` view with the renderer:

```csharp
using System;
using System.ComponentModel;
using System.IO;

using AVFoundation;
using AVKit;
using CoreMedia;
using Foundation;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FormsVideoLibrary.VideoPlayer),
                          typeof(FormsVideoLibrary.iOS.VideoPlayerRenderer))]

namespace FormsVideoLibrary.iOS
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, UIView>
    {
        ···
    }
}
```

Generally a renderer that sets a platform control derives from the [`ViewRenderer<View, NativeView>`](https://github.com/xamarin/Xamarin.Forms/blob/master/Xamarin.Forms.Platform.iOS/ViewRenderer.cs) class, where `View` is the Xamarin.Forms `View` derivative (in this case, `VideoPlayer`) and `NativeView` is an iOS `UIView` derivative for the renderer class. For this renderer, that generic argument is simply set to `UIView`, for reasons you'll see shortly.

When a renderer is based on a `UIViewController` derivative (as this one is), then the class should override the `ViewController` property and return the view controller, in this case `AVPlayerViewController`. That is the purpose of the `_playerViewController` field:

```csharp
namespace FormsVideoLibrary.iOS
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, UIView>
    {
        AVPlayer player;
        AVPlayerItem playerItem;
        AVPlayerViewController _playerViewController;       // solely for ViewController property

        public override UIViewController ViewController => _playerViewController;

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            base.OnElementChanged(args);

            if (args.NewElement != null)
            {
                if (Control == null)
                {
                    // Create AVPlayerViewController
                    _playerViewController = new AVPlayerViewController();

                    // Set Player property to AVPlayer
                    player = new AVPlayer();
                    _playerViewController.Player = player;

                    // Use the View from the controller as the native control
                    SetNativeControl(_playerViewController.View);
                }
                ···
            }
        }
        ···
    }
}
```

The primary responsibility of the `OnElementChanged` override is to check if the `Control` property is `null` and, if so, create a platform control, and pass it to the `SetNativeControl` method. In this case, that object is only available from the `View` property of the `AVPlayerViewController`. That `UIView` derivative happens to be a private class named `AVPlayerView`, but because it's private, it cannot be explicitly specified as the second generic argument to `ViewRenderer`.

Generally the `Control` property of the renderer class thereafter refers to the `UIView` used to implement the renderer, but in this case the `Control` property is not used elsewhere.

### The Android video view

The Android renderer for `VideoPlayer` is based on the Android [`VideoView`](xref:Android.Widget.VideoView) class. However, if `VideoView` is used by itself to play a video in a Xamarin.Forms application, the video fills the area alloted for the `VideoPlayer` without maintaining the correct aspect ratio. For this reason (as you'll see shortly), the `VideoView` is made a child of an Android `RelativeLayout`. A `using` directive defines `ARelativeLayout` to distinguish it from the Xamarin.Forms `RelativeLayout`, and that's the second generic argument in the `ViewRenderer`:

```csharp
using System;
using System.ComponentModel;
using System.IO;

using Android.Content;
using Android.Media;
using Android.Widget;
using ARelativeLayout = Android.Widget.RelativeLayout;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FormsVideoLibrary.VideoPlayer),
                          typeof(FormsVideoLibrary.Droid.VideoPlayerRenderer))]

namespace FormsVideoLibrary.Droid
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, ARelativeLayout>
    {
        ···
        public VideoPlayerRenderer(Context context) : base(context)
        {
        }
        ···
    }
}
```

Beginning in Xamarin.Forms 2.5, Android renderers should include a constructor with a `Context` argument.

The `OnElementChanged` override creates both the `VideoView` and `RelativeLayout` and sets the layout parameters for the `VideoView` to center it within the `RelativeLayout`.

```csharp
namespace FormsVideoLibrary.Droid
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, ARelativeLayout>
    {
        VideoView videoView;
        MediaController mediaController;    // Used to display transport controls
        bool isPrepared;
        ···
        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            base.OnElementChanged(args);

            if (args.NewElement != null)
            {
                if (Control == null)
                {
                    // Save the VideoView for future reference
                    videoView = new VideoView(Context);

                    // Put the VideoView in a RelativeLayout
                    ARelativeLayout relativeLayout = new ARelativeLayout(Context);
                    relativeLayout.AddView(videoView);

                    // Center the VideoView in the RelativeLayout
                    ARelativeLayout.LayoutParams layoutParams =
                        new ARelativeLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
                    layoutParams.AddRule(LayoutRules.CenterInParent);
                    videoView.LayoutParameters = layoutParams;

                    // Handle a VideoView event
                    videoView.Prepared += OnVideoViewPrepared;

                    // Use the RelativeLayout as the native control
                    SetNativeControl(relativeLayout);
                }
                ···
            }
            ···
        }

        protected override void Dispose(bool disposing)
        {
            if (Control != null && videoView != null)
            {
                videoView.Prepared -= OnVideoViewPrepared;
            }
            base.Dispose(disposing);
        }

        void OnVideoViewPrepared(object sender, EventArgs args)
        {
            isPrepared = true;
            ···
        }
        ···
    }
}
```

A handler for the `Prepared` event is attached in this method and detached in the `Dispose` method. This event is fired when the `VideoView` has sufficient information to begin playing a video file.

### The UWP media element

In the Universal Windows Platform (UWP), the most common video player is [`MediaElement`](xref:Windows.UI.Xaml.Controls.MediaElement). That documentation of `MediaElement` indicates that the [`MediaPlayerElement`](xref:Windows.UI.Xaml.Controls.MediaPlayerElement) should be used instead when it's only necessary to support versions of Windows 10 beginning with build 1607.

The `OnElementChanged` override needs to create a `MediaElement`, set a couple of event handlers, and pass the `MediaElement` object to `SetNativeControl`:

```csharp
using System;
using System.ComponentModel;

using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(FormsVideoLibrary.VideoPlayer),
                          typeof(FormsVideoLibrary.UWP.VideoPlayerRenderer))]

namespace FormsVideoLibrary.UWP
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, MediaElement>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            base.OnElementChanged(args);

            if (args.NewElement != null)
            {
                if (Control == null)
                {
                    MediaElement mediaElement = new MediaElement();
                    SetNativeControl(mediaElement);

                    mediaElement.MediaOpened += OnMediaElementMediaOpened;
                    mediaElement.CurrentStateChanged += OnMediaElementCurrentStateChanged;
                }
                ···
            }
            ···
        }

        protected override void Dispose(bool disposing)
        {
            if (Control != null)
            {
                Control.MediaOpened -= OnMediaElementMediaOpened;
                Control.CurrentStateChanged -= OnMediaElementCurrentStateChanged;
            }

            base.Dispose(disposing);
        }        
        ···
    }
}
```

The two event handlers are detached in the `Dispose` event for the renderer.

## Showing the transport controls

All the video players included in the platforms support a default set of transport controls that include buttons for playing and pausing, and a bar to indicate the current position within the video, and to move to a new position.

The `VideoPlayer` class defines a property named `AreTransportControlsEnabled` and sets the default value to `true`:

```csharp
namespace FormsVideoLibrary
{
    public class VideoPlayer : View, IVideoPlayerController
    {
        ···
        // AreTransportControlsEnabled property
        public static readonly BindableProperty AreTransportControlsEnabledProperty =
            BindableProperty.Create(nameof(AreTransportControlsEnabled), typeof(bool), typeof(VideoPlayer), true);

        public bool AreTransportControlsEnabled
        {
            set { SetValue(AreTransportControlsEnabledProperty, value); }
            get { return (bool)GetValue(AreTransportControlsEnabledProperty); }
        }
        ···
    }
}
```

Although this property has both `set` and `get` accessors, the renderer has to handle cases only when the property is set. The `get` accessor simply returns the current value of the property.

Properties such as `AreTransportControlsEnabled` are handled in platform renderers in two ways:

- The first time is when Xamarin.Forms creates a `VideoPlayer` element. This is indicated in the `OnElementChanged` override of the renderer when the `NewElement` property is not `null`. At this time, the renderer can set is own platform video player from the property's initial value as defined in the `VideoPlayer`.

- If the property in `VideoPlayer` later changes, then the `OnElementPropertyChanged` method in the renderer is called. This allows the renderer to update the platform video player based on the new property setting.

The following sections discuss how the `AreTransportControlsEnabled` property is handled on each platform.

### iOS playback controls

The property of the iOS `AVPlayerViewController` that governs the display of transport controls is [`ShowsPlaybackControls`](xref:AVKit.AVPlayerViewController.ShowsPlaybackControls*). Here's how that property is set in the iOS `VideoViewRenderer`:

```csharp
namespace FormsVideoLibrary.iOS
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, UIView>
    {
        ···
        AVPlayerViewController _playerViewController;       // solely for ViewController property

        public override UIViewController ViewController => _playerViewController;

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            ···
            if (args.NewElement != null)
            {
                ···
                SetAreTransportControlsEnabled();
                ···
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            if (args.PropertyName == VideoPlayer.AreTransportControlsEnabledProperty.PropertyName)
            {
                SetAreTransportControlsEnabled();
            }
            ···
        }

        void SetAreTransportControlsEnabled()
        {
            ((AVPlayerViewController)ViewController).ShowsPlaybackControls = Element.AreTransportControlsEnabled;
        }
        ···
    }
}
```

The `Element` property of the renderer refers to the `VideoPlayer` class.

### The Android media controller

In Android, displaying the transport controls requires creating a [`MediaController`](xref:Android.Widget.MediaController) object and associating it with the `VideoView` object. The mechanics are demonstrated in the `SetAreTransportControlsEnabled` method:

```csharp
namespace FormsVideoLibrary.Droid
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, ARelativeLayout>
    {
        VideoView videoView;
        MediaController mediaController;    // Used to display transport controls
        bool isPrepared;

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            ···
            if (args.NewElement != null)
            {
                ···
                SetAreTransportControlsEnabled();
                ···
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            if (args.PropertyName == VideoPlayer.AreTransportControlsEnabledProperty.PropertyName)
            {
                SetAreTransportControlsEnabled();
            }
            ···
        }

        void SetAreTransportControlsEnabled()
        {
            if (Element.AreTransportControlsEnabled)
            {
                mediaController = new MediaController(Context);
                mediaController.SetMediaPlayer(videoView);
                videoView.SetMediaController(mediaController);
            }
            else
            {
                videoView.SetMediaController(null);

                if (mediaController != null)
                {
                    mediaController.SetMediaPlayer(null);
                    mediaController = null;
                }
            }
        }
        ···
    }
}
```

### The UWP Transport Controls property

The UWP `MediaElement` defines a property named [`AreTransportControlsEnabled`](xref:Windows.UI.Xaml.Controls.MediaElement.AreTransportControlsEnabled*), so that property is set from the `VideoPlayer` property of the same name:

```csharp
namespace FormsVideoLibrary.UWP
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, MediaElement>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            ···
            if (args.NewElement != null)
            {
                ···
                SetAreTransportControlsEnabled();
                ···
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            if (args.PropertyName == VideoPlayer.AreTransportControlsEnabledProperty.PropertyName)
            {
                SetAreTransportControlsEnabled();
            }
            ···
        }

        void SetAreTransportControlsEnabled()
        {
            Control.AreTransportControlsEnabled = Element.AreTransportControlsEnabled;
        }
        ···
    }
}
```

One more property is necessary to begin playing a video: This is the crucial `Source` property that references a video file. Implementing the `Source` property is described in the next article, [Playing a Web Video](web-videos.md).

## Related Links

- [Video Player Demos (sample)](/samples/xamarin/xamarin-forms-samples/customrenderers-videoplayerdemos)