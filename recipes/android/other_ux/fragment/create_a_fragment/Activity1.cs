using System;
using Android.App;
using Android.OS;

namespace CreateAFragment
{
    [Activity(Label = "CreateAFragment",
        MainLauncher = true,
        Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);
                SetContentView(Resource.Layout.Main);
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e);
            }
        }
    }
}