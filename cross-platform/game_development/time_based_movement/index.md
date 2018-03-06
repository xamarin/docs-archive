---
id: 2ACFC981-107F-4CAB-9D68-9F090A9FA533
title: "Time Based Movement in CocosSharp"
brief: "How to move an object independent of frame rate"
---

# Recipe

Moving game objects should use time when calculating how far to move each frame so that behavior is consistent regardless of frame rate.

Movement can be handled through a `Schedule` call, which expects an `Action<float>`. The `float` parameter is the number of seconds since last frame, so it can be used to perform time based movement. First `Schedule` must be called:

```
// assuming this is called in an object that inherits from CCNode, such as an entity:
this.Schedule(PerformMovement);
```

`PerformMovement` implements the movement logic as follows:

```
void PerformMovement(float timeInSeconds)
{
	float velocity = 10; // in pixels per second
	this.PositionX += velocity * timeInSeconds;
}
```


