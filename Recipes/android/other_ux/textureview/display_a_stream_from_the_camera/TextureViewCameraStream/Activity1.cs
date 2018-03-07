using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Hardware;

namespace TextureViewCameraStream
{
    [Activity (Label = "TextureViewCameraStream", MainLauncher = true)]
    public class Activity1 : Activity, TextureView.ISurfaceTextureListener
    {
        Camera _camera;
        TextureView _textureView;
        
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);
   
            _textureView = new TextureView (this);
            _textureView.SurfaceTextureListener = this;
            
            SetContentView (_textureView);
        }
                
        public void OnSurfaceTextureAvailable (Android.Graphics.SurfaceTexture surface, int w, int h)
        {
            _camera = Camera.Open ();
            
            _textureView.LayoutParameters = new FrameLayout.LayoutParams (w, h);
            
            try {
                _camera.SetPreviewTexture (surface);
                _camera.StartPreview ();
                
            } catch (Java.IO.IOException ex) {
                Console.WriteLine (ex.Message);
            }
        }

        public bool OnSurfaceTextureDestroyed (Android.Graphics.SurfaceTexture surface)
        {
            _camera.StopPreview ();
            _camera.Release ();
            
            return true;
        }

        public void OnSurfaceTextureSizeChanged (Android.Graphics.SurfaceTexture surface, int width, int height)
        {
            // camera takes care of this
        }

        public void OnSurfaceTextureUpdated (Android.Graphics.SurfaceTexture surface)
        {
            
        }
      
    }
}


