using Xamarin.Forms;

namespace CustomizeTabs
{
	public partial class TabbedPageDemoPage : TabbedPage
	{
		public TabbedPageDemoPage ()
		{
			InitializeComponent ();
			ItemsSource = MonkeyDataModel.All;
		}
	}
}
