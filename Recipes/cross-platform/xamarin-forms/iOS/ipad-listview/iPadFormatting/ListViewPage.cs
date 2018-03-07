using System;

using Xamarin.Forms;
using System.Collections.Generic;

namespace iPadFormatting
{
	public class ListViewPage : ContentPage
	{
		public ListViewPage ()
		{
			Title = "ListView";
			var lv = new ListView ();

			lv.ItemsSource = new List<string> {
				"alpha", "beta", "gamma", "delta", "epsilon"
			};

			Content = lv;
		}
	}
}


