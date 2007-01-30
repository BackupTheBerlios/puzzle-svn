using System;
using System.Collections.Generic;
using System.Text;
using AopDraw.Interfaces;
using System.Drawing;

namespace AopDraw.Classes.Shapes
{
    public abstract class Shape2D : IShape2D
    {
        #region Property X
        private double x;
        public virtual double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }
        #endregion

        #region Property Y
        private double y;
        public virtual double Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }
        #endregion

        #region Property Width
        private double width;
        public virtual double Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }
        #endregion

        #region Property Height
        private double height;
        public virtual double Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }
        #endregion

        public RectangleF GetBoundsF()
        {
            return new RectangleF((float)x, (float)y, (float)width, (float)height);
        }
        public Rectangle GetBounds()
        {
            return new Rectangle((int)x, (int)y, (int)width, (int)height);
        }

        public abstract void Render(CanvasPaintArgs e);

        public virtual bool HitTest(double x, double y)
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
