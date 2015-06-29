using System.Linq;
using Android.App;
using Android.Locations;
using Android.OS;
using Android.Widget;

namespace ReverseGeocode
{
		[Activity (Label = "ReverseGeocode", MainLauncher = true)]
		public class Activity1 : Activity
		{
				protected override void OnCreate (Bundle bundle)
				{
						base.OnCreate (bundle);

						SetContentView (Resource.Layout.Main);
            
						var button = FindViewById<Button> (Resource.Id.revGeocodeButton);

						button.Click += async (sender, e) => {
								var geo = new Geocoder (this);
								var addresses = await geo.GetFromLocationAsync (42.37419, -71.120639, 1);

								var addressText = FindViewById<TextView> (Resource.Id.addressText);
								if (addresses.Any ()) {
										addresses.ToList ().ForEach (addr => addressText.Append (addr + System.Environment.NewLine + System.Environment.NewLine));
								} else {
										addressText.Text = "Could not find any addresses.";
								}
						};
			            
				}
		}
}