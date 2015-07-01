using Android.App;
using Android.OS;

namespace DrawingRecipe
{
    [Activity(Label = "Drawing Recipe", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
protected override void OnCreate(Bundle bundle)
{
    base.OnCreate(bundle);
    SetContentView(new MyOvalShape(this));
}
    }
}