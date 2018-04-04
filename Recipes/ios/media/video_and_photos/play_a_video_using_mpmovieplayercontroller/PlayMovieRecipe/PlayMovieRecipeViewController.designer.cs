// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;

namespace PlayMovieRecipe
{
	[Register ("PlayMovieRecipeViewController")]
	partial class PlayMovieRecipeViewController
	{
		[Outlet]
		UIKit.UIButton playMovie { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (playMovie != null) {
				playMovie.Dispose ();
				playMovie = null;
			}
		}
	}
}
