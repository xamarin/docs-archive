using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.IO;
using System.Collections.Generic;

namespace AutoCompleteTextViewExample {
    [Activity(Label = "AutoCompleteTextView", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView(Resource.Layout.Main);

            var autoCompleteOptions = new String[] { "Hello", "Hey", "Hej", "Hi", "Hola", "Bonjour", "Gday", "Goodbye", "Sayonara", "Farewell", "Adios" };
            ArrayAdapter autoCompleteAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line
                , autoCompleteOptions);

            var autocompleteTextView = FindViewById<AutoCompleteTextView>(Resource.Id.AutoCompleteInput);
            autocompleteTextView.Adapter = autoCompleteAdapter;

            #region Additional Information - use a file to populate autocomplete array
            /* 
            // instead of the small array of greetings, use a large dictionary of words loaded from a file
            Stream seedDataStream = Assets.Open(@"WordList.txt");
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(seedDataStream)) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    lines.Add(line);
                }
            }
            string[] wordlist = lines.ToArray();
            ArrayAdapter dictionaryAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, wordlist);
            autocompleteTextView.Adapter = dictionaryAdapter;
            */
            #endregion
        }
    }
}


