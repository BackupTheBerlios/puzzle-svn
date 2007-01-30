using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AopDraw.Classes.Shapes
{
    public class CircleShape : Shape2D
    {
        public override void Render(CanvasPaintArgs e)
        {
            e.g.FillEllipse(e.FillBrush, GetBoundsF());
            e.g.DrawEllipse(e.BorderPen, GetBoundsF());
        }
    }
}
