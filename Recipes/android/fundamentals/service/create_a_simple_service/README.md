---
id: DCEC2052-373D-C101-30F0-6BFA78C498AB
title: "Create a Simple Service"
brief: "This recipe shows how to create a service."
dateupdated: 2016-12-12
---


# Recipe

This recipe will demonstrate how to start and stop an Android service using an explicit Intent.

![](Images/simpleservice1.png)

1.  Create a new Xamarin.Android application named **SimpleService**.
2.  Add a new class named **SimpleService**, setting its base class to `Android.App.Service`.
3.  Replace the code in the **SimpleService** with the following:

```csharp
using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;

namespace SimpleService
{

    [Service]
    public class SimpleStartedService : Service
    {
        static readonly string TAG = "X:" + typeof(SimpleStartedService).Name;
        static readonly int TimerWait = 4000;
        Timer timer;
        DateTime startTime;
        bool isStarted = false;
    
        public override void OnCreate()
        {
            base.OnCreate();
        }
    
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Log.Debug(TAG, $"OnStartCommand called at {startTime}, flags={flags}, startid={startId}");
            if (isStarted)
            {
                TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
                Log.Debug(TAG, $"This service was already started, it's been running for {runtime:c}.");
            }
            else
            {
                startTime = DateTime.UtcNow;
                Log.Debug(TAG, $"Starting the service, at {startTime}.");
                timer = new Timer(HandleTimerCallback, startTime, 0, TimerWait);
                isStarted = true;
            }
            return StartCommandResult.NotSticky;
        }
    
        public override IBinder OnBind(Intent intent)
        {
            // This is a started service, not a bound service, so we just return null.
            return null;
        }
    
    
        public override void OnDestroy()
        {
            timer.Dispose();
            timer = null;
            isStarted = false;
    
            TimeSpan runtime = DateTime.UtcNow.Subtract(startTime);
            Log.Debug(TAG, $"Simple Service destroyed at {DateTime.UtcNow} after running for {runtime:c}.");
            base.OnDestroy();
        }
    
        void HandleTimerCallback(object state)
        {
            TimeSpan runTime = DateTime.UtcNow.Subtract(startTime);
            Log.Debug(TAG, $"This service has been running for {runTime:c} (since ${state})." );
        }
    }
}
```

This code will use an  `Timer` to write a message to the application log every four seconds.  

<a name="Additional_Information" class="injected"></a>

# Additional Information

Android services are well suited to long running tasks such as completing a web upload, playing background audio and receiving location updates.

