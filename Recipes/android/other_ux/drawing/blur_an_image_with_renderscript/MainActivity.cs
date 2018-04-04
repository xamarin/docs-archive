using System.Threading.Tasks;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Renderscripts;
using Android.Widget;

namespace Xamarin.Example.Api17.Blur
{
	/// <summary>
	///   Demonstration of using the ScriptIntrinsicBlur class that is first available in API 17.
	/// </summary>
	[Activity(Label = "@string/app_name", MainLauncher = true)]
	public class MainActivity : Activity
	{

		static readonly string PROGRESS_DIALOG_TAG = "progress_dialog";
		ImageView imageView;
		SeekBar seekBar;
		RenderScript renderScript;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Main);
			imageView = FindViewById<ImageView>(Resource.Id.originalImageView);

			seekBar = FindViewById<SeekBar>(Resource.Id.seekBar1);
			seekBar.StopTrackingTouch += BlurImageHandler;

			// Create the Renderscript context.
			renderScript = RenderScript.Create(this);
		}

		protected override void OnDestroy()
		{
			renderScript.Destroy();
			base.OnDestroy();
		}

		void BlurImageHandler(object sender, SeekBar.StopTrackingTouchEventArgs e)
		{
			int radius = e.SeekBar.Progress;
			if (radius == 0)
			{
				// We don't want to blur, so just load the un-altered image.
				imageView.SetImageResource(Resource.Drawable.dog_and_monkeys);
			}
			else {
				DisplayBlurredImage(radius);
			}
		}

		Bitmap CreateBlurredImage(int radius)
		{
			// Load a clean bitmap to work from.
			Bitmap originalBitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.dog_and_monkeys);

			// Create another bitmap that will hold the results of the filter.
			Bitmap blurredBitmap = Bitmap.CreateBitmap(originalBitmap);

			// Load up an instance of the specific script that we want to use.
			// An Element is similar to a C type. The second parameter, Element.U8_4,
			// tells the Allocation is made up of 4 fields of 8 unsigned bits.
			ScriptIntrinsicBlur script = ScriptIntrinsicBlur.Create(renderScript, Element.U8_4(renderScript));

			// Create an Allocation for the kernel inputs.
			Allocation input = Allocation.CreateFromBitmap(renderScript, originalBitmap,
														   Allocation.MipmapControl.MipmapFull,
														   AllocationUsage.Script);

			// Assign the input Allocation to the script.
			script.SetInput(input);

			// Set the blur radius
			script.SetRadius(radius);

			// Finally we need to create an output allocation to hold the output of the Renderscript.
			Allocation output = Allocation.CreateTyped(renderScript, input.Type);

			// Next, run the script. This will run the script over each Element in the Allocation, and copy it's
			// output to the allocation we just created for this purpose.
			script.ForEach(output);

			// Copy the output to the blurred bitmap
			output.CopyTo(blurredBitmap);

			// Cleanup.
			output.Destroy();
			input.Destroy();
			script.Destroy();

			return blurredBitmap;
		}

		void DismissIndeterminateProgressDialog()
		{
			Fragment frag = FragmentManager.FindFragmentByTag(PROGRESS_DIALOG_TAG);
			if (frag != null)
			{
				((DialogFragment)frag).Dismiss();
			}
		}

		/// <summary>
		/// Display a progress dialog inside of a DialogFragment.
		/// </summary>
		void ShowIndeterminateProgressDialog()
		{
			MyProgressDialog progressDialog = FragmentManager.FindFragmentByTag(PROGRESS_DIALOG_TAG) as MyProgressDialog;

			if (progressDialog == null)
			{
				var tx = FragmentManager.BeginTransaction();
				progressDialog = new MyProgressDialog();
				progressDialog.Show(tx, PROGRESS_DIALOG_TAG);
			}
		}

		void DisplayBlurredImage(int radius)
		{
			// Disable the event handler and the Seekbar to prevent this from 
			// happening multiple times.
			seekBar.StopTrackingTouch -= BlurImageHandler;
			seekBar.Enabled = false;

			ShowIndeterminateProgressDialog();

			Task.Run(() => {
				Bitmap bmp = CreateBlurredImage(radius);
				return bmp;
			}).ContinueWith(task => {
				// Processing is done - display the image and re-enable all of our
				// event handlers and widgets. This work is done on the UI thread.
				Bitmap bmp = task.Result;
				imageView.SetImageBitmap(bmp);
				seekBar.StopTrackingTouch += BlurImageHandler;
				seekBar.Enabled = true;
				DismissIndeterminateProgressDialog();
			}, TaskScheduler.FromCurrentSynchronizationContext());
		}
	}

	public class MyProgressDialog : DialogFragment
	{
		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{
			ProgressDialog dialog = new ProgressDialog(Activity);

			dialog.SetTitle(Resource.String.dialogtitle);
			dialog.SetMessage(GetString(Resource.String.dialogmessage));
			dialog.Indeterminate = true;
			dialog.SetCancelable(false);

			return dialog;
		}
	}
}
