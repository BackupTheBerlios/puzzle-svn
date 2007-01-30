using System;
using System.Collections.Generic;
using System.Text;
using AopDraw.Classes;
using System.Drawing;

namespace AopDraw.Interfaces
{
    public interface IShape
    {
        double X { get;set;}
        double Y { get;set;}
        
        void Render(CanvasPaintArgs e);
        bool HitTest(double x, double y);
    }
}
