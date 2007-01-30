using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AopDraw.Interfaces
{
    public interface IShape1D : IShape
    {
        double X2 { get;set;}
        double Y2 { get;set;}
        RectangleF GetBoundsF();
        Rectangle GetBounds();
    }
}
