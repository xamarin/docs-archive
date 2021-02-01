---
title: "Custom video transport controls"
description: "This article explains how to implement custom transport controls in a video player application, using Xamarin.Forms."
ms.prod: xamarin
ms.assetid: CE9E955D-A9AC-4019-A5D7-6390D80DECA1
ms.technology: xamarin-forms
author: davidbritch
ms.author: dabritch
ms.date: 02/12/2018
no-loc: [Xamarin.Forms, Xamarin.Essentials]
---

# Custom video transport controls

[![Download Sample](~/media/shared/download.png) Download the sample](/samples/xamarin/xamarin-forms-samples/customrenderers-videoplayerdemos)

The transport controls of a video player include the buttons that perform the functions **Play**, **Pause**, and **Stop**. These buttons are generally identified with familiar icons rather than text, and the **Play** and **Pause** functions are generally combined into one button.

By default, the `VideoPlayer` displays transport controls supported by each platform. When you set the `AreTransportControlsEnabled` property to `false`, these controls are suppressed. You can then control the `VideoPlayer` programmatically or supply your own transport controls.

## The Play, Pause, and Stop methods

The `VideoPlayer` class defines three methods named `Play`, `Pause`, and `Stop` that are implemented by firing events:

```csharp
namespace FormsVideoLibrary
{
    public class VideoPlayer : View, IVideoPlayerController
    {
        ···
        public event EventHandler PlayRequested;

        public void Play()
        {
            PlayRequested?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler PauseRequested;

        public void Pause()
        {
            PauseRequested?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler StopRequested;

        public void Stop()
        {
            StopRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
```

Event handlers for these events are set by the `VideoPlayerRenderer` class in each platform, as shown below:

### iOS transport implementations

The iOS version of `VideoPlayerRenderer` uses the `OnElementChanged` method to set handlers for these three events when the `NewElement` property is not `null` and detaches the event handlers when `OldElement` is not `null`:

```csharp
namespace FormsVideoLibrary.iOS
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, UIView>
    {
        AVPlayer player;
        ···
        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            ···
            if (args.NewElement != null)
            {
                ···
                args.NewElement.PlayRequested += OnPlayRequested;
                args.NewElement.PauseRequested += OnPauseRequested;
                args.NewElement.StopRequested += OnStopRequested;
            }

            if (args.OldElement != null)
            {
                ···
                args.OldElement.PlayRequested -= OnPlayRequested;
                args.OldElement.PauseRequested -= OnPauseRequested;
                args.OldElement.StopRequested -= OnStopRequested;
            }
        }
        ···
        // Event handlers to implement methods
        void OnPlayRequested(object sender, EventArgs args)
        {
            player.Play();
        }

        void OnPauseRequested(object sender, EventArgs args)
        {
            player.Pause();
        }

        void OnStopRequested(object sender, EventArgs args)
        {
            player.Pause();
            player.Seek(new CMTime(0, 1));
        }
    }
}
```

The event handlers are implemented by calling methods on the `AVPlayer` object. There is no `Stop` method for `AVPlayer`, so it's simulated by pausing the video and moving the position to the beginning.

### Android transport implementations

The Android implementation is similar to the iOS implementation. The handlers for the three functions are set when `NewElement` is not `null` and detached when `OldElement` is not `null`:

```csharp
namespace FormsVideoLibrary.Droid
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, ARelativeLayout>
    {
        VideoView videoView;
        ···
        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            ···
            if (args.NewElement != null)
            {
                ···
                args.NewElement.PlayRequested += OnPlayRequested;
                args.NewElement.PauseRequested += OnPauseRequested;
                args.NewElement.StopRequested += OnStopRequested;
            }

            if (args.OldElement != null)
            {
                ···
                args.OldElement.PlayRequested -= OnPlayRequested;
                args.OldElement.PauseRequested -= OnPauseRequested;
                args.OldElement.StopRequested -= OnStopRequested;
            }
        }
        ···
        void OnPlayRequested(object sender, EventArgs args)
        {
            videoView.Start();
        }

        void OnPauseRequested(object sender, EventArgs args)
        {
            videoView.Pause();
        }

        void OnStopRequested(object sender, EventArgs args)
        {
            videoView.StopPlayback();
        }
    }
}
```

