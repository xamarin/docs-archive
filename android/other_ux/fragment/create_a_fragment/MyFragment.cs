using System.Text;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace CreateAFragment
{
    public class MyFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            // A large amount of text to display.
            var textToDisplay = new StringBuilder().Insert(0, "The quick brown fox jumps over the lazy dog. ", 450).ToString();

            var view = inflater.Inflate(Resource.Layout.MyFragment, container, false);

            var textView = view.FindViewById<TextView>(Resource.Id.text_view);
            textView.Text = textToDisplay;

            return view;
        }
    }
}