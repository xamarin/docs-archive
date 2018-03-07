---
id: E6A3644B-3F75-4AFA-A657-D43DB0869460
title: "Scrolling a CCLayer"
brief: "How to create a scrolling effect"
---

# Recipe

## Introduction

Many games require a scrolling environment. CocosSharp supports scrolling through a Layer's CCCamera instance, as well as by modifying the ```CCLayer.Position``` value.

## Scrolling CCCamera Example

A layer's `CCCamera` can be adjusted to create a scrolling effect. This approach to scrolling should be used if the user is moving through an environment, such as the camera following a character throughout a large world.

The following shows how to scroll a layer at a constant rate by adjusting a the `CCLayer.Camera`. First, `Schedule` must be called:

```
// assuming this is called in an object that inherits from CCNode, such as an entity:
this.Schedule(PerformScrolling);
```

`PerformScrolling` implements the scrolling logic as follows:

```
void PerformScrolling(float timeInSeconds)
{
	const float pixelsPerSecond = 100;

	// This moves the location of the camera:
	var center = layerToScroll.Camera.CenterInWorldspace;
	center.X += pixelsPerSecond * timeInSeconds;
	layerToScroll.Camera.CenterInWorldspace = center;

	// This moves where the camera is looking.
	// If we don't do this, the camera continues
	// to look at its center location, resulting in
	// perspective being applied to the scene:
	var target = layerToScroll.Camera.TargetInWorldspace;
	target.X = center.X;
	target.Y = center.Y;
	layerToScroll.Camera.TargetInWorldspace = target;
}
```
The code above assumes that `layerToScroll` is a valid layer which should scroll at the given rate. This code can be added to a `CCLayer` (such as the default `GameLayer`), in which case `layerToScroll` should be changed to `this`.

## Scrolling CCLayer.Position Example

A layer's position values can be adjusted to create a scrolling effect. This approach to scrolling should be used if the objects contained in the layer are physically moving, such as clouds scrolling in the background.

The following shows how to scroll a layer at a constant rate by adjusting the `CCLayer.Position`. First, `Schedule` must be called:

```
// assuming this is called in an object that inherits from CCNode, such as an entity:
this.Schedule(PerformScrolling);
```

`PerformScrolling` implements the scrolling logic as follows:

```
void TimeBasedMovement(float timeInSeconds)
{
	const float pixelsPerSecond = 100;

    // We move the layer to the left to simulate the camera moving to the right:
	layerToScroll.PositionX -= pixelsPerSecond * timeInSeconds;
}
```
