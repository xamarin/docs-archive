using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace com.xamarin.sample.datepicker
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        TextView _dateDisplay;
        Button _dateSelectButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            _dateDisplay = FindViewById<TextView>(Resource.Id.date_display);
            _dateSelectButton = FindViewById<Button>(Resource.Id.date_select_button);
            _dateSelectButton.Click += DateSelect_OnClick;
        }

        void DateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate(DateTime time)
                                                                     {
                                                                         _dateDisplay.Text = time.ToLongDateString();
                                                                     });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
    }
}