The three functions call methods defined by `VideoView`.

### UWP transport implementations

The UWP implementation of the three transport functions is very similar to both the iOS and Android implementations:

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
                args.NewElement.PlayRequested += OnPlayRequested;
                args.NewElement.PauseRequested += OnPauseRequested;
                args.NewElement.StopRequested += OnStopRequested;
            }

            if (args.OldElement != null)
            {
                ···
                args.OldElement.PlayRequested -= OnPlayRequested;
                args.OldElement.PauseRequested -= OnPauseRequested;
                args.OldElement.StopRequested -= OnStopRequested;
            }
        }
        ···
        // Event handlers to implement methods
        void OnPlayRequested(object sender, EventArgs args)
        {
            Control.Play();
        }

        void OnPauseRequested(object sender, EventArgs args)
        {
            Control.Pause();
        }

        void OnStopRequested(object sender, EventArgs args)
        {
            Control.Stop();
        }
    }
}
```

## The video player status

Implementing the **Play**, **Pause**, and **Stop** functions is not sufficient for supporting transport controls. Often the **Play** and **Pause** commands are implemented with the same button that changes its appearance to indicate whether the video is currently playing or paused. Moreover, the button shouldn't even be enabled if the video has not yet loaded.

These requirements imply that the video player needs to make available a current status indicating if it's playing or paused, or if it's not yet ready to play a video. (Each platform also supports properties that indicate if the video can be paused, or can be moved to a new position, but these properties are applicable for streaming video rather than video files, so they are not supported in the `VideoPlayer` described here.)

The **VideoPlayerDemos** project includes a `VideoStatus` enumeration with three members:

```csharp
namespace FormsVideoLibrary
{
    public enum VideoStatus
    {
        NotReady,
        Playing,
        Paused
    }
}
```

The `VideoPlayer` class defines a real-only bindable property named `Status` of type `VideoStatus`. This property is defined as read-only because it should only be set from the platform renderer:

```csharp
using System;
using Xamarin.Forms;

namespace FormsVideoLibrary
{
    public class VideoPlayer : View, IVideoPlayerController
    {
        ···
        // Status read-only property
        private static readonly BindablePropertyKey StatusPropertyKey =
            BindableProperty.CreateReadOnly(nameof(Status), typeof(VideoStatus), typeof(VideoPlayer), VideoStatus.NotReady);

        public static readonly BindableProperty StatusProperty = StatusPropertyKey.BindableProperty;

        public VideoStatus Status
        {
            get { return (VideoStatus)GetValue(StatusProperty); }
        }

        VideoStatus IVideoPlayerController.Status
        {
            set { SetValue(StatusPropertyKey, value); }
            get { return Status; }
        }
        ···
    }
}
```

Usually, a read-only bindable property would have a private `set` accessor on the `Status` property to allow it to be set from within the class. For a `View` derivative supported by renderers, however, the property must be set from outside the class, but only by the platform renderer.

For this reason, another property is defined with the name `IVideoPlayerController.Status`. This is an explicit interface implementation, and is made possible by the `IVideoPlayerController` interface that the `VideoPlayer` class implements:

```csharp
namespace FormsVideoLibrary
{
    public interface IVideoPlayerController
    {
        VideoStatus Status { set; get; }

        TimeSpan Duration { set; get; }
    }
}
```

This is similar to how the [`WebView`](xref:Xamarin.Forms.WebView) control uses the [`IWebViewController`](xref:Xamarin.Forms.IWebViewController) interface to implement the `CanGoBack` and `CanGoForward` properties. (See the source code of [`WebView`](https://github.com/xamarin/Xamarin.Forms/blob/master/Xamarin.Forms.Core/WebView.cs) and its renderers for details.)

This makes it possible for a class external to `VideoPlayer` to set the `Status` property by referencing the `IVideoPlayerController` interface. (You'll see the code shortly.) The property can be set from other classes as well, but it's unlikely to be set inadvertently. Most importantly, the `Status` property cannot be set through a data binding.

To assist the renderers in keeping this `Status` property updated, the `VideoPlayer` class defines an `UpdateStatus` event that is triggered every tenth of a second:

```csharp
namespace FormsVideoLibrary
{
    public class VideoPlayer : View, IVideoPlayerController
    {
        public event EventHandler UpdateStatus;

