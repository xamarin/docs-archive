using System;
using Xamarin.Forms;

namespace ScreenOrientation
{
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
			SizeChanged += OnSizeChanged;
		}

		void OnSizeChanged (object sender, EventArgs e)
		{
			image.Source = ImageSource.FromFile (Height > Width ? "portrait.jpg" : "landscape.jpg");
		}
	}
}
