---
title: "Playing a Web video"
description: "This article explains how to play web videos in a video player application, using Xamarin.Forms."
ms.prod: xamarin
ms.assetid: 75781A10-865D-4BA8-8D6B-E3DA012922BC
ms.technology: xamarin-forms
author: davidbritch
ms.author: dabritch
ms.date: 02/12/2018
no-loc: [Xamarin.Forms, Xamarin.Essentials]
---

# Playing a Web video

[![Download Sample](~/media/shared/download.png) Download the sample](/samples/xamarin/xamarin-forms-samples/customrenderers-videoplayerdemos)

The `VideoPlayer` class defines a `Source` property used to specify the source of the video file, as well as an `AutoPlay` property. `AutoPlay` has a default setting of `true`, which means that the video should begin playing automatically after `Source` has been set:

```csharp
using System;
using Xamarin.Forms;

namespace FormsVideoLibrary
{
    public class VideoPlayer : View, IVideoPlayerController
    {
        ···
        // Source property
        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create(nameof(Source), typeof(VideoSource), typeof(VideoPlayer), null);

        [TypeConverter(typeof(VideoSourceConverter))]
        public VideoSource Source
        {
            set { SetValue(SourceProperty, value); }
            get { return (VideoSource)GetValue(SourceProperty); }
        }

        // AutoPlay property
        public static readonly BindableProperty AutoPlayProperty =
            BindableProperty.Create(nameof(AutoPlay), typeof(bool), typeof(VideoPlayer), true);

        public bool AutoPlay
        {
            set { SetValue(AutoPlayProperty, value); }
            get { return (bool)GetValue(AutoPlayProperty); }
        }
        ···
    }
}
```

The `Source` property is of type `VideoSource`, which is patterned after the Xamarin.Forms [`ImageSource`](xref:Xamarin.Forms.ImageSource) abstract class, and its three derivatives, [`UriImageSource`](xref:Xamarin.Forms.UriImageSource), [`FileImageSource`](xref:Xamarin.Forms.FileImageSource), and [`StreamImageSource`](xref:Xamarin.Forms.StreamImageSource). No stream option is available for the `VideoPlayer` however, because iOS and Android do not support playing a video from a stream.

## Video sources

The abstract `VideoSource` class consists solely of three static methods that instantiate the three classes that derive from `VideoSource`:

```csharp
namespace FormsVideoLibrary
{
    [TypeConverter(typeof(VideoSourceConverter))]
    public abstract class VideoSource : Element
    {
        public static VideoSource FromUri(string uri)
        {
            return new UriVideoSource() { Uri = uri };
        }

        public static VideoSource FromFile(string file)
        {
            return new FileVideoSource() { File = file };
        }

        public static VideoSource FromResource(string path)
        {
            return new ResourceVideoSource() { Path = path };
        }
    }
}
```

The `UriVideoSource` class is used to specify a downloadable video file with a URI. It defines a single property of type `string`:

```csharp
namespace FormsVideoLibrary
{
    public class UriVideoSource : VideoSource
    {
        public static readonly BindableProperty UriProperty =
            BindableProperty.Create(nameof(Uri), typeof(string), typeof(UriVideoSource));

        public string Uri
        {
            set { SetValue(UriProperty, value); }
            get { return (string)GetValue(UriProperty); }
        }
    }
}
```

Handling objects of type `UriVideoSource` is described below.

The `ResourceVideoSource` class is used to access video files that are stored as embedded resources in the platform application, also specified with a `string` property:

```csharp
namespace FormsVideoLibrary
{
    public class ResourceVideoSource : VideoSource
    {
        public static readonly BindableProperty PathProperty =
            BindableProperty.Create(nameof(Path), typeof(string), typeof(ResourceVideoSource));

        public string Path
        {
            set { SetValue(PathProperty, value); }
            get { return (string)GetValue(PathProperty); }
        }
    }
}
```

Handling objects of type `ResourceVideoSource` is described in the article [Loading Application Resource Videos](loading-resources.md). The `VideoPlayer` class has no facility to load a video file stored as a resource in the .NET Standard library.

The `FileVideoSource` class is used to access video files from the device's video library. The single property is also of type `string`:

```csharp
namespace FormsVideoLibrary
{
    public class FileVideoSource : VideoSource
    {
        public static readonly BindableProperty FileProperty =
                  BindableProperty.Create(nameof(File), typeof(string), typeof(FileVideoSource));

        public string File
        {
            set { SetValue(FileProperty, value); }
            get { return (string)GetValue(FileProperty); }
        }
    }
}
```

