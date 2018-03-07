using Android.App;
using Android.OS;
using Android.Widget;

namespace Plurals
{
    [Activity(Label = "Plurals", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private int count = 0;

protected override void OnCreate(Bundle bundle)
{
    base.OnCreate(bundle);
    SetContentView(Resource.Layout.Main);

    var button = FindViewById<Button>(Resource.Id.MyButton);
    button.Click += delegate
                        {
                            count++;
                            var buttonText = Resources.GetQuantityString(Resource.Plurals.numberOfSongsAvailable, count, count);
                            button.Text = buttonText;
                        };
}
    }
}