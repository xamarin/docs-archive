using System;
using System.Data;
using System.IO;

using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;

using Mono.Data.Sqlite;
using System.Runtime.InteropServices;


using System.Runtime.InteropServices;
using System.Threading.Tasks;



namespace CreateDatabaseWithAdoNet
{
    [Activity(Label = "CreateDatabaseWithAdoNet", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // assign variables to the used view widgets
            var btnCreate = FindViewById<Button>(Resource.Id.btnCreateDB);
            var txtResult = FindViewById<TextView>(Resource.Id.txtResults);

            // get the context of the button for use with Toast
            var context = btnCreate.Context;

            // create and test the database connection
            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var pathToDatabase = Path.Combine(docsFolder, "db_adonet.db");

            // create the event for the button
            btnCreate.Click += async delegate
            {
                try
                {
                    SqliteConnection.CreateFile(pathToDatabase);
                    txtResult.Text = string.Format("Database created successfully - filename = {0}\n", pathToDatabase);
                }
                catch (IOException ex)
                {
                    var reason = string.Format("Unable to create the database - reason = {0}", ex.Message);
                    Toast.MakeText(context, reason, ToastLength.Long).Show();
                }
                
                // create the schema, perform using an async task
                txtResult.Text += await createTable(pathToDatabase);
            };
        }

        private async Task<string> createTable(string path)
        {
            // create a connection string for the database
            var connectionString = string.Format("Data Source={0};Version=3;", path);
            try
            {
                using (var conn = new SqliteConnection((connectionString)))
                {
                    await conn.OpenAsync();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "CREATE TABLE People (PersonID INTEGER PRIMARY KEY AUTOINCREMENT, FirstName ntext, LastName ntext)";
                        command.CommandType = CommandType.Text;
                        await command.ExecuteNonQueryAsync();
                        return "Database table created successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                var reason = string.Format("Failed to insert into the database - reason = {0}", ex.Message);
                return reason;
            }
        }
    }
}


