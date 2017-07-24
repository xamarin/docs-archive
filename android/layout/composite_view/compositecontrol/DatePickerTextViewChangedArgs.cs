using System;
namespace com.xamarin.recipes.compositecontrol
{
    public class DatePickerChangedArgs : EventArgs
    {
        public DatePickerChangedArgs(DateTime date)
        {
            this.Date = date;
        }

        public DateTime Date { get; private set; }
    }
}
