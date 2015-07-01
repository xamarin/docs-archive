namespace PickImageFromGallery
{
    using System;

    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Widget;

    using Uri = Android.Net.Uri;

    [Activity(Label = "PickImageFromGallery", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        public static readonly int PickImageId = 1000;
        private ImageView _imageView;

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                Uri uri = data.Data;
                _imageView.SetImageURI(uri);
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += ButtonOnClick;
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        }
    }
}