Handling objects of type `FileVideoSource` is described in the article [Accessing the Device's Video Library](accessing-library.md).

The `VideoSource` class includes a `TypeConverter` attribute that references `VideoSourceConverter`:

```csharp
namespace FormsVideoLibrary
{
    [TypeConverter(typeof(VideoSourceConverter))]
    public abstract class VideoSource : Element
    {
        ···
    }
}
```

This type converter is invoked when the `Source` property is set to a string in XAML. Here's the `VideoSourceConverter` class:

```csharp
namespace FormsVideoLibrary
{
    public class VideoSourceConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (!String.IsNullOrWhiteSpace(value))
            {
                Uri uri;
                return Uri.TryCreate(value, UriKind.Absolute, out uri) && uri.Scheme != "file" ?
                                VideoSource.FromUri(value) : VideoSource.FromResource(value);
            }

            throw new InvalidOperationException("Cannot convert null or whitespace to ImageSource");
        }
    }
}
```

The `ConvertFromInvariantString` method attempts to convert the string to a `Uri` object. If that succeeds, and the scheme is not `file:`, then the method returns a `UriVideoSource`. Otherwise, it returns a `ResourceVideoSource`.

## Setting the video source

All the other logic involving video sources is implemented in the individual platform renderers. The following sections show how the platform renderers play videos when the `Source` property is set to a `UriVideoSource` object.

### The iOS video source

Two sections of the `VideoPlayerRenderer` are involved in setting the video source of the video player. When Xamarin.Forms first creates an object of type `VideoPlayer`, the `OnElementChanged` method is called with the `NewElement` property of the arguments object set to that `VideoPlayer`. The `OnElementChanged` method calls `SetSource`:

```csharp
namespace FormsVideoLibrary.iOS
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, UIView>
    {
        ···
        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            ···
            if (args.NewElement != null)
            {
                ···
                SetSource();
                ···
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            ···
            else if (args.PropertyName == VideoPlayer.SourceProperty.PropertyName)
            {
                SetSource();
            }
            ···
        }
        ···
    }
}
```

Later on, when the `Source` property is changed, the `OnElementPropertyChanged` method is called with a `PropertyName` property of "Source", and `SetSource` is called again.

To play a video file in iOS, an object of type [`AVAsset`](xref:AVFoundation.AVAsset) is first created to encapsulate the video file, and that is used to create an [`AVPlayerItem`](xref:AVFoundation.AVPlayerItem), which is then handed off to the `AVPlayer` object. Here's how the `SetSource` method handles the `Source` property when it's of type `UriVideoSource`:

```csharp
namespace FormsVideoLibrary.iOS
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, UIView>
    {
        AVPlayer player;
        AVPlayerItem playerItem;
        ···
        void SetSource()
        {
            AVAsset asset = null;

            if (Element.Source is UriVideoSource)
            {
                string uri = (Element.Source as UriVideoSource).Uri;

                if (!String.IsNullOrWhiteSpace(uri))
                {
                    asset = AVAsset.FromUrl(new NSUrl(uri));
                }
            }
            ···
            if (asset != null)
            {
                playerItem = new AVPlayerItem(asset);
            }
            else
            {
                playerItem = null;
            }

            player.ReplaceCurrentItemWithPlayerItem(playerItem);

            if (playerItem != null && Element.AutoPlay)
            {
                player.Play();
            }
        }
        ···
    }
}
```

The `AutoPlay` property has no analogue in the iOS video classes, so the property is examined at the end of the `SetSource` method to call the `Play` method on the `AVPlayer` object.

In some cases, videos continued playing after the page with the `VideoPlayer` navigated back to the home page. To stop the video, the `ReplaceCurrentItemWithPlayerItem` is also set in the `Dispose` override:

```csharp
namespace FormsVideoLibrary.iOS
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, UIView>
    {
        ···
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (player != null)
            {
                player.ReplaceCurrentItemWithPlayerItem(null);
            }
        }
        ···
    }
}
```

### The Android video source

The Android `VideoPlayerRenderer` needs to set the video source of the player when the `VideoPlayer` is first created and later when the `Source` property changes:

```csharp
namespace FormsVideoLibrary.Droid
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, ARelativeLayout>
    {
        ···
        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            ···
            if (args.NewElement != null)
            {
                ···
                SetSource();
                ···
            }
        }
        ···
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            ···
            else if (args.PropertyName == VideoPlayer.SourceProperty.PropertyName)
            {
                SetSource();
            }
            ···
        }
        ···
    }
}
```

The `SetSource` method handles objects of type `UriVideoSource` by calling `SetVideoUri` on the `VideoView` with an Android `Uri` object created from the string URI. The `Uri` class is fully qualified here to distinguish it from the .NET `Uri` class:

```csharp
namespace FormsVideoLibrary.Droid
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, ARelativeLayout>
    {
        ···
        void SetSource()
        {
            isPrepared = false;
            bool hasSetSource = false;

            if (Element.Source is UriVideoSource)
            {
                string uri = (Element.Source as UriVideoSource).Uri;

                if (!String.IsNullOrWhiteSpace(uri))
                {
                    videoView.SetVideoURI(Android.Net.Uri.Parse(uri));
                    hasSetSource = true;
                }
            }
            ···

            if (hasSetSource && Element.AutoPlay)
            {
                videoView.Start();
            }
        }
        ···
    }
}
```

The Android `VideoView` doesn't have a corresponding `AutoPlay` property, so the `Start` method is called if a new video has been set.

There is a difference between the behavior of the iOS and Android renderers if the `Source` property of `VideoPlayer` is set to `null`, or if the `Uri` property of `UriVideoSource` is set to `null` or a blank string. If the iOS video player is currently playing a video, and `Source` is set to `null` (or the string is `null` or blank), `ReplaceCurrentItemWithPlayerItem` is called with `null` value. The current video is replaced and stops playing.

Android does not support a similar facility. If the `Source` property is set to `null`, the `SetSource` method simply ignores it, and the current video continues to play.

### The UWP video source

The UWP `MediaElement` defines an `AutoPlay` property, which is handled in the renderer like any other property:

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
                SetSource();
                SetAutoPlay();
                ···    
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            ···
            else if (args.PropertyName == VideoPlayer.SourceProperty.PropertyName)
            {
                SetSource();
            }
            else if (args.PropertyName == VideoPlayer.AutoPlayProperty.PropertyName)
            {
                SetAutoPlay();
            }
            ···
        }
        ···
    }
}
```

The `SetSource` property handles a `UriVideoSource` object by setting the `Source` property of `MediaElement` to a .NET `Uri` value, or to `null` if the `Source` property of `VideoPlayer` is set to `null`:

```csharp
namespace FormsVideoLibrary.UWP
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, MediaElement>
    {
        ···
        async void SetSource()
        {
            bool hasSetSource = false;

            if (Element.Source is UriVideoSource)
            {
                string uri = (Element.Source as UriVideoSource).Uri;

                if (!String.IsNullOrWhiteSpace(uri))
                {
                    Control.Source = new Uri(uri);
                    hasSetSource = true;
                }
            }
            ···
            if (!hasSetSource)
            {
                Control.Source = null;
            }
        }

        void SetAutoPlay()
        {
            Control.AutoPlay = Element.AutoPlay;
        }
        ···
    }
}
```

## Setting a URL source

With the implementation of these properties in the three renderers, it's possible to play a video from a URL source. The **Play Web Video** page in the [**VideoPlayDemos**](/samples/xamarin/xamarin-forms-samples/customrenderers-videoplayerdemos) program is defined by the following XAML file:

```xaml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:video="clr-namespace:FormsVideoLibrary"
             x:Class="VideoPlayerDemos.PlayWebVideoPage"
             Title="Play Web Video">

    <video:VideoPlayer Source="https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4" />

