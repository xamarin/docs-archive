using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;

namespace SimpleService
{
    [Service]
    public class SimpleService : Service
    {
        Timer _timer;



        public override void OnStart(Intent intent, int startId)
        {
            base.OnStart(intent, startId);

            Log.Debug("SimpleService", "SimpleService started");

            DoStuff();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            _timer.Dispose();

            Log.Debug("SimpleService", "SimpleService stopped");
        }

        public void DoStuff()
        {
            _timer = new Timer(o => { Log.Debug("SimpleService", "hello from simple service"); }
                               , null, 0, 4000);
        }

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }
    }
}