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
        static readonly int FIRST_ITEM_TO_SELECT = 4;

        readonly List<TableItem> tableItems = new List<TableItem>();
        ArrayAdapter<string> listAdapter;
        ListView listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            LoadTableItems();
            InitializeListView();

            SelectInitialItem();
        }

        void SelectInitialItem()
        {
            listView.SetItemChecked(FIRST_ITEM_TO_SELECT, true);
            Log.Debug(TAG, "Selecting listview item #" + FIRST_ITEM_TO_SELECT);
        }

        protected override void OnResume()
        {
            base.OnResume();
//            listView.Post(() =>
//                          {
//                              listView.SetItemChecked(FIRST_ITEM_TO_SELECT, true);
//                              Log.Debug(TAG, "Selecting listview item #" + FIRST_ITEM_TO_SELECT);
//                          });
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

        void InitializeListView()
        {
            listView = FindViewById<ListView>(Resource.Id.listView1);
            listView.ChoiceMode = ChoiceMode.Single;
            listView.Adapter = new HomeScreenAdapter(this, tableItems);
            listView.ItemClick += ListViewOnItemClick;
        }

        void ListViewOnItemClick(object sender, AdapterView.ItemClickEventArgs itemClickEventArgs)
        {
            itemClickEventArgs.View.Selected = true;
            Log.Debug(TAG, "item clicked " + itemClickEventArgs.Position);
        }
    }
}