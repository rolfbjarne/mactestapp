using System;
using System.Net.NetworkInformation;

using AppKit;
using Foundation;

namespace mactestapp {
	[Register ("AppDelegate")]
	public class AppDelegate : NSApplicationDelegate {
		enum FooEnum {
			Foo = 1,
			Bar = 2
		}

		public AppDelegate ()
		{
		}

		public override void DidFinishLaunching (NSNotification notification)
		{
			// Insert code here to initialize your application
			var allInterfaces = NetworkInterface.GetAllNetworkInterfaces ();
			Console.WriteLine ("Found {0} network interfaces", allInterfaces.Length);
			foreach (var iface in allInterfaces)
				Console.WriteLine ("    {0}", iface.Name);
		}

		public override void WillTerminate (NSNotification notification)
		{
			// Insert code here to tear down your application
		}
	}
}

