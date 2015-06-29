using System;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Locations;
using System.Threading;
using System.Linq;

namespace GeocodeAddress
{
    [Activity (Label = "GeocodeAddress", MainLauncher = true)]
    public class Activity1 : Activity
    {

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.Main);
            
            var button = FindViewById<Button> (Resource.Id.geocodeButton);
            
            button.Click += (sender, e) => {
                
                new Thread (new ThreadStart (() => {
                    var geo = new Geocoder (this);
                
                    var addresses = geo.GetFromLocationName ("50 Church St, Cambridge, MA", 1);
                    
                    RunOnUiThread (() => {
                        var addressText = FindViewById<TextView> (Resource.Id.addressText);
         
                        addresses.ToList ().ForEach ((addr) => {
                            addressText.Append (addr.ToString () + "\r\n\r\n");
                        });
                    });
             
                })).Start ();
            };
        }

    }
}