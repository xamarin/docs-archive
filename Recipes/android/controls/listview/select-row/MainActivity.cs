using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace SelectRow
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static readonly string TAG = "SelectARow";
		static readonly int INITIAL_SELECTION = 4;

        readonly List<TableItem> tableItems = new List<TableItem>();
        ArrayAdapter<string> listAdapter;
        ListView listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            LoadTableItems();
            InitializeListView();

			listView.SetItemChecked(INITIAL_SELECTION, true);
			Log.Debug(TAG, "Selecting listview item #" + INITIAL_SELECTION);
        }
		void InitializeListView()
		{
			// Initialize the ListView with the data first.
			listView = FindViewById<ListView>(Resource.Id.listView1);
			listView.Adapter = new HomeScreenAdapter(this, tableItems);

			// Important - Set the ChoiceMode
			listView.ChoiceMode = ChoiceMode.Single;
		}

        void LoadTableItems()
        {
            tableItems.Add(new TableItem
                           {
                               Heading = "Vegetables",
                               SubHeading = "65 items",
                               ImageResourceId = Resource.Drawable.Vegetables
                           });
            tableItems.Add(new TableItem
                           {
                               Heading = "Fruits",
                               SubHeading = "17 items",
                               ImageResourceId = Resource.Drawable.Fruits
                           });
            tableItems.Add(new TableItem
                           {
                               Heading = "Flower Buds",
                               SubHeading = "5 items",
                               ImageResourceId = Resource.Drawable.FlowerBuds
                           });
            tableItems.Add(new TableItem
                           {
                               Heading = "Legumes",
                               SubHeading = "33 items",
                               ImageResourceId = Resource.Drawable.Legumes
                           });
            tableItems.Add(new TableItem
                           {
                               Heading = "Bulbs",
                               SubHeading = "18 items",
                               ImageResourceId = Resource.Drawable.Bulbs
                           });
            tableItems.Add(new TableItem
                           {
                               Heading = "Tubers",
                               SubHeading = "43 items",
                               ImageResourceId = Resource.Drawable.Tubers
                           });
        }

    }
}