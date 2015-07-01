using System;
using System.Diagnostics;

using Android.Util;

namespace Xample.BlurImage
{
    public class Timer : IDisposable
    {
        readonly string TAG = "BlurImage_TimeElapsed";
        readonly string _message;
        readonly Stopwatch _stopWatch = Stopwatch.StartNew();

        public Timer(string message)
        {
            _message = message;
        }

        public void Dispose()
        {
            _stopWatch.Stop();
            Log.Debug(TAG, _message + " " + _stopWatch.Elapsed);
        }
    }
}
