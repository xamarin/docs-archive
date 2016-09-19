using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Widget;
using SQLite;

namespace AndroidWithComponent
{
    [Activity(Label = "AndroidWithComponent", MainLauncher = true, Icon = "@drawable/icon")]
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
            var pathToDatabase = System.IO.Path.Combine(docsFolder, "db_sqlcompnet.db");

            // disable the single and list buttons until the database has been created
            btnSingle.Enabled = btnList.Enabled = false;

            // create events for buttons
            btnCreate.Click += async delegate
            {
                var result = await createDatabase(pathToDatabase);
                txtResult.Text = result + "\n";
                // if the database was created ok, then enable the list and single buttons
                if (result == "Database created")
                    btnList.Enabled = btnSingle.Enabled = true;
            };

            btnSingle.Click += async delegate
            {
                var result = await insertUpdateData(new Person{ FirstName = string.Format("John {0}", DateTime.Now.Ticks), LastName = "Smith" }, pathToDatabase);
                var records = await findNumberRecords(pathToDatabase);
                txtResult.Text += string.Format("{0}\nNumber of records = {1}\n", result, records);
            };

            btnList.Click += async delegate
            {
                var peopleList = new List<Person>
                {
                    new Person { FirstName = "Miguel", LastName = string.Format("de Icaza ({0})", DateTime.Now.Ticks) },
                    new Person { FirstName = string.Format("Kevin {0}", DateTime.Now.Ticks), LastName = "Mullins" },
                    new Person { FirstName = "Amy", LastName = string.Format("Burns ({0})", DateTime.Now.Ticks) } 
                };
                var result = await insertUpdateAllData(peopleList, pathToDatabase);
                var records = await findNumberRecords(pathToDatabase);
                txtResult.Text += string.Format("{0}\nNumber of records = {1}\n", result, records);
            };

        }

        private async Task<string> createDatabase(string path)
        {
            try
            {
                var connection = new SQLiteAsyncConnection(path);
                await connection.CreateTableAsync<Person>();
                return "Database created";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        private async Task<string> insertUpdateData(Person data, string path)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);
                if (await db.InsertAsync(data) != 0)
                    await db.UpdateAsync(data);
                return "Single data file inserted or updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        private async Task<string> insertUpdateAllData(IEnumerable<Person> data, string path)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);
                if (await db.InsertAllAsync(data) != 0)
                    await db.UpdateAllAsync(data);
                return "List of data inserted or updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        private async Task<int> findNumberRecords(string path)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);
                // this counts all records in the database, it can be slow depending on the size of the database
                var count = await db.ExecuteScalarAsync<int>("SELECT Count(*) FROM Person");

                // for a non-parameterless query
                // var count = db.ExecuteScalarAsync<int>("SELECT Count(*) FROM Person WHERE FirstName="Amy");

                return count;
            }
            catch (SQLiteException ex)
            {
                return -1;
            }
        }
    }
}


