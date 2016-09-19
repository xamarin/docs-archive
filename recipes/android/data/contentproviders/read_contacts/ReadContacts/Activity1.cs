using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Provider;
using System.Collections.Generic;

namespace ReadContacts
{
    [Activity (Label = "ReadContacts", MainLauncher = true)]
    public class Activity1 : ListActivity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            var uri = ContactsContract.Contacts.ContentUri;
            
            string[] projection = { 
                ContactsContract.Contacts.InterfaceConsts.Id, ContactsContract.Contacts.InterfaceConsts.DisplayName };
            
            var cursor = ManagedQuery (uri, projection, null, null, null);
            
            var contactList = new List<string> ();    
            
            if (cursor.MoveToFirst ()) {
                do {
                    contactList.Add (cursor.GetString (cursor.GetColumnIndex (projection [1])));
                } while (cursor.MoveToNext());
            }
            
            ListAdapter = new ArrayAdapter<string> (this, Resource.Layout.ContactItemView, contactList);
        }
    }
}


