// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace HelloMusic
{
	[Register ("HelloMusicViewController")]
	partial class HelloMusicViewController
	{
		[Outlet]
		UIKit.UILabel Album { get; set; }

		[Outlet]
		UIKit.UILabel Artist { get; set; }

		[Outlet]
		UIKit.UIImageView Artwork { get; set; }

		[Outlet]
		UIKit.UILabel Lyrics { get; set; }

		[Outlet]
		UIKit.UILabel Track { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Album != null) {
				Album.Dispose ();
				Album = null;
			}

			if (Artist != null) {
				Artist.Dispose ();
				Artist = null;
			}

			if (Lyrics != null) {
				Lyrics.Dispose ();
				Lyrics = null;
			}

			if (Track != null) {
				Track.Dispose ();
				Track = null;
			}

			if (Artwork != null) {
				Artwork.Dispose ();
				Artwork = null;
			}
		}
	}
}
