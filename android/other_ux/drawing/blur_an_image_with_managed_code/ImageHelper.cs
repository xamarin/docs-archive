using System;
using System.IO;
using System.Threading.Tasks;

using Android.Content;
using Android.Graphics;

namespace Xample.BlurImage
{
    public class ImageHelper
    {
        readonly Context _context;
        readonly IBlurImage _imageBlur = new StackBlur();
        Bitmap _blurredBitmap;
        Bitmap _originalBitmap;

        public ImageHelper(Context context)
        {
            _context = context;
        }

        public async Task<Bitmap> GetBlurryResizedImageAsync(string path, int width, int height, int blurRadius)
        {
            if (_blurredBitmap != null)
            {
                return _blurredBitmap;
            }

            if (_originalBitmap == null)
            {
                _originalBitmap = await GetResizedBitmapAsync(path, width, height);
            }
            _blurredBitmap = await Task.Run(() => _imageBlur.GetBlurredBitmap(_originalBitmap, blurRadius));
            return _blurredBitmap;
        }

        public async Task<Bitmap> GetResizedBitmapAsync(string path, int width, int height)
        {
            if (_originalBitmap != null)
            {
                return _originalBitmap;
            }

            #region Get some some information about the bitmap so we can resize it
            BitmapFactory.Options options = new BitmapFactory.Options
                                            {
                                                InJustDecodeBounds = true
                                            };

            using (Stream stream = _context.Assets.Open(path))
            {
                await BitmapFactory.DecodeStreamAsync(stream);
            }
            await BitmapFactory.DecodeFileAsync(path, options);
            #endregion

            // Calculate the factor by which we should reduce the image by
            options.InSampleSize = CalculateInSampleSize(options, width, height);

            #region Go and load the image and resize it at the same time.
            options.InJustDecodeBounds = false;

            using (Stream inputSteam = _context.Assets.Open(path))
            {
                _originalBitmap = await BitmapFactory.DecodeStreamAsync(inputSteam);
            }
            #endregion

            return _originalBitmap;
        }

        /// <summary>
        ///   This method will calculate a scaling factor that must be applied to get the image down
        ///   to the dimensions specified.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="reqWidth"></param>
        /// <param name="reqHeight"></param>
        /// <returns>A scaling factor to reduce the size of the image.</returns>
        static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            float height = options.OutHeight;
            float width = options.OutWidth;
            double inSampleSize = 1d;

            if (height > reqHeight || width > reqWidth)
            {
                if (width > height)
                {
                    inSampleSize = Math.Round(height / reqHeight);
                }
                else
                {
                    inSampleSize = Math.Round(width / reqWidth);
                }
            }

            return (int)inSampleSize;
        }
    }
}
