using System.Windows.Forms;
using System.Windows.Forms.Design;
using Puzzle.Drawing;

namespace Puzzle.Windows.Forms.PopUpContainer
{
	public class PopUpContainerDesigner : ScrollableControlDesigner
	{
		public PopUpContainerDesigner() : base()
		{
		}

		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			this.DrawGrid = false;

			PopUpContainerControl c = this.Control as PopUpContainerControl;

			if (c != null && c.BorderStyle == System.Windows.Forms.BorderStyle.None)
			{
				DrawingTools.DrawDesignTimeBorder(pe.Graphics, c.ClientRectangle);
			}
			base.OnPaintAdornments(pe);
		}

	}
}