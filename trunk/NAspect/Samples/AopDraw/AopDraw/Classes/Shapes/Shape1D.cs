using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AopDraw.Attributes;
using AopDraw.Interfaces;

namespace AopDraw.Classes.Shapes
{
    public class Shape1D : Shape
    {
        #region Property X2 
        private double x2;
        public double X2
        {
            get
            {
                return this.x2;
            }
            set
            {
                this.x2 = value;
            }
        }                        
        #endregion

        #region Property Y2 
        private double y2;
        public double Y2
        {
            get
            {
                return this.y2;
            }
            set
            {
                this.y2 = value;
            }
        }                        
        #endregion

        public RectangleF GetBoundsF()
        {
            return new RectangleF((float)X, (float)Y, (float)(X2-X), (float)(Y2-Y));
        }
        public Rectangle GetBounds()
        {
            return new Rectangle((int)X, (int)Y, (int)(X2 - X), (int)(Y2 - Y));
        }

        public override void Render(CanvasPaintArgs e)
        {
            e.g.DrawLine(e.BorderPen, (float)X, (float)Y, (float)X2, (float)Y2);
        }

        public override bool HitTest(double x, double y)
        {
            if (this.GetBoundsF().Contains((float)x, (float)y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
