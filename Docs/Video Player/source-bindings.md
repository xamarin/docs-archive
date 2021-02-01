---
title: "Binding video sources to the player"
description: "This article explains how to bind video sources to the video player, using Xamarin.Forms."
ms.prod: xamarin
ms.assetid: 504E0C7E-051A-4AF2-B654-BAB4D0957928
ms.technology: xamarin-forms
author: davidbritch
ms.author: dabritch
ms.date: 02/12/2018
no-loc: [Xamarin.Forms, Xamarin.Essentials]
---

# Binding video sources to the player

[![Download Sample](~/media/shared/download.png) Download the sample](/samples/xamarin/xamarin-forms-samples/customrenderers-videoplayerdemos)

When the `Source` property of the `VideoPlayer` view is set to a new video file, the existing video stops playing and the new video begins. This is demonstrated by the **Select Web Video** page of the [**VideoPlayerDemos**](/samples/xamarin/xamarin-forms-samples/customrenderers-videoplayerdemos) sample. The page includes a `ListView` with the titles of the three videos referenced from the **App.xaml** file:

```xaml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:video="clr-namespace:FormsVideoLibrary"
             x:Class="VideoPlayerDemos.SelectWebVideoPage"
             Title="Select Web Video">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="2*" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>

        <video:VideoPlayer x:Name="videoPlayer"
                           Grid.Row="0" />

        <ListView Grid.Row="1"
                  ItemSelected="OnListViewItemSelected">
            <ListView.ItemsSource>
                <x:Array Type="{x:Type x:String}">
                    <x:String>Elephant's Dream</x:String>
                    <x:String>Big Buck Bunny</x:String>
                    <x:String>Sintel</x:String>
                </x:Array>
            </ListView.ItemsSource>
        </ListView>
    </Grid>
</ContentPage>
```

When a video is selected, the `ItemSelected` event handler in the code-behind file is executed. The handler removes any blanks and apostrophes from the title and uses that as a key to obtain one of the resources defined in the **App.xaml** file. That `UriVideoSource` object is then set to the `Source` property of the `VideoPlayer`.

```csharp
namespace VideoPlayerDemos
{
    public partial class SelectWebVideoPage : ContentPage
    {
        public SelectWebVideoPage()
        {
            InitializeComponent();
        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                string key = ((string)args.SelectedItem).Replace(" ", "").Replace("'", "");
                videoPlayer.Source = (UriVideoSource)Application.Current.Resources[key];
            }
        }
    }
}
```

When the page first loads, no item is selected in the `ListView`, so you must select one for the video to begin playing:

[![Select Web Video](source-bindings-images/selectwebvideo-small.png "Select Web Video")](source-bindings-images/selectwebvideo-large.png#lightbox "Select Web Video")

The `Source` property of `VideoPlayer` is backed by a bindable property, which means that it can be the target of a data binding. This is demonstrated by the **Bind to VideoPlayer** page. The markup in the **BindToVideoPlayer.xaml** file is supported by the following class that encapsulates a title of a video and a corresponding `VideoSource` object:

```csharp
namespace VideoPlayerDemos
{
    public class VideoInfo
    {
        public string DisplayName { set; get; }

        public VideoSource VideoSource { set; get; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
```

The `ListView` in the **BindToVideoPlayer.xaml** file contains an array of these `VideoInfo` objects, each one initialized with a video title and the `UriVideoSource` object from the resource dictionary in **App.xaml**:

```xaml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:VideoPlayerDemos"
             xmlns:video="clr-namespace:FormsVideoLibrary"
             x:Class="VideoPlayerDemos.BindToVideoPlayerPage"
             Title="Bind to VideoPlayer">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="2*" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>

        <video:VideoPlayer x:Name="videoPlayer"
                           Grid.Row="0"
                           Source="{Binding Source={x:Reference listView},
                                            Path=SelectedItem.VideoSource}" />

        <ListView x:Name="listView"
                  Grid.Row="1">
            <ListView.ItemsSource>
                <x:Array Type="{x:Type local:VideoInfo}">
                    <local:VideoInfo DisplayName="Elephant's Dream"
                                     VideoSource="{StaticResource ElephantsDream}" />

                    <local:VideoInfo DisplayName="Big Buck Bunny"
                                     VideoSource="{StaticResource BigBuckBunny}" />

                    <local:VideoInfo DisplayName="Sintel"
                                     VideoSource="{StaticResource Sintel}" />
                </x:Array>
            </ListView.ItemsSource>
        </ListView>
    </Grid>
</ContentPage>
```

The `Source` property of the `VideoPlayer` is bound to the `ListView`. The `Path` of the binding is specified as `SelectedItem.VideoSource`, which is a compound path consisting of two properties: `SelectedItem` is a property of `ListView`. The selected item is of type `VideoInfo`, which has a `VideoSource` property.

As with the first **Select Web Video** page, no item is initially selected from the `ListView`, so you need to select one of the videos before it begins playing.

## Related Links

- [Video Player Demos (sample)](/samples/xamarin/xamarin-forms-samples/customrenderers-videoplayerdemos)