using System;

using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Provider;
using System.IO;

namespace ReadGalleryImage
{
    [Activity (Label = "ReadGalleryImage", MainLauncher = true)]
    public class Activity1 : Activity
    {        
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.Main);

            var button = FindViewById<Button> (Resource.Id.myButton);
            
            button.Click += delegate {
                
                var imageIntent = new Intent ();
                imageIntent.SetType ("image/*");
                imageIntent.SetAction (Intent.ActionGetContent);
                StartActivityForResult (Intent.CreateChooser (imageIntent, "Select photo"), 0);
            };
        }
        
        protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult (requestCode, resultCode, data);
            
            if (resultCode == Result.Ok) {
                
                var imageView = FindViewById<ImageView> (Resource.Id.myImageView);
                imageView.SetImageURI (data.Data);
            }
        }

    }
}