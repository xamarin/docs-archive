---
id: EEEF63AF-44A6-4F84-82DD-CAA44CACAD39
title: "Audio in CocosSharp"
brief: "How to play sound and music in CocosSharp"
---

# Recipe

Audio can be played using the `CCAudioEngine` class. Background music is streamed from device, and is typically represented by a compressed file format. Sound effects are played from uncompressed files. 

The following code shows how to play background music:

```
CCAudioEngine.SharedEngine.PlayBackgroundMusic (
    filename:"FruityFalls",
    loop:false);
```

The following code shows how to play a sound effect:

```
CCAudioEngine.SharedEngine.PlayEffect (filename:"Electricity");
```

CocosSharp supports the MonoGame content pipeline types (.xnb files) for music and sound effects. For information on using the content pipeline, see the [CocosSharp Content Pipeline Guide](https://developer.xamarin.com/guides/cross-platform/game_development/cocossharp/content_pipeline/).

For a full example on playing audio, see the [CCAudioEngine sample](/samples/mobile/CCAudioEngine/).
