using System;

using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Xample.BlurImage
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static readonly string TAG = "BlurImage";
        static readonly string PictureName = "picture.jpg";
        static readonly int BlurRadius = 30;
        Button _blurImageButton;
        ImageHelper _imageHelper;
        ImageView _imageView;
        TextView _pleaseWaitTextView;
        Button _unblurredImageButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _imageHelper = new ImageHelper(this);
            SetContentView(Resource.Layout.Main);

            _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
            _imageView.SetImageBitmap(null);

            _pleaseWaitTextView = FindViewById<TextView>(Resource.Id.blurring_image_textview);
            _blurImageButton = FindViewById<Button>(Resource.Id.blurred_image_button);
            _blurImageButton.Click += async delegate{
                                                try
                                                {
                                                    using (Timer timer = new Timer("Blurring the image"))
                                                    {
                                                        _pleaseWaitTextView.Visibility = ViewStates.Visible;
                                                        Bitmap bm = await _imageHelper.GetBlurryResizedImageAsync(PictureName, _imageView.Width, _imageView.Height, BlurRadius);
                                                        _imageView.SetImageBitmap(bm);
                                                    }
                                                }
                                                catch (AggregateException aex)
                                                {
                                                    Toast.MakeText(this, "There was an error blurring the bitmap.", ToastLength.Short).Show();
                                                    _imageView.SetImageBitmap(null);
                                                    Log.Error(TAG, aex.InnerException.ToString());
                                                }
                                                finally
                                                {
                                                    _pleaseWaitTextView.Visibility = ViewStates.Gone;
                                                }
                                            };

            _unblurredImageButton = FindViewById<Button>(Resource.Id.unblurred_image_button);
            _unblurredImageButton.Click += async delegate{
                                                     Bitmap bm = await _imageHelper.GetResizedBitmapAsync(PictureName, _imageView.Width, _imageView.Height);
                                                     _imageView.SetImageBitmap(bm);
                                                 };
        }
    }
}
