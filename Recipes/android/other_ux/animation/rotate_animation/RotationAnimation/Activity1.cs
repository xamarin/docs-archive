using Android.App;
using Android.OS;
using Android.Views.Animations;
using Android.Widget;

namespace RotationAnimation
{
    [Activity(Label = "RotationAnimation", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var image = FindViewById<ImageView>(Resource.Id.floating_image);

            // Set things up so that when this button is clicked the image roates about a corner.
            var rotateAboutCornerButton = FindViewById<Button>(Resource.Id.rotate_corner_button);
            var rotateAboutCornerAnimation = AnimationUtils.LoadAnimation(this, Resource.Animation.rotate_corner);
            rotateAboutCornerButton.Click += (sender, args) => image.StartAnimation(rotateAboutCornerAnimation);

            // Setup things so that when this button is clicked the image rotates about it's center.
            var rotateAboutCenterButton = FindViewById<Button>(Resource.Id.rotate_center_button);
            var rotateAboutCenterAnimation = AnimationUtils.LoadAnimation(this, Resource.Animation.rotate_center);
            rotateAboutCenterButton.Click += (sender, args) => image.StartAnimation(rotateAboutCenterAnimation);

        }
    }
}