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
			var panel = NSSavePanel.SavePanel;
			Console.WriteLine ("Panel: 0x{0} => {1}", panel.Handle.ToString ("x"), panel.Class.Name);
			return panel;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			NSNotificationCenter.DefaultCenter.AddObserver (NSWindow.DidResizeNotification, notification => {
				Console.WriteLine ("DidResizeNotification ({0})", notification.Object.GetType ());
				Console.WriteLine (Environment.StackTrace);
			});

			NSUserDefaults.StandardUserDefaults.SetBool (false, "NSUseRemoteSavePanel");

			//for (int i = 0; i < 10; ++i)
			using (var v = OnCreatePanel ())
				v.RunModal ();
		}
	}
}
