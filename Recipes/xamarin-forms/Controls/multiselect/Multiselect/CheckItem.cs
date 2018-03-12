using System;

namespace Multiselect
{
	/// <summary>
	/// Example 'business model' class for items to appear 
	/// in the multi-select list. 
	/// By default it expects a Name property.
	/// </summary>
	public class CheckItem
	{
		public CheckItem ()
		{
		}

		public string Name {get;set;}
	}
}

