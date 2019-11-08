using System;

using AppKit;
using Foundation;

namespace mactestapp {
	public partial class ViewController : NSViewController {
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		NSSavePanel OnCreatePanel ()
		{
			var panel = NSOpenPanel.OpenPanel;
			Console.WriteLine ("Panel: 0x{0} => {1}", panel.Handle.ToString ("x"), panel.Class.Name);
			panel.CanChooseDirectories = false;
			panel.CanChooseFiles = true;
			return panel;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			for (int i = 0; i < 10; ++i)
				using (var v = OnCreatePanel ())
					v.RunModal ();
		}

		public override NSObject RepresentedObject {
			get {
				return base.RepresentedObject;
			}
			set {
				base.RepresentedObject = value;
				// Update the view, if already loaded.
			}
		}
	}
}
