using System;
namespace com.xamarin.recipes.customcompoundview
{
    public class DatePickerTextViewChangedArgs : EventArgs
    {
        public DatePickerTextViewChangedArgs(DateTime date)
        {
            this.Date = date;
        }

        public DateTime Date { get; private set; }
    }
}
