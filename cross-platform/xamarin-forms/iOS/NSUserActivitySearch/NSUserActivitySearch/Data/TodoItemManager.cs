using System.Collections.Generic;
using System.Linq;

namespace NSUserActivitySearch
{
	public class TodoItemManager
	{
		List<TodoItem> todoItems;

		public TodoItemManager ()
		{
			InitializeData ();
		}

		public List<TodoItem> All {
			get { return todoItems; }
		}

		public bool DoesItemExist (string id)
		{
			return todoItems.Any (item => item.ID == id);
		}

		public TodoItem Find (string id)
		{
			return todoItems.FirstOrDefault (item => item.ID == id);
		}

		public void Insert (TodoItem item)
		{
			todoItems.Add (item);
		}

		public void Update (TodoItem item)
		{
			var todoItem = Find (item.ID);
			var index = todoItems.IndexOf (todoItem);
			todoItems.RemoveAt (index);
			todoItems.Insert (index, item);
		}

		public void Delete (string id)
		{
			todoItems.Remove (Find (id));
		}

		void InitializeData ()
		{
			todoItems = new List<TodoItem> ();

			var todoItem1 = new TodoItem {
				ID = "6bb8a868-dba1-4f1a-93b7-24ebce87e243",
				Name = "Learn app development",
				Notes = "Attend Xamarin University",
				Done = true
			};

			var todoItem2 = new TodoItem {
				ID = "b94afb54-a1cb-4313-8af3-b7511551b33b",
				Name = "Develop apps",
				Notes = "Use Xamarin Studio/Visual Studio",
				Done = false
			};

			var todoItem3 = new TodoItem {
				ID = "ecfa6f80-3671-4911-aabe-63cc442c1ecf",
				Name = "Publish apps",
				Notes = "All app stores",
				Done = false,
			};

			todoItems.Add (todoItem1);
			todoItems.Add (todoItem2);
			todoItems.Add (todoItem3);
		}
	}
}
