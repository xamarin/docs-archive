
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AppLinksAndroid
{
	[Activity (Label = "Product Details")]
	[IntentFilter(new [] {Android.Content.Intent.ActionView },
		DataScheme="example",
		DataHost="products",
		Categories=new [] { Android.Content.Intent.CategoryDefault })]
	public class ProductActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.ProductLayout);

			var id = "No Product ID Found";

			if (Intent.HasExtra ("al_applink_data")) {

				var appLinkData = Intent.GetStringExtra ("al_applink_data");

				var alUrl = new Rivets.AppLinkUrl (Intent.Data.ToString (), appLinkData);

				// InputQueryParameters will contain our product id
				if (alUrl != null && alUrl.InputQueryParameters.ContainsKey ("id"))
					id = alUrl.InputQueryParameters ["id"];
			}

			FindViewById<TextView> (Resource.Id.textViewProductId).Text = id;
		}
	}
}