        public VideoPlayer()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                UpdateStatus?.Invoke(this, EventArgs.Empty);
                return true;
            });
        }
        ···
    }
}
```

### The iOS status setting

The iOS `VideoPlayerRenderer` sets a handler for the `UpdateStatus` event (and detaches that handler when the underlying `VideoPlayer` element is absent), and uses the handler to set the `Status` property:

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
                args.NewElement.UpdateStatus += OnUpdateStatus;
                ···
            }

            if (args.OldElement != null)
            {
                args.OldElement.UpdateStatus -= OnUpdateStatus;
                ···
            }
        }
        ···
        void OnUpdateStatus(object sender, EventArgs args)
        {
            VideoStatus videoStatus = VideoStatus.NotReady;

            switch (player.Status)
            {
                case AVPlayerStatus.ReadyToPlay:
                    switch (player.TimeControlStatus)
                    {
                        case AVPlayerTimeControlStatus.Playing:
                            videoStatus = VideoStatus.Playing;
                            break;

                        case AVPlayerTimeControlStatus.Paused:
                            videoStatus = VideoStatus.Paused;
                            break;
                    }
                    break;
                }
            }

            ((IVideoPlayerController)Element).Status = videoStatus;
            ···
        }
        ···
    }
}
```

Two properties of `AVPlayer` must be accessed: The [`Status`](xref:AVFoundation.AVPlayer.Status*) property of type `AVPlayerStatus` and the [`TimeControlStatus`](xref:AVFoundation.AVPlayer.TimeControlStatus*) property of type `AVPlayerTimeControlStatus`. Notice that the `Element` property (which is the `VideoPlayer`) must be cast to `IVideoPlayerController` to set the `Status` property.

### The Android status setting

The [`IsPlaying`](xref:Android.Widget.VideoView.IsPlaying) property of the Android `VideoView` is a Boolean that only indicates if the video is playing or paused. To determine if the `VideoView` can neither play nor pause the video yet, the `Prepared` event of `VideoView` must be handled. These two handlers are set in the `OnElementChanged` method, and detached during the `Dispose` override:

```csharp
namespace FormsVideoLibrary.Droid
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, ARelativeLayout>
    {
        VideoView videoView;
        ···
        bool isPrepared;

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            ···
            if (args.NewElement != null)
            {
                if (Control == null)
                {
                    ···
                    videoView.Prepared += OnVideoViewPrepared;
                    ···
                }
                ···
                args.NewElement.UpdateStatus += OnUpdateStatus;
                ···
            }

            if (args.OldElement != null)
            {
                args.OldElement.UpdateStatus -= OnUpdateStatus;
                ···
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (Control != null && videoView != null)
            {
                videoView.Prepared -= OnVideoViewPrepared;
            }
            if (Element != null)
            {
                Element.UpdateStatus -= OnUpdateStatus;
            }

            base.Dispose(disposing);
        }
        ···
    }
}
```

The `UpdateStatus` handler uses the `isPrepared` field (set in the `Prepared` handler) and the `IsPlaying` property to set the `Status` property:

```csharp
namespace FormsVideoLibrary.Droid
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, ARelativeLayout>
    {
        VideoView videoView;
        ···
        bool isPrepared;
        ···
        void OnVideoViewPrepared(object sender, EventArgs args)
        {
            isPrepared = true;
            ···
        }
        ···
        void OnUpdateStatus(object sender, EventArgs args)
        {
            VideoStatus status = VideoStatus.NotReady;

            if (isPrepared)
            {
                status = videoView.IsPlaying ? VideoStatus.Playing : VideoStatus.Paused;
            }
            ···
        }
        ···
    }
}
```

