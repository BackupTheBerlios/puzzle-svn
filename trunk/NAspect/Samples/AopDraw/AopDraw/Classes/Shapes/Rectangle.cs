using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AopDraw.Classes.Shapes
{
    public class RectangleShape : Shape2D
    {
        public override void Render(CanvasPaintArgs e)
        {
            RectangleF bounds = GetBoundsF();
            e.g.FillRectangle(e.FillBrush, bounds);
            e.g.DrawRectangle(e.BorderPen, bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }
    }
}
