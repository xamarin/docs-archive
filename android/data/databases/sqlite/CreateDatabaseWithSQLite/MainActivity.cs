using System;
using System.Collections.Generic;

using Android.App;
using Android.Widget;
using Android.OS;

using SQLite;

namespace CreateDatabaseWithSQLite
{
    [Activity(Label = "CreateDatabaseWithSQLite", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // create variables for our onscreen widgets

            var btnCreate = FindViewById<Button>(Resource.Id.btnCreateDB);
            var btnSingle = FindViewById<Button>(Resource.Id.btnCreateSingle);
            var btnList = FindViewById<Button>(Resource.Id.btnList);
            var txtResult = FindViewById<TextView>(Resource.Id.txtResults);

            // create DB path
            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var pathToDatabase = System.IO.Path.Combine(docsFolder, "db_sqlnet.db");

            // disable the single and list buttons until the database has been created
            btnSingle.Enabled = btnList.Enabled = false;

            // create events for buttons
            btnCreate.Click += delegate
            {
                var result = createDatabase(pathToDatabase);
                txtResult.Text = result + "\n";
                // if the database was created ok, then enable the list and single buttons
                if (result == "Database created")
                    btnList.Enabled = btnSingle.Enabled = true;
            };

            btnSingle.Click += delegate
            {
                var result = insertUpdateData(new Person{ FirstName = string.Format("John {0}", DateTime.Now.Ticks), LastName = "Smith" }, pathToDatabase);
                var records = findNumberRecords(pathToDatabase);
                txtResult.Text += string.Format("{0}\nNumber of records = {1}\n", result, records);
            };

            btnList.Click += delegate
            {
                var peopleList = new List<Person>
                {
                    new Person { FirstName = "Miguel", LastName = string.Format("de Icaza ({0})", DateTime.Now.Ticks) },
                    new Person { FirstName = string.Format("Kevin {0}", DateTime.Now.Ticks), LastName = "Mullins" },
                    new Person { FirstName = "Amy", LastName = string.Format("Burns ({0})", DateTime.Now.Ticks) } 
                };
                var result = insertUpdateAllData(peopleList, pathToDatabase);
                var records = findNumberRecords(pathToDatabase);
                txtResult.Text += string.Format("{0}\nNumber of records = {1}\n", result, records);
            };
        }

        private string createDatabase(string path)
        {
            try
            {
                var connection = new SQLiteConnection(path);
                connection.CreateTable<Person>();
                return "Database created";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        private string insertUpdateData(Person data, string path)
        {
            try
            {
                var db = new SQLiteConnection(path);
                if (db.Insert(data) != 0)
                    db.Update(data);
                return "Single data file inserted or updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        private string insertUpdateAllData(IEnumerable<Person> data, string path)
        {
            try
            {
                var db = new SQLiteConnection(path);
                if (db.InsertAll(data) != 0)
                    db.UpdateAll(data);
                return "List of data inserted or updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        private int findNumberRecords(string path)
        {
            try
            {
                var db = new SQLiteConnection(path);
                // this counts all records in the database, it can be slow depending on the size of the database
                var count = db.ExecuteScalar<int>("SELECT Count(*) FROM Person");

                // for a non-parameterless query
                // var count = db.ExecuteScalar<int>("SELECT Count(*) FROM Person WHERE FirstName="Amy");

                return count;
            }
            catch (SQLiteException)
            {
                return -1;
            }
        }
    }
}


