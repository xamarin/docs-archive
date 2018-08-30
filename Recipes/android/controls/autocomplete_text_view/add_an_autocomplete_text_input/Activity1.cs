using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.IO;
using System.Collections.Generic;
using Android.Support.V7.App;

namespace AutoCompleteTextViewExample {
    [Activity(Label = "AutoCompleteTextView", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : AppCompatActivity {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView(Resource.Layout.Main);

            var wordList = GetFewAutoCompleteOptions();

            ArrayAdapter autoCompleteAdapter = new ArrayAdapter(this, 
                Android.Resource.Layout.SimpleDropDownItem1Line, 
                wordList);

            var autocompleteTextView = FindViewById<AutoCompleteTextView>(Resource.Id.AutoCompleteInput);
            autocompleteTextView.Adapter = autoCompleteAdapter;

        }

        /// <summary>
        /// This returns a small list of words for the Autocomplete TextView
        /// </summary>
        /// <returns>The few auto complete options.</returns>
        String[] GetFewAutoCompleteOptions() 
        {
            return new String[] { "Hello", "Hey", "Hej", "Hi", "Hola", "Bonjour", "Gday", "Goodbye", "Sayonara", "Farewell", "Adios" };
        }


        /// <summary>
        /// Retrieves a list of words from a text file.
        /// </summary>
        /// <returns>The many auto complete options.</returns>
        String[] GetManyAutoCompleteOptions()
        {
            Stream seedDataStream = Assets.Open(@"WordList.txt");
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(seedDataStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            string[] wordlist = lines.ToArray();
            return wordlist;
        }
    }
}