### The UWP status setting

The UWP `VideoPlayerRenderer` makes use of the `UpdateStatus` event, but it does not need it for setting the `Status` property. The `MediaElement` defines a [`CurrentStateChanged`](xref:Windows.UI.Xaml.Controls.MediaElement.CurrentStateChanged) event that allows the renderer to be notified when the [`CurrentState`](xref:Windows.UI.Xaml.Controls.MediaElement.CurrentState*) property has changed. The property is detached in the `Dispose` override:

```csharp
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
                    ···
                    mediaElement.CurrentStateChanged += OnMediaElementCurrentStateChanged;
                };
                ···
            }
            ···
        }

        protected override void Dispose(bool disposing)
        {
            if (Control != null)
            {
                ···
                Control.CurrentStateChanged -= OnMediaElementCurrentStateChanged;
            }

            base.Dispose(disposing);
        }
        ···
    }
}
```

The `CurrentState` property is of type [`MediaElementState`](/uwp/api/windows.ui.xaml.media.mediaelementstate), and maps easily into `VideoStatus`:

```csharp
namespace FormsVideoLibrary.UWP
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, MediaElement>
    {
        ···
        void OnMediaElementCurrentStateChanged(object sender, RoutedEventArgs args)
        {
            VideoStatus videoStatus = VideoStatus.NotReady;

            switch (Control.CurrentState)
            {
                case MediaElementState.Playing:
                    videoStatus = VideoStatus.Playing;
                    break;

                case MediaElementState.Paused:
                case MediaElementState.Stopped:
                    videoStatus = VideoStatus.Paused;
                    break;
            }

            ((IVideoPlayerController)Element).Status = videoStatus;
        }
        ···
    }
}
```

## Play, Pause, and Stop Buttons

