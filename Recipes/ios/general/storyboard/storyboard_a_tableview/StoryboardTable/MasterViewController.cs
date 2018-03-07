using System;
using CoreGraphics;
using System.Collections.Generic;

using Foundation;
using UIKit;

namespace StoryboardTable
{
	public partial class MasterViewController : UITableViewController
	{
		List<Chore> chores;

		public MasterViewController (IntPtr handle) : base (handle)
		{
			Title = "ChoreBoard";

			// Custom initialization
			chores = new List<Chore> {
			new Chore() {Name="Groceries", Notes="Buy bread, cheese, apples", Done=false},
			new Chore() {Name="Devices", Notes="Buy Nexus, Galaxy, Droid", Done=false}
		};

		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "TaskSegue") { // set in Storyboard
				var navctlr = segue.DestinationViewController as TaskDetailViewController;
				if (navctlr != null) {
					var source = TableView.Source as RootTableSource;
					var rowPath = TableView.IndexPathForSelectedRow;
					var item = source.GetItem(rowPath.Row);
					navctlr.SetTask (this, item); // to be defined on the TaskDetailViewController
				}
			}
		}

		public void CreateTask () 
		{
			// first, add the task to the underlying data
			var newId = chores[chores.Count - 1].Id + 1;
			var newChore = new Chore(){Id=newId};
			chores.Add (newChore);

			// then open the detail view to edit it
			var detail = Storyboard.InstantiateViewController("detail") as TaskDetailViewController;
			detail.SetTask (this, newChore);
			NavigationController.PushViewController (detail, true);
		}

		public void SaveTask (Chore chore)
		{
			var oldTask = chores.Find(t => t.Id == chore.Id);
			oldTask = chore;
			NavigationController.PopViewController(true);
		}

		public void DeleteTask (Chore chore) 
		{
			var oldTask = chores.Find(t => t.Id == chore.Id);
			chores.Remove (oldTask);
			NavigationController.PopViewController(true);
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.
			AddButton.Clicked += (sender, e) => {
				CreateTask ();
			};
		}
			

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			// bind every time, to reflect deletion in the Detail view
			TableView.Source = new RootTableSource(chores.ToArray ());
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion


	}
}

