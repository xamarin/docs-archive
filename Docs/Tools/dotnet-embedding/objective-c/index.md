---
title: "Objective-C Support"
description: "This document provides a description of the support for Objective-C in .NET Embedding. It discusses Automatic Reference Counting, NSString, Protocols, NSObject protocol, exceptions, and more."
ms.prod: xamarin
ms.assetid: 3367A4A4-EC88-4B75-96D0-51B1FCBCE614
author: davidortinau
ms.author: daortin
ms.date: 11/14/2017
---

# Objective-C Support

## Specific features

The generation of Objective-C has a few special features that are worth noting.

### Automatic Reference Counting

The use of Automatic Reference Counting (ARC) is **required** to call the generated bindings. Project using a .NET Embedding-based library must be compiled with `-fobjc-arc`.

### NSString support

APIs that expose `System.String` types are converted into `NSString`. This makes memory management easier than when dealing with `char*`.

### Protocols support

Managed interfaces are converted into Objective-C protocols where all members are `@required`.

### NSObject protocol support

By default, the default hashing and equality of both .NET and the Objective-C runtime are assumed to be interchangeable, as they share similar semantics.

When a managed type overrides `Equals(Object)` or `GetHashCode`, it generally means that the default (.NET) behavior was not sufficient; this implies that the default Objective-C behavior is likely not sufficient either.