Using Unicode characters for symbolic **Play**, **Pause**, and **Stop** images is problematic. The [Miscellaneous Technical](https://unicode-table.com/en/blocks/miscellaneous-technical/) section of the Unicode standard defines three symbol characters seemingly appropriate for this purpose. These are:

- 0x23F5 (black medium right-pointing triangle) or &#x23F5; for **Play**
- 0x23F8 (double vertical bar) or &#x23F8; for **Pause**
- 0x23F9 (black square) or &#x23F9; for **Stop**

Regardless how these symbols appear in your browser (and different browsers handle them in different ways), they are not displayed consistently on the platforms supported by Xamarin.Forms. On iOS and UWP devices, the **Pause** and **Stop** characters have a graphical appearance, with a blue 3D background and a white foreground. This isn't the case on Android, where the symbol is simply blue. However, the 0x23F5 codepoint for **Play** does not have that same appearance on the UWP, and it's not even supported on iOS and Android.

For that reason, the 0x23F5 codepoint can't be used for **Play**. A good substitute is:

- 0x25B6 (black right-pointing triangle) or &#x25B6; for **Play**

This is supported by each platform except that it's a plain black triangle that does not resemble the 3D appearance of **Pause** and **Stop**. One possibility is to follow the 0x25B6 codepoint with a variant code:

- 0x25B6 followed by 0xFE0F (variant 16) or &#x25B6;&#xFE0F; for **Play**

This is what's used in the markup shown below. On iOS, it gives the **Play** symbol the same 3D appearance as the **Pause** and **Stop** buttons, but the variant doesn't work on Android and the UWP.

The **Custom Transport** page sets the **AreTransportControlsEnabled** property to **false** and includes an `ActivityIndicator` displayed when the video is loading, and two buttons. `DataTrigger` objects are used to enable and disable the `ActivityIndicator` and the buttons, and to switch the first button between **Play** and **Pause**:

```xaml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:video="clr-namespace:FormsVideoLibrary"
             x:Class="VideoPlayerDemos.CustomTransportPage"
             Title="Custom Transport">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="*" />
            <RowDefinition Height="Auto" />
        </Grid.RowDefinitions>

        <video:VideoPlayer x:Name="videoPlayer"
                           Grid.Row="0"
                           AutoPlay="False"
                           AreTransportControlsEnabled="False"
                           Source="{StaticResource BigBuckBunny}" />

        <ActivityIndicator Grid.Row="0"
                           Color="Gray"
                           IsVisible="False">
            <ActivityIndicator.Triggers>
                <DataTrigger TargetType="ActivityIndicator"
                             Binding="{Binding Source={x:Reference videoPlayer},
                                               Path=Status}"
                             Value="{x:Static video:VideoStatus.NotReady}">
                    <Setter Property="IsVisible" Value="True" />
                    <Setter Property="IsRunning" Value="True" />
                </DataTrigger>
            </ActivityIndicator.Triggers>
        </ActivityIndicator>

        <StackLayout Grid.Row="1"
                     Orientation="Horizontal"
                     Margin="0, 10"
                     BindingContext="{x:Reference videoPlayer}">

            <Button Text="&#x25B6;&#xFE0F; Play"
                    HorizontalOptions="CenterAndExpand"
                    Clicked="OnPlayPauseButtonClicked">
                <Button.Triggers>
                    <DataTrigger TargetType="Button"
                                 Binding="{Binding Status}"
                                 Value="{x:Static video:VideoStatus.Playing}">
                        <Setter Property="Text" Value="&#x23F8; Pause" />
                    </DataTrigger>

                    <DataTrigger TargetType="Button"
                                 Binding="{Binding Status}"
                                 Value="{x:Static video:VideoStatus.NotReady}">
                        <Setter Property="IsEnabled" Value="False" />
                    </DataTrigger>
                </Button.Triggers>
            </Button>

            <Button Text="&#x23F9; Stop"
                    HorizontalOptions="CenterAndExpand"
                    Clicked="OnStopButtonClicked">
                <Button.Triggers>
                    <DataTrigger TargetType="Button"
                                 Binding="{Binding Status}"
                                 Value="{x:Static video:VideoStatus.NotReady}">
                        <Setter Property="IsEnabled" Value="False" />
                    </DataTrigger>
                </Button.Triggers>
            </Button>
        </StackLayout>
    </Grid>
</ContentPage>
```

Data triggers are described in detail in the article [Data Triggers](~/xamarin-forms/app-fundamentals/triggers.md#data-triggers).

The code-behind file has the handlers for the button `Clicked` events:

```csharp
namespace VideoPlayerDemos
{
    public partial class CustomTransportPage : ContentPage
    {
        public CustomTransportPage()
        {
            InitializeComponent();
        }

        void OnPlayPauseButtonClicked(object sender, EventArgs args)
        {
            if (videoPlayer.Status == VideoStatus.Playing)
            {
                videoPlayer.Pause();
            }
            else if (videoPlayer.Status == VideoStatus.Paused)
            {
                videoPlayer.Play();
            }
        }

        void OnStopButtonClicked(object sender, EventArgs args)
        {
            videoPlayer.Stop();
        }
    }
}
```

Because `AutoPlay` is set to `false` in the **CustomTransport.xaml** file, you'll need to press the **Play** button when it becomes enabled to begin the video. The buttons are defined so that the Unicode characters discussed above are accompanied by their text equivalents. The buttons have a consistent appearance on each platform when the video is playing:

[![Custom Transport Playing](custom-transport-images/customtransportplaying-small.png "Custom Transport Playing")](custom-transport-images/customtransportplaying-large.png#lightbox "Custom Transport Playing")

But on Android and UWP, the **Play** button looks very different when the video is paused:

[![Custom Transport Paused](custom-transport-images/customtransportpaused-small.png "Custom Transport Paused")](custom-transport-images/customtransportpaused-large.png#lightbox "Custom Transport Paused")

In a production application, you'll probably want to use your own bitmap images for the buttons to achieve visual uniformity.

## Related Links

- [Video Player Demos (sample)](/samples/xamarin/xamarin-forms-samples/customrenderers-videoplayerdemos)