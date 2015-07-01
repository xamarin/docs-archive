namespace FillAPolygon
{
    using Android.App;
    using Android.OS;
    using Android.Widget;

    [Activity(Label = "FillAPolygon", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private int count = 1;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(new FilledPolygon(this));
        }
    }
}
