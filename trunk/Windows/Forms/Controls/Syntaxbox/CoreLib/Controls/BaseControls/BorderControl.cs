using System;
using System.Windows.Forms;

namespace Puzzle.Windows.Forms
{
	public class ControlBorderPainter : NativeWindow
	{
		public ControlBorderPainter(IntPtr Handle)
		{
			this.AssignHandle(Handle);
		}

		protected override void WndProc(ref Message m)
		{
		}
	}
}