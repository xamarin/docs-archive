using Android.App;
using Android.OS;
using Android.Util;

namespace com.xamarin.recipes.customcompoundview
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        DatePickerTextView datePicker;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            datePicker = FindViewById<DatePickerTextView>(Resource.Id.date_picker_view);
            datePicker.DateChanged += DatePicker_DateChanged;

        }

        void DatePicker_DateChanged(object sender, DatePickerTextViewChangedArgs e)
        {
            Log.Debug("MainActivity", "The date changed to " + e.Date);
        }
    }
}