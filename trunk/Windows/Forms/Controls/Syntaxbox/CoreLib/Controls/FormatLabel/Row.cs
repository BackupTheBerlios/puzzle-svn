using System.Collections;

namespace Puzzle.Windows.Forms.FormatLabel
{
	public class Row
	{
		public int Width = 0;
		public int Height = 0;
		public int BottomPadd = 0;
		public ArrayList Words = new ArrayList();
		public bool RenderSeparator = false;
		public bool Visible = false;
		public int Top = 0;

		public Row()
		{
		}
	}
}