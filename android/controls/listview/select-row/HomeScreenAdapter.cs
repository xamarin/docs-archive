using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace SelectRow
{
    public class HomeScreenAdapter : BaseAdapter<TableItem>
    {
        readonly Activity context;
        readonly List<TableItem> items;

        public HomeScreenAdapter(Activity context, List<TableItem> items)
        {
            this.context = context;
            this.items = items;
        }

        public override TableItem this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TableItem item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.row_layout, null);
            }
            view.FindViewById<TextView>(Resource.Id.Text1).Text = item.Heading;
            view.FindViewById<TextView>(Resource.Id.Text2).Text = item.SubHeading;
            view.FindViewById<ImageView>(Resource.Id.Image).SetImageResource(item.ImageResourceId);

            return view;
        }
    }
}