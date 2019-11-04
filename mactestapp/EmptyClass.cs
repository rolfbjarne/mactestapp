using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using Foundation;
using CoreFoundation;


namespace MacPlatform.Tests {
	//[TestFixture]
	public class MemoryMonitorTests /*: IdeTestBase */ {
		//[Test]
		public async Task TestMemoryMonitorWithSimulatedValues ()
		{
			using (var monitor = new MacMemoryMonitor ()) {
				var countsReached = new Dictionary<PlatformMemoryStatus, int> ();

				monitor.StatusChanged += (args) => {
					Console.WriteLine ($"StatusChanged ({args})");
					//if (!countsReached.TryGetValue (args.MemoryStatus, out int count))
					//	count = 0;
					//countsReached [args.MemoryStatus] = ++count;
				};

				await SimulateMemoryPressure ("warn");
				await SimulateMemoryPressure ("critical");

				//Assert.That (countsReached [PlatformMemoryStatus.Low], Is.GreaterThanOrEqualTo (1));
				//Assert.That (countsReached [PlatformMemoryStatus.Critical], Is.GreaterThanOrEqualTo (1));
				Console.WriteLine ("DONE!");
			}
		}

		// We need root for this to work.
		static async Task SimulateMemoryPressure (string kind)
		{
			using (var cts = new CancellationTokenSource (TimeSpan.FromSeconds (3))) {
				var done = new TaskCompletionSource<bool> ();

				var psi = new ProcessStartInfo ("/usr/bin/sudo", "-n /usr/bin/memory_pressure -S -l " + kind) {
				};

				using (var process = Process.Start (psi)) {
					process.Exited += (o, args) => done.SetResult (true);
					process.EnableRaisingEvents = true;

					await done.Task;
				}
			}
		}
	}

	internal class MacMemoryMonitor : /* MemoryMonitor,*/ IDisposable {
		const MemoryPressureFlags notificationFlags = MemoryPressureFlags.Critical | MemoryPressureFlags.Warn | MemoryPressureFlags.Normal;
		internal DispatchSource.MemoryPressure DispatchSource { get; private set; }

		public event Action<MemoryPressureFlags> StatusChanged;

		public MacMemoryMonitor ()
		{
			DispatchSource = new DispatchSource.MemoryPressure (notificationFlags, DispatchQueue.MainQueue);
			DispatchSource.SetEventHandler (() => {
				StatusChanged (DispatchSource.PressureFlags);
				//var metadata = CreateMemoryMetadata (DispatchSource.PressureFlags);

				//var args = new PlatformMemoryStatusEventArgs (metadata);
				//OnStatusChanged (args);
			});
			DispatchSource.Resume ();
		}

		public void Dispose ()
		{
			if (DispatchSource != null) {
				DispatchSource.Cancel ();
				DispatchSource.Dispose ();
				DispatchSource = null;
			}
		}



	}
	
		public enum PlatformMemoryStatus
		{
			Normal,
			Low,
			Critical
		}
}