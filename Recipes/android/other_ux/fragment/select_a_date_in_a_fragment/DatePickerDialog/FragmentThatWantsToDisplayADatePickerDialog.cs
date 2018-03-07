using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace DatePickerDialog
{
    internal class FragmentThatWantsToDisplayADatePickerDialog : Fragment, Android.App.DatePickerDialog.IOnDateSetListener
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_layout, container, false);
            view.FindViewById<Button>(Resource.Id.pick_date_button).Click += (sender, args) =>
                                                                                 {
                                                                                     var dialog = new DatePickerDialogFragment(Activity, DateTime.Now, this);
                                                                                     dialog.Show(FragmentManager, null);                                                                                     
                                                                                 };
            return view;
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            var date = new DateTime(year, monthOfYear + 1, dayOfMonth);
            View.FindViewById<TextView>(Resource.Id.display_date_text).Text = "You picked " + date.ToString("yyyy-MMM-dd");
        }
    }
}