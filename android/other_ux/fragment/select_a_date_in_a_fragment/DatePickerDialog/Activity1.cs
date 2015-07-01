using Android.App;
using Android.OS;

namespace DatePickerDialog
{
    [Activity(Label = "DatePickerDialog", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var fragTx = FragmentManager.BeginTransaction();
            var frag = new FragmentThatWantsToDisplayADatePickerDialog();
            fragTx.Add(Resource.Id.fragment_container, frag);
            fragTx.Commit();
        }
    }
}