using System;
using System.IO;
using CoreGraphics;
using System.Data;
using System.Text;
using Mono.Data.Sqlite;
using Foundation;
using UIKit;

namespace CreateDatabaseWithAdoNet
{
    public partial class CreateDatabaseWithAdoNetViewController : UIViewController
    {
        UIButton _btnCreateDatabase;
        UITextView _txtView;
        
        public CreateDatabaseWithAdoNetViewController() : base ("CreateDatabaseWithAdoNetViewController", null)
        {
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Create the buttons and TextView to run the sample code
            _btnCreateDatabase = UIButton.FromType(UIButtonType.RoundedRect);
            _btnCreateDatabase.Frame = new CGRect(10, 10, 145, 50);
            _btnCreateDatabase.SetTitle("Create Database", UIControlState.Normal);

            _txtView = new UITextView(new CGRect(10, 90, 300, 350));
            _txtView.Editable = false;
            _txtView.ScrollEnabled = true;
            
            _btnCreateDatabase.TouchUpInside += HandleTouchUpInside;

            Add(_btnCreateDatabase);
            Add(_txtView);
        }

        /// <summary>
        /// This code will handle creating the database and a table.
        /// </summary>
        void HandleTouchUpInside(object sender, EventArgs e)
        {
            // Create the database
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var pathToDatabase = Path.Combine(documents, "db_adonet.db");
            SqliteConnection.CreateFile(pathToDatabase);

            var msg = "Created new database at " + pathToDatabase;
            
            // Create a table
            var connectionString = String.Format("Data Source={0};Version=3;", pathToDatabase);
            using (var conn= new SqliteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE People (PersonID INTEGE" +
                    	"R PRIMARY KEY AUTOINCREMENT , FirstName ntext, LastName ntext)";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
                
            // Let the user know that the database was created, and disable the button
            // to prevent double clicks.
            _txtView.Text = msg;
            _btnCreateDatabase.Enabled = false;         
        }
        
        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
            
            ReleaseDesignerOutlets();
        }
        
        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            // Return true for supported orientations
            return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
        }
    }
}

