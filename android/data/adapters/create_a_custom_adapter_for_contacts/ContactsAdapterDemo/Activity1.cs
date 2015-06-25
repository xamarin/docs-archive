using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ContactsAdapterDemo
{
    [Activity (Label = "ContactsAdapterDemo", MainLauncher = true)]
    public class Activity1 : Activity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.Main);

            var contactsAdapter = new ContactsAdapter (this);       
            var contactsListView = FindViewById<ListView> (Resource.Id.ContactsListView);      
            contactsListView.Adapter = contactsAdapter;
        }
    }
}


