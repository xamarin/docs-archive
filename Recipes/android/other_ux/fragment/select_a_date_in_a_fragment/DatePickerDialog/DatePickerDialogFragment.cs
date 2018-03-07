using System;
using Android.App;
using Android.Content;
using Android.OS;

namespace DatePickerDialog
{


    public class DatePickerDialogFragment : DialogFragment
    {
        private readonly Context _context;
        private  DateTime _date;
        private readonly Android.App.DatePickerDialog.IOnDateSetListener _listener;

        public DatePickerDialogFragment(Context context, DateTime date, Android.App.DatePickerDialog.IOnDateSetListener listener  )
        {
            _context = context;
            _date = date;
            _listener = listener;
        }

        public override Dialog OnCreateDialog(Bundle savedState)
        {
            var dialog = new Android.App.DatePickerDialog(_context, _listener, _date.Year, _date.Month - 1, _date.Day);
            return dialog;
        }
    }
}