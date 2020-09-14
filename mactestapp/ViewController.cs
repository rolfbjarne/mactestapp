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

			// Insert code here to initialize your application
			var allInterfaces = NetworkInterface.GetAllNetworkInterfaces ();
			var sb = new StringBuilder ();
			sb.AppendFormat ("Found {0} network interfaces\n", allInterfaces.Length);
			foreach (var iface in allInterfaces)
				sb.AppendFormat ("    {0}\n", iface.Name);
			Console.WriteLine (sb);

			var text = new NSTextView (View.Bounds);
			text.Value = sb.ToString ();
			View.AddSubview (text);

			new Thread (() => {
				Thread.Sleep (5000);
				Environment.Exit (40);
			}).Start ();
		}
	}
}