In such cases, the generator overrides the [`isEqual:`](https://developer.apple.com/reference/objectivec/1418956-nsobject/1418795-isequal?language=objc) method and [`hash`](https://developer.apple.com/reference/objectivec/1418956-nsobject/1418859-hash?language=objc) property defined in the [`NSObject` protocol](https://developer.apple.com/reference/objectivec/1418956-nsobject?language=objc). This allows the custom managed implementation to be used from Objective-C code transparently.

### Exceptions support

Passing `--nativeexception` as an argument to `objcgen` will convert managed exceptions into Objective-C exceptions that can be caught and processed. 

### Comparison

Managed types that implement `IComparable` (or its generic version `IComparable<T>`) will produce Objective-C friendly methods that return a `NSComparisonResult` and accept a `nil` argument. This makes the generated API more friendly to Objective-C developers. For example:

```objc
- (NSComparisonResult)compare:(XAMComparableType * _Nullable)other;
```

### Categories

Managed extensions methods are converted into categories. For example, the following extension methods on `Collection`:

```csharp
public static class SomeExtensions {
    public static int CountNonNull (this Collection collection) { ... }
    public static int CountNull (this Collection collection) { ... }
}
```

would create an Objective-C category like this one:

```objc
@interface Collection (SomeExtensions)

- (int)countNonNull;
- (int)countNull;

@end
```

When a single managed type extends several types, multiple Objective-C categories are generated.

### Subscripting

Managed indexed properties are converted into object subscripting. For example:

```csharp
public bool this[int index] {
    get { return c[index]; }
    set { c[index] = value; }
}
```

would create Objective-C similar to:

```objc
- (id)objectAtIndexedSubscript:(int)idx;
- (void)setObject:(id)obj atIndexedSubscript:(int)idx;
```

which can be used via the Objective-C subscripting syntax:

```objc
if ([intCollection [0] isEqual:@42])
    intCollection[0] = @13;
```

Depending on the type of your indexer, indexed or keyed subscripting will be generated where appropriate.

This [article](https://nshipster.com/object-subscripting/) is a great introduction to subscripting.

## Main differences with .NET

### Constructors vs initializers

In Objective-C, you can call any of the initializer prototypes of any of the parent classes in the inheritance chain, unless it is marked as unavailable (`NS_UNAVAILABLE`).

In C# you must explicitly declare a constructor member inside a class, which means constructors are not inherited.

To expose the right representation of the C# API to Objective-C, `NS_UNAVAILABLE` is added to any initializer that is not present in the child class from the parent class.

C# API:

```csharp
public class Unique {
    public Unique () : this (1)
    {
    }

    public Unique (int id)
    {
    }
}

public class SuperUnique : Unique {
    public SuperUnique () : base (911)
    {
    }
}
```

Objective-C surfaced API:

```objc
@interface SuperUnique : Unique

- (instancetype)initWithId:(int)id NS_UNAVAILABLE;
- (instancetype)init;

@end
```

Here, `initWithId:` has been marked as unavailable.

### Operator

Objective-C does not support operator overloading as C# does, so operators are converted to class selectors:

```csharp
public static AllOperators operator + (AllOperators c1, AllOperators c2)
{
    return new AllOperators (c1.Value + c2.Value);
}
```

to

```objc
+ (instancetype)add:(Overloads_AllOperators *)anObjectC1 c2:(Overloads_AllOperators *)anObjectC2;
```

However, some .NET languages do not support operator overloading, so it is common to also include a ["friendly"](/dotnet/standard/design-guidelines/operator-overloads) named method in addition to the operator overload.

If both the operator version and the "friendly" version are found, only the friendly version will be generated, as they will generate to the same Objective-C name.

```csharp
public static AllOperatorsWithFriendly operator + (AllOperatorsWithFriendly c1, AllOperatorsWithFriendly c2)
{
    return new AllOperatorsWithFriendly (c1.Value + c2.Value);
}

public static AllOperatorsWithFriendly Add (AllOperatorsWithFriendly c1, AllOperatorsWithFriendly c2)
{
    return new AllOperatorsWithFriendly (c1.Value + c2.Value);
}
```

becomes:

```objc
+ (instancetype)add:(Overloads_AllOperatorsWithFriendly *)anObjectC1 c2:(Overloads_AllOperatorsWithFriendly *)anObjectC2;
```

### Equality operator

In general operator `==` in C# is handled as a general operator as noted above.

However, if the "friendly" Equals operator is found, both operator `==` and operator `!=` will be skipped in generation.

### DateTime vs NSDate

From the [`NSDate`](https://developer.apple.com/reference/foundation/nsdate?language=objc) documentation:

> `NSDate` objects encapsulate a single point in time, independent of any particular calendrical system or time zone. Date objects are immutable, representing an invariant time interval relative to an absolute reference date (00:00:00 UTC on 1 January 2001).

Due to `NSDate` reference date, all conversions between it and `DateTime` must be done in UTC.

#### DateTime to NSDate

When converting from `DateTime` to `NSDate`, the `Kind` property on `DateTime` is taken into account:

|Kind|Results|
|---|---|
|`Utc`|Conversion is performed using the provided `DateTime` object as is.|
|`Local`|The result of calling `ToUniversalTime()` in the provided `DateTime` object is used for conversion.|
|`Unspecified`|The provided `DateTime` object is assumed to be UTC, so same behavior when `Kind` is `Utc`.|

The conversion uses the following formula:

```
TimeInterval = DateTimeObjectTicks - NSDateReferenceDateTicks / TicksPerSecond
```

In this formula: 

- `NSDateReferenceDateTicks` is calculated based on the `NSDate` reference date of 00:00:00 UTC on 1 January 2001: 

    ```csharp
    new DateTime (year:2001, month:1, day:1, hour:0, minute:0, second:0, kind:DateTimeKind.Utc).Ticks;
    ```

- [`TicksPerSecond`](/dotnet/api/system.timespan.tickspersecond) is defined on [`TimeSpan`](/dotnet/api/system.timespan)

To create the `NSDate` object, the `TimeInterval` is used with the `NSDate` [dateWithTimeIntervalSinceReferenceDate:](https://developer.apple.com/reference/foundation/nsdate/1591577-datewithtimeintervalsincereferen?language=objc) selector.

#### NSDate to DateTime

The conversion from `NSDate` to `DateTime` uses the following formula:

```
DateTimeTicks = NSDateTimeIntervalSinceReferenceDate * TicksPerSecond + NSDateReferenceDateTicks
```

In this formula: 

- `NSDateReferenceDateTicks` is calculated based on the `NSDate` reference date of 00:00:00 UTC on 1 January 2001: 

    ```csharp
    new DateTime (year:2001, month:1, day:1, hour:0, minute:0, second:0, kind:DateTimeKind.Utc).Ticks;
    ```

- [`TicksPerSecond`](/dotnet/api/system.timespan.tickspersecond) is defined on [`TimeSpan`](/dotnet/api/system.timespan)

After calculating `DateTimeTicks`, the `DateTime` [constructor](/dotnet/api/system.datetime.-ctor#System_DateTime__ctor_System_Int64_System_DateTimeKind_) is invoked, setting its `kind` to `DateTimeKind.Utc`.

> [!NOTE]
> `NSDate` can be `nil`, but a `DateTime` is a struct in .NET, which by definition can't be `null`. If you give a `nil` `NSDate`, it will be translated to the default `DateTime` value, which maps to `DateTime.MinValue`.

`NSDate` supports a higher maximum and a lower minimum value than `DateTime`. When converting from `NSDate` to `DateTime`, these higher and lower values are changed to the `DateTime` [MaxValue](/dotnet/api/system.datetime.maxvalue) or [MinValue](/dotnet/api/system.datetime.minvalue), respectively.