using System;
using Xamarin.Forms;

namespace ScreenOrientation
{
	public class HomePageCS : ContentPage
	{
		Image image;

		public HomePageCS ()
		{
			image = new Image ();
			Content = new Grid { 
				Children = {
					image					
				}
			};

			SizeChanged += OnSizeChanged;
		}

		void OnSizeChanged (object sender, EventArgs e)
		{
			image.Source = ImageSource.FromFile (Height > Width ? "portrait.jpg" : "landscape.jpg");
		}
	}
}
