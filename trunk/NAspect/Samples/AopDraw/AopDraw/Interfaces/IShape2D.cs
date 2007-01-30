using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AopDraw.Interfaces
{
    public interface IShape2D : IShape
    {
        double Width { get;set;}
        double Height { get;set;}
        RectangleF GetBoundsF();
        Rectangle GetBounds();
    }
}
