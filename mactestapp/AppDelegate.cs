using AppKit;
using CoreFoundation;
using Foundation;

using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace mactestapp {
	[Register ("AppDelegate")]
	public class AppDelegate : NSApplicationDelegate {

		const MemoryPressureFlags notificationFlags = MemoryPressureFlags.Critical | MemoryPressureFlags.Warn | MemoryPressureFlags.Normal;
		internal DispatchSource.MemoryPressure DispatchSource { get; private set; }

		public AppDelegate ()
		{
			//DispatchSource = new DispatchSource.MemoryPressure (notificationFlags, DispatchQueue.MainQueue);
			//DispatchSource.SetEventHandler (() => {
			//	Console.WriteLine ($"Memory event!!! {DispatchSource.PressureFlags}");
			//});
			//DispatchSource.SetCancelHandler (() => {
			//	Console.WriteLine ("Cancel handler");
			//});
			//DispatchSource.SetRegistrationHandler (() => {
			//	Console.WriteLine ("Registration handler");
			//});
			//DispatchSource.Resume ();
			Task.Run (async () => {
				await new MacPlatform.Tests.MemoryMonitorTests ().TestMemoryMonitorWithSimulatedValues ();
			});
		}

		public override void DidFinishLaunching (NSNotification notification)
		{
			//const int allocate = 1024 * 1024 * 1000 / 4;
			//long allocated = 0;
			//var pagesize = getpagesize ();
			//var randomData = new byte [pagesize];
			//var rnd = new Random ((int) DateTime.Now.Ticks);

			//rnd.NextBytes (randomData);

			//var tmpAllocate = 1024 * 1024 * 1000 * 55L;
			//var ptr = Marshal.AllocHGlobal (new IntPtr (tmpAllocate));
			//int rv = 0;
			//rv = mlock (ptr, (nuint) tmpAllocate);
			//allocated += allocate;
			//Console.WriteLine ("Main allocated {0} bytes ({1:#.##} kb, {2:#.##} mb, {3:#.##} GB) => 0x{4} (mlock status: {5})", allocated, allocated / 1024.0, allocated / (1024.0 * 1024.0), allocated / (1024.0 * 1024.0 * 1024.0), ptr.ToString ("x"), rv);

			//// Insert code here to initialize your application
			//NSTimer.CreateRepeatingScheduledTimer (1, (v) => {
			//	ptr = Marshal.AllocHGlobal (allocate);
			//	unsafe {
			//		ulong* buffer = (ulong*) ptr;
			//		var step = sizeof (long);

			//		for (var i = 0; i < allocate; i += pagesize) {
			//			//Console.WriteLine ($"memcpy (0x{(ptr + i * pagesize).ToString ("x")}, randomData, {pagesize});");
			//			memcpy (ptr + i, randomData, pagesize);
			//			//Console.WriteLine ($"buffer [{i / step}] = 0xDEADF00d;");
			//			//buffer [i / step] = 0xFFFFFFFFFFFFFFFFUL;
			//		}



			//		//Console.WriteLine ($"{buffer [0]} {buffer [1]} {buffer [2]} {buffer [3]} {buffer [4]} {buffer [5]} {buffer [6]} {buffer [7]}");
			//		//for (var i = 0; i < (allocate / step); i += (pagesize / step)) {
			//		//	buffer [i] = 0xFEFEEFEFFEFEEFEFUL;
			//		//	Console.WriteLine ($"buffer [{i}] = 0x{buffer [i].ToString ("x")};");
			//		//}
			//		//Console.WriteLine ($"{buffer [0]} {buffer [1]} {buffer [2]} {buffer [3]} {buffer [4]} {buffer [5]} {buffer [6]} {buffer [7]}");
			//	}
			//	//var rv = mlock (ptr, allocate);
			//	allocated += allocate;
			//	Console.WriteLine ("Allocated {0} bytes ({1:#.##} kb, {2:#.##} mb, {3:#.##} GB) => 0x{4} (mlock status: {5})", allocated, allocated / 1024.0, allocated / (1024.0 * 1024.0), allocated / (1024.0 * 1024.0 * 1024.0), ptr.ToString ("x"), rv);
			//});
		}
		
		[DllImport ("libc")]
		static extern int mlock (IntPtr addr, nuint length);

		[DllImport ("libc")]
		static extern IntPtr memcpy (IntPtr destination, byte [] source, nint size);

		[DllImport ("libc")]
		static extern int getpagesize ();
	}
}
