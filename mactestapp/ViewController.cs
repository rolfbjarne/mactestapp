using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

using AppKit;
using Foundation;

namespace mactestapp {
	public partial class ViewController : NSViewController {
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//var text = new NSTextView(View.Bounds);
			//text.Value = "Hello world";
			//View.AddSubview(text);

			var view = new ClickView(View.Bounds);
			//view.Value = "Hello Click World";
			View.AddSubview(view);
		}
	}

	public class ClickView : NSView
	{
		public ClickView (CoreGraphics.CGRect bounds)
			: base (bounds)
		{
		}

		public ClickView()
		{
		}


		public override void MouseDown(NSEvent theEvent)
		{
			Console.WriteLine("Mouse down");
		}

		public override void MouseUp(NSEvent theEvent)
		{
			Console.WriteLine("Mouse up");
		}

		public override void RightMouseDown(NSEvent theEvent)
		{
			Console.WriteLine("Right down");
		}

		public override void RightMouseUp(NSEvent theEvent)
		{
			Console.WriteLine("Right up");
		}
	}
}

