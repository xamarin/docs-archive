namespace Xamarin.Example.Api17.Blur
{
	using System.Threading.Tasks;
	using Android.App;
	using Android.Graphics;
	using Android.OS;
	using Android.Renderscripts;
	using Android.Widget;

	/// <summary>
	///   Demonstration of using the ScriptIntrinsicBlur class that is first available in API 17.
	/// </summary>
	[Activity(Label = "@string/app_name", MainLauncher = true)]
	public class MainActivity : Activity
	{
		private ImageView _imageView;
		private SeekBar _seekbar;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);
			_imageView = FindViewById<ImageView> (Resource.Id.originalImageView);

			_seekbar = FindViewById<SeekBar> (Resource.Id.seekBar1);
			_seekbar.StopTrackingTouch += BlurImageHandler;
		}

		private void BlurImageHandler (object sender, SeekBar.StopTrackingTouchEventArgs e)
		{
			int radius = e.SeekBar.Progress;
			if (radius == 0) {
				// We don't want to blur, so just load the un-altered image.
				_imageView.SetImageResource (Resource.Drawable.dog_and_monkeys);
			} else {
				DisplayBlurredImage (radius);
			}

		}

		private Bitmap CreateBlurredImage (int radius)
		{
			// Load a clean bitmap and work from that.
			Bitmap originalBitmap = BitmapFactory.DecodeResource (Resources, Resource.Drawable.dog_and_monkeys);

			// Create another bitmap that will hold the results of the filter.
			Bitmap blurredBitmap;
			blurredBitmap = Bitmap.CreateBitmap (originalBitmap);

			// Create the Renderscript instance that will do the work.
			RenderScript rs = RenderScript.Create (this);

			// Allocate memory for Renderscript to work with
			Allocation input = Allocation.CreateFromBitmap (rs, originalBitmap, Allocation.MipmapControl.MipmapFull, AllocationUsage.Script);
			Allocation output = Allocation.CreateTyped (rs, input.Type);

			// Load up an instance of the specific script that we want to use.
			ScriptIntrinsicBlur script = ScriptIntrinsicBlur.Create (rs, Element.U8_4 (rs));
			script.SetInput (input);

			// Set the blur radius
			script.SetRadius (radius);

			// Start Renderscript working.
			script.ForEach (output);

			// Copy the output to the blurred bitmap
			output.CopyTo (blurredBitmap);

			return blurredBitmap;
		}

		private void DismissIndeterminateProgressDialog ()
		{
			Fragment frag = FragmentManager.FindFragmentByTag ("progress_dialog");
			if (frag != null) {
				((DialogFragment)frag).Dismiss ();
			}
		}

		/// <summary>
		/// Display a progress dialog inside of a DialogFragment.
		/// </summary>
		private void ShowIndeterminateProgressDialog ()
		{
			MyProgressDialog progressDialog = FragmentManager.FindFragmentByTag ("progress_dialog") as MyProgressDialog;

			if (progressDialog == null) {
				var tx = FragmentManager.BeginTransaction ();
				progressDialog = new MyProgressDialog ();
				progressDialog.Show (tx, "progress_dialog");
			}
		}

		private void DisplayBlurredImage (int radius)
		{
			// Disable the event handler and the Seekbar to prevent this from 
			// happening multiple times.
			_seekbar.StopTrackingTouch -= BlurImageHandler;
			_seekbar.Enabled = false;

			ShowIndeterminateProgressDialog ();
		
			// Do the processing in a background thread.
			Task.Factory.StartNew (() => {
				Bitmap bmp = CreateBlurredImage (radius);
				return bmp;
			})
            .ContinueWith (task => {
				// Processing is done - display the image and re-enable all of our
				// event handlers and widgets. This work is done on the UI thread.
				Bitmap bmp = task.Result;
				_imageView.SetImageBitmap (bmp);
				_seekbar.StopTrackingTouch += BlurImageHandler;
				_seekbar.Enabled = true;
				DismissIndeterminateProgressDialog ();
			}, TaskScheduler.FromCurrentSynchronizationContext ());
		}
	}

	public class MyProgressDialog: DialogFragment
	{
		public override Dialog OnCreateDialog (Bundle savedInstanceState)
		{
			ProgressDialog dialog = new ProgressDialog (Activity);

			dialog.SetTitle (Resource.String.dialogtitle);
			dialog.SetMessage (GetString (Resource.String.dialogmessage));
			dialog.Indeterminate = true;
			dialog.SetCancelable (false);

			return dialog;
		}
	}
}
