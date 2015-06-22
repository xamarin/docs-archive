using System;
using System.Linq;
using System.Collections.Generic;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using MonoTouch.CoreFoundation;

namespace SplitMultiDetailDemo
{
	public class ColorsController : DialogViewController
	{
		public event EventHandler<ColorSelectedEventArgs> ColorSelected;

		List<string> colors = new List<string>{"Red", "Green"};

		public List<string> Colors {
			get {
				return colors;
			}
		}

		public ColorsController () : base (null)
		{
			Root = new RootElement ("Colors") {
                new Section () {
                    from color in colors
                        select (Element) new StringElement(color, () => {   
                            if(ColorSelected != null)
                                ColorSelected(this, new ColorSelectedEventArgs{Color = color});
                        })
                }   
            };
		}
	}

	public class ColorSelectedEventArgs : EventArgs
	{
		public string Color { get; set; }
	}
}