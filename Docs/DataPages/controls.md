---
title: "DataPages Controls Reference"
description: "This article introduces the controls that are available in the Xamarin.Forms DataPages NuGet package."
ms.prod: xamarin
ms.assetid: 891615D0-E8BD-4ACC-A7F0-4C3725FBCC31
ms.technology: xamarin-forms
author: davidbritch
ms.author: dabritch
ms.date: 12/01/2017
no-loc: [Xamarin.Forms, Xamarin.Essentials]
---

# DataPages Controls Reference

![This API is currently in preview](~/media/shared/preview.png)

> [!IMPORTANT]
> DataPages requires a Xamarin.Forms Theme reference to render. This involves installing the [Xamarin.Forms.Theme.Base](https://www.nuget.org/packages/Xamarin.Forms.Theme.Base/) NuGet package into your project, followed by either the [Xamarin.Forms.Theme.Light](https://www.nuget.org/packages/Xamarin.Forms.Theme.Light/) or [Xamarin.Forms.Theme.Dark](https://www.nuget.org/packages/Xamarin.Forms.Theme.Dark/) NuGet packages.

The Xamarin.Forms DataPages NuGet includes a number of controls that can
take advantage of data source binding.

To use these controls in XAML, ensure the namespace has been included,
for example see the `xmlns:pages` declaration below:

```xaml
<ContentPage
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:pages="clr-namespace:Xamarin.Forms.Pages;assembly=Xamarin.Forms.Pages"
    x:Class="DataPagesDemo.Detail">
```

The examples below include `DynamicResource` references which would need
to exist in the project's resources dictionary to work. There is
also an example of how to build a [custom control](#custom-control-example).

## Built-in Controls

* [HeroImage](#heroimage)
* [ListItem](#listitem)

### HeroImage

The `HeroImage` control has four properties:

* Text
* Detail
* ImageSource
* Aspect

```xaml
<pages:HeroImage
    ImageSource="{ DynamicResource HeroImageImage }"
    Text="Keith Ballinger"
    Detail="Xamarin"
/>
```

**Android**

![HeroImage Control on Android](controls-images/heroimage-light-android.png) ![HeroImage Control on Android](controls-images/heroimage-dark-android.png)

**iOS**

![HeroImage Control on iOS](controls-images/heroimage-light-ios.png) ![HeroImage Control on iOS](controls-images/heroimage-dark-ios.png)

### ListItem

The `ListItem` control's layout is similar to native iOS and Android list or table rows,
however it can also be used as a regular view. In the example code below it
is shown hosted inside a `StackLayout`, but it can also be used in
data-bound scolling list controls.

There are five properties:

* Title
* Detail
* ImageSource
* PlaceholdImageSource
* Aspect

```xaml
<StackLayout Spacing="0">
    <pages:ListItemControl
        Detail="Xamarin"
        ImageSource="{ DynamicResource UserImage }"
        Title="Miguel de Icaza"
        PlaceholdImageSource="{ DynamicResource IconImage }"
    />
```

These screenshots show the `ListItem` on iOS and Android platforms using
both the Light and Dark themes:

**Android**

![ListItem Control on Android](controls-images/listitem-light-android.png) ![ListItem Control on Android](controls-images/listitem-dark-android.png)

**iOS**

![ListItem Control on iOS](controls-images/listitem-light-ios.png) ![ListItem Control on iOS](controls-images/listitem-dark-ios.png)

## Custom Control Example

The goal of this custom `CardView` control is to resemble the native Android CardView.

It will contain three properties:

* Text
* Detail
* ImageSource

The goal is a custom control that will look like the code below (note that a custom
`xmlns:local` is required that references the current assembly):

```xaml
<local:CardView
  ImageSource="{ DynamicResource CardViewImage }"
  Text="CardView Text"
  Detail="CardView Detail"
/>
```

It should look like the screenshots below using colors corresponding
to the built-in Light and Dark themes:

**Android**

![CardView Custom Control on Android](controls-images/cardview-light-android.png) ![CardView Custom Control on Android](controls-images/cardview-dark-android.png)

**iOS**

![CardView Custom Control on iOS](controls-images/cardview-light-ios.png) ![CardView Custom Control on iOS](controls-images/cardview-dark-ios.png)

### Building the Custom CardView

1. [DataView subclass](#1-dataview-subclass)
2. [Define Font, Layout, and Margins](#2-define-font-layout-and-margins)
3. [Create Styles for the Control's Children](#3-create-styles-for-the-controls-children)
4. [Create the Control Layout Template](#4-create-the-control-layout-template)
5. [Add the Theme-specific Resources](#5-add-the-theme-specific-resources)
6. [Set the ControlTemplate for the CardView class](#6-set-the-controltemplate-for-the-cardview-class)
7. [Add the Control to a Page](#7-add-the-control-to-a-page)

#### 1. DataView Subclass

The C# subclass of `DataView` defines the bindable properties for the control.

```csharp
public class CardView : DataView
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create ("Text", typeof (string), typeof (CardView), null, BindingMode.TwoWay);

    public string Text
    {
        get { return (string)GetValue (TextProperty); }
        set { SetValue (TextProperty, value); }
    }

    public static readonly BindableProperty DetailProperty =
        BindableProperty.Create ("Detail", typeof (string), typeof (CardView), null, BindingMode.TwoWay);

    public string Detail
    {
        get { return (string)GetValue (DetailProperty); }
        set { SetValue (DetailProperty, value); }
    }

    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create ("ImageSource", typeof (ImageSource), typeof (CardView), null, BindingMode.TwoWay);

    public ImageSource ImageSource
    {
        get { return (ImageSource)GetValue (ImageSourceProperty); }
        set { SetValue (ImageSourceProperty, value); }
    }

    public CardView()
    {
    }
}
```

#### 2. Define Font, Layout, and Margins

The control designer would figure out these values as part of the
user-interface design for the custom control. Where platform-specific
specifications are required, the `OnPlatform` element is used.

Note that some values refer to `StaticResource`s – these will be defined
in [step 5](#5-add-the-theme-specific-resources).

```xml
<!-- CARDVIEW FONT SIZES -->
<OnPlatform x:TypeArguments="x:Double" x:Key="CardViewTextFontSize">
        <On Platform="iOS, Android" Value="15" />
</OnPlatform>

<OnPlatform x:TypeArguments="x:Double" x:Key="CardViewDetailFontSize">
        <On Platform="iOS, Android" Value="13" />
</OnPlatform>

<OnPlatform x:TypeArguments="Color"    x:Key="CardViewTextTextColor">
        <On Platform="iOS" Value="{StaticResource iOSCardViewTextTextColor}" />
        <On Platform="Android" Value="{StaticResource AndroidCardViewTextTextColor}" />
</OnPlatform>

<OnPlatform x:TypeArguments="Thickness"    x:Key="CardViewTextlMargin">
        <On Platform="iOS" Value="12,10,12,4" />
        <On Platform="Android" Value="20,0,20,5" />
</OnPlatform>

<OnPlatform x:TypeArguments="Color"    x:Key="CardViewDetailTextColor">
        <On Platform="iOS" Value="{StaticResource iOSCardViewDetailTextColor}" />
        <On Platform="Android" Value="{StaticResource AndroidCardViewDetailTextColor}" />
</OnPlatform>

<OnPlatform x:TypeArguments="Thickness"    x:Key="CardViewDetailMargin">
        <On Platform="iOS" Value="12,0,10,12" />
        <On Platform="Android" Value="20,0,20,20" />
</OnPlatform>

<OnPlatform x:TypeArguments="Color"    x:Key="CardViewBackgroundColor">
        <On Platform="iOS" Value="{StaticResource iOSCardViewBackgroundColor}" />
        <On Platform="Android" Value="{StaticResource AndroidCardViewBackgroundColor}" />
</OnPlatform>

<OnPlatform x:TypeArguments="x:Double" x:Key="CardViewShadowSize">
        <On Platform="iOS" Value="2" />
        <On Platform="Android" Value="5" />
</OnPlatform>

<OnPlatform x:TypeArguments="x:Double" x:Key="CardViewCornerRadius">
        <On Platform="iOS" Value="0" />
        <On Platform="Android" Value="4" />
</OnPlatform>

<OnPlatform x:TypeArguments="Color"    x:Key="CardViewShadowColor">
        <On Platform="iOS, Android" Value="#CDCDD1" />
</OnPlatform>
```

#### 3. Create Styles for the Control's Children

Reference all the elements defined about to create the children that
will be used in the custom control:

```xml
<!-- EXPLICIT STYLES (will be Classes) -->
<Style TargetType="Label" x:Key="CardViewTextStyle">
    <Setter Property="FontSize" Value="{ StaticResource CardViewTextFontSize }" />
    <Setter Property="TextColor" Value="{ StaticResource CardViewTextTextColor }" />
    <Setter Property="HorizontalOptions" Value="Start" />
    <Setter Property="Margin" Value="{ StaticResource CardViewTextlMargin }" />
    <Setter Property="HorizontalTextAlignment" Value="Start" />
</Style>

<Style TargetType="Label" x:Key="CardViewDetailStyle">
    <Setter Property="HorizontalTextAlignment" Value="Start" />
    <Setter Property="TextColor" Value="{ StaticResource CardViewDetailTextColor }" />
    <Setter Property="FontSize" Value="{ StaticResource CardViewDetailFontSize }" />
    <Setter Property="HorizontalOptions" Value="Start" />
    <Setter Property="Margin" Value="{ StaticResource CardViewDetailMargin }" />
</Style>

<Style TargetType="Image" x:Key="CardViewImageImageStyle">
    <Setter Property="HorizontalOptions" Value="Center" />
    <Setter Property="VerticalOptions" Value="Center" />
    <Setter Property="WidthRequest" Value="220"/>
    <Setter Property="HeightRequest" Value="165"/>
</Style>
```

#### 4. Create the Control Layout Template

The visual design of the custom control is explicitly declared in the control
template, using the resources defined above:

```xml
<!--- CARDVIEW -->
<ControlTemplate x:Key="CardViewControlControlTemplate">
  <StackLayout
    Spacing="0"
    BackgroundColor="{ TemplateBinding BackgroundColor }"
  >

    <!-- CARDVIEW IMAGE -->
    <Image
      Source="{ TemplateBinding ImageSource }"
      HorizontalOptions="FillAndExpand"
      VerticalOptions="StartAndExpand"
      Aspect="AspectFill"
      Style="{ StaticResource CardViewImageImageStyle }"
    />

    <!-- CARDVIEW TEXT -->
    <Label
      Text="{ TemplateBinding Text }"
      LineBreakMode="WordWrap"
      VerticalOptions="End"
      Style="{ StaticResource CardViewTextStyle }"
    />

    <!-- CARDVIEW DETAIL -->
    <Label
      Text="{ TemplateBinding Detail }"
      LineBreakMode="WordWrap"
      VerticalOptions="End"
      Style="{ StaticResource CardViewDetailStyle }" />

  </StackLayout>

</ControlTemplate>
```

#### 5. Add the Theme-specific Resources

Because this is a custom control, add the resources that match
the theme you are using the resource dictionary:

##### Light Theme Colors

```xaml
<Color x:Key="iOSCardViewBackgroundColor">#FFFFFF</Color>
<Color x:Key="AndroidCardViewBackgroundColor">#FFFFFF</Color>

<Color x:Key="AndroidCardViewTextTextColor">#030303</Color>
<Color x:Key="iOSCardViewTextTextColor">#030303</Color>

<Color x:Key="AndroidCardViewDetailTextColor">#8F8E94</Color>
<Color x:Key="iOSCardViewDetailTextColor">#8F8E94</Color>
```

##### Dark Theme Colors

```xaml
<!-- CARD VIEW COLORS -->
            <Color x:Key="iOSCardViewBackgroundColor">#404040</Color>
            <Color x:Key="AndroidCardViewBackgroundColor">#404040</Color>

            <Color x:Key="AndroidCardViewTextTextColor">#FFFFFF</Color>
            <Color x:Key="iOSCardViewTextTextColor">#FFFFFF</Color>

            <Color x:Key="AndroidCardViewDetailTextColor">#B5B4B9</Color>
            <Color x:Key="iOSCardViewDetailTextColor">#B5B4B9</Color>
```

#### 6. Set the ControlTemplate for the CardView class

Finally, ensure the C# class created in [step 1](#1-dataview-subclass) uses the control template
defined in [step 4](#4-create-the-control-layout-template) using a `Style` `Setter` element

```xml
<Style TargetType="local:CardView">
    <Setter Property="ControlTemplate" Value="{ StaticResource CardViewControlControlTemplate }" />
  ... some custom styling omitted
  <Setter Property="BackgroundColor" Value="{ StaticResource CardViewBackgroundColor }" />
</Style>
```

#### 7. Add the Control to a Page

The `CardView` control can now be added to a page. The example below
shows it hosted in a `StackLayout`:

```xaml
<StackLayout Spacing="0">
  <local:CardView
    Margin="12,6"
    ImageSource="{ DynamicResource CardViewImage }"
    Text="CardView Text"
    Detail="CardView Detail"
  />
</StackLayout>

```
