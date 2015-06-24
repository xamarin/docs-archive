using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SelectDate
{
    [Activity (Label = "SelectDate", MainLauncher = true)]
    public class Activity1 : Activity
    {
        DateTime _date;
        
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.Main);

            var button = FindViewById<Button> (Resource.Id.myButton);
            
            button.Click += delegate {
                ShowDialog (0);
            };
            _date = DateTime.Today;
            button.Text = _date.ToString ("d");  
        }
        
        protected override Dialog OnCreateDialog (int id)
        {
            return new DatePickerDialog (this, HandleDateSet, _date.Year, _date.Month - 1, _date.Day); 
        }
        
        void HandleDateSet (object sender, DatePickerDialog.DateSetEventArgs e)
        {
            _date = e.Date;
            var button = FindViewById<Button> (Resource.Id.myButton);
            button.Text = _date.ToString ("d");
        }
    }
}