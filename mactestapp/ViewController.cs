using System;

using AppKit;
using Foundation;

namespace mactestapp {
	public partial class ViewController : NSViewController {
		public ViewController (IntPtr handle) : base (handle)
		{
		}
		SavePanelDelegate del = new SavePanelDelegate ();
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Do any additional setup after loading the view.
			
			var panel = new NSSavePanel ();
			panel.Delegate = del;
			View.AddSubview (NSButton.CreateButton ("a", () => panel.RunModal ()));
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

	class SavePanelDelegate : NSObject, INSOpenSavePanelDelegate {
		[Export ("panel:didChangeToDirectoryURL:")]
		void DidChangeToDirectory (NSPanel sender, NSObject args)
		{
			Console.WriteLine ($"DidChangeDirectory ({sender}, {args} = {args?.GetType ()})");
			if (args is NSUrl url)
				Console.WriteLine ($"    url: {url?.Path}");
		}
	}
}