</ContentPage>
```

The `VideoSourceConverter` class converts the string to a `UriVideoSource`. When you navigate to the **Play Web Video** page, the video begins loading and starts playing when a sufficient quantity of data has been downloaded and buffered. The video is about 10 minutes in length:

[![Play Web Video](web-videos-images/playwebvideo-small.png "Play Web Video")](web-videos-images/playwebvideo-large.png#lightbox "Play Web Video")

On each of the platforms, the transport controls fade out if they're not used but can be restored to view by tapping the video.

You can prevent the video from automatically starting by setting the `AutoPlay` property to `false`:

```xaml
<video:VideoPlayer Source="https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4"
                   AutoPlay="false" />
```

You'll need to press the **Play** button to start the video.

Similarly, you can suppress the display of the transport controls by setting the `AreTransportControlsEnabled` property to `false`:

```xaml
<video:VideoPlayer Source="https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4"
                   AreTransportControlsEnabled="False" />
```

If you set both properties to `false`, then the video won't begin playing and there will be no way to start it! You would need to call `Play` from the code-behind file, or to create your own transport controls as described in the article [Implementing Custom Video Transport Controls](custom-transport.md).

The **App.xaml** file includes resources for two additional videos:

```xaml
<Application xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:video="clr-namespace:FormsVideoLibrary"
             x:Class="VideoPlayerDemos.App">
    <Application.Resources>
        <ResourceDictionary>

            <video:UriVideoSource x:Key="ElephantsDream"
                                  Uri="https://archive.org/download/ElephantsDream/ed_hd_512kb.mp4" />

            <video:UriVideoSource x:Key="BigBuckBunny"
                                  Uri="https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4" />

            <video:UriVideoSource x:Key="Sintel"
                                  Uri="https://archive.org/download/Sintel/sintel-2048-stereo_512kb.mp4" />

        </ResourceDictionary>
    </Application.Resources>
</Application>
```

To reference one of these other movies, you can replace the explicit URL in the **PlayWebVideo.xaml** file with a `StaticResource` markup extension, in which case `VideoSourceConverter` is not required to create the `UriVideoSource` object:

```xaml
<video:VideoPlayer Source="{StaticResource ElephantsDream}" />
```

Alternatively, you can set the `Source` property from a video file in a `ListView`, as described in the next article, [Binding Video Sources to the Player](source-bindings.md).

## Related Links

- [Video Player Demos (sample)](/samples/xamarin/xamarin-forms-samples/customrenderers-videoplayerdemos)