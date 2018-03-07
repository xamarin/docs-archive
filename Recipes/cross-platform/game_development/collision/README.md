---
id: 16BD8CA4-2320-4CCC-A1D9-F032920978F2
title: "Collision in CocosSharp"
brief: "How to test for collision in CocosSharp"
---


# Recipe

## Detecting Collisions

`CCSprite` provides a `BoundingBox` property which can be used to detect collision. For example, detecting a collision between a bullet `CCSprite` and an enemy `CCSprite` could be done as follows:

```
if (bulletSprite.BoundingBox.IntersectsRect (enemySprite.BoundingBox))
{
	// React to collision
}
```

If the sprites are attached to different `CCNode` parents, then using `BoundingBox` property (which is relative to the parent of the `CCSprite`) may cause unexpected results. The `BoundingBoxTransformedToWorld` property will detect collision correctly regardless even if `CCSprite` instances have different parents:

```
if (bulletSprite.BoundingBoxTransformedToWorld.IntersectsRect (enemySprite.BoundingBoxTransformedToWorld))
{
	// React to collision
}
```


## Separating rectangles

Some games include collidable objects which should not overlap, such as a player object and walls in a maze. In some cases one of the objects will be static (such as an immovable wall), while in other cases both objects can be moved. To enable this the first step is to find the *separation vector*, which contains an X and Y value that an object should be moved by to separate the colliding objects. Once the reposition vector is obtained, it can be applied wholly to one of the two colliding objects (making the other static), or the vector can be applied proportionally according to desired relative mass.

The following shows how to obtain the separation vector from two `CCRect` instances:

```
// Returns the vector that the 'first' should be moved by
// to separate the objects. 
CCVector2 GetSeparatingVector(CCRect first, CCRect second)
{
	// Default to no separation
	CCVector2 separation = CCVector2.Zero;

	// Only calculate separation if the rectangles intersect
	if (first.IntersectsRect (second))
	{
		// The intersectionRect returns the rectangle produced
		// by overlapping the two rectangles
		var intersectionRect = first.Intersection (second);

		// Separation should occur by moving the minimum distance
		// possible. We do this by checking which is smaller: width or height?
		bool separateHorizontally = intersectionRect.Size.Width < intersectionRect.Size.Height;

		if (separateHorizontally)
		{
			separation.X = intersectionRect.Size.Width;
			// Since separation is from the perspective
			// of 'first', the value should be negative if
			// the first is to the left of the second.
			if (first.Center.X < second.Center.X)
			{
				separation.X *= -1;
			}
			separation.Y = 0;
		}
		else
		{
			separation.X = 0;

			separation.Y = intersectionRect.Size.Height;
			if (first.Center.Y < second.Center.Y)
			{
				separation.Y *= -1;
			}
		}
	}

	return separation;
}

```

The following shows how a player can collide against a solid wall:

```
// Assuming player and wall are both CCSprite instances:
var separatingVector = GetSeparatingVector( player.BoundingBoxTransformedToWorld, wall.BoundingBoxTransformedToWorld);

player.PositionX += separatingVector.X;
player.PositionY += separatingVector.Y;
```

The following shows how a player can collide against a movable block. The code is written such that the block is twice as massive as the player:

```
var separatingVector = GetSeparatingVector( player.BoundingBoxTransformedToWorld, block.BoundingBoxTransformedToWorld);

// First we define the masses
float playerMass = 1;
float blockMass = 2;

// We need the total mass to calculate ratios
float totalMass = playerMass + blockMass;

// The heaver an object is, 
// the less it will move (smaller ratio)
float playerMovementRatio = 1 - (playerMass / totalMass);
float blockMovementRatio = 1 - (blockMass / totalMass);
	

player.PositionX += separatingVector.X * playerMovementRatio;
player.PositionY += separatingVector.Y * playerMovementRatio;

// reposition vector is relative to the 'first' object, which
// is the player. We need to invert the vector for the block
block.PositionX -= separatingVector.X * blockMovementRatio;
block.PositionY -= separatingVector.Y * blockMovementRatio;

```


