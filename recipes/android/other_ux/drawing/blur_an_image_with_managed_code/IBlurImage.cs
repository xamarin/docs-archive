using Android.Graphics;

namespace Xample.BlurImage
{
    interface IBlurImage
    {
        Bitmap GetBlurredBitmap(Bitmap original, int radius);
    }
}
