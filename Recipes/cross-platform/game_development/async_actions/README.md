---
id: 862D8299-099D-474B-9604-C869E31F4B0E
title: "Async CCActions"
brief: "How to use async CCActions to script behavior"
---

# Recipe

## Introduction

The `CCAction` class simplifies common animated behaviors such as moving, scaling, or rotating an object over a set amount of time. The CCNode class provides methods to run CCAction instances async. Async actions can be used to perform scripted sequences.

## Using RunActionsAsync

The following code shows how to move a CCNode named player in a square. The await keyword is used so that each RunAsyncAction call waits until the previous finishes before it begins. The end result is that the player moves in a square four times:

```
// Set the initial position:
player.PositionX = 100;
player.PositionY = 100;

// Define the four parts of the animation, which are move up:
var moveTo1 = new CCMoveTo(duration:3, position:new CCPoint(100, 300));

// move right:
var moveTo2 = new CCMoveTo(duration:3, position:new CCPoint(300, 300));

// move down:
var moveTo3 = new CCMoveTo(duration:3, position:new CCPoint(300, 100));

// move left (back to the original position):
var moveTo4 = new CCMoveTo(duration:3, position:new CCPoint(100, 100));

// We can call RunActionAsync to play these in sequence. Doing so with
// the await keyword results in the animation playing 4 times
await player.RunActionsAsync (moveTo1, moveTo2, moveTo3, moveTo4);
await player.RunActionsAsync (moveTo1, moveTo2, moveTo3, moveTo4);
await player.RunActionsAsync (moveTo1, moveTo2, moveTo3, moveTo4);
await player.RunActionsAsync (moveTo1, moveTo2, moveTo3, moveTo4);
```
