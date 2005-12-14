using System.Drawing;

namespace Puzzle.Windows.Forms.FormatLabel
{
	public class Word
	{
		public Image Image = null;
		public string Text = "";
		public int Width = 0;
		public int Height = 0;
		public Element Element = null;
		public Rectangle ScreenArea = new Rectangle(0, 0, 0, 0);
		//	public bool Link=false;

		public Word()
		{
		}
	}
}