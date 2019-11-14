using AppKit;

namespace mactestapp {
	static class MainClass {
		static void Main (string [] args)
		{
			NSApplication.Init ();
			NSApplication.Main (new string [] { "-NSUseRemoteSavePanel", "NO" });
		}
	}
}
