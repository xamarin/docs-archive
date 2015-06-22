
using System;

using Foundation;
using UIKit;

namespace TabBarEdit2
{
	public class TabControllerSource : UITableViewSource
	{
		public TabControllerSource ()
		{
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			// TODO: return the actual number of sections
			return 1;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			// TODO: return the actual number of items in the section
			return 1;
		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
			return "Header";
		}

		public override string TitleForFooter (UITableView tableView, nint section)
		{
			return "Footer";
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (TabControllerCell.Key) as TabControllerCell;
			if (cell == null)
				cell = new TabControllerCell ();
			
			// TODO: populate the cell with the appropriate data based on the indexPath
			cell.DetailTextLabel.Text = "DetailsTextLabel";
			
			return cell;
		}
	}
}

