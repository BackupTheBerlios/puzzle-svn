using System;
using System.Collections.Generic;
using System.Text;
using AopDraw.Interfaces;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Framework;
using AopDraw.Classes;
using System.Windows.Forms;
using System.Drawing;
using AopDraw.Classes.Shapes;

namespace AopDraw.Mixins
{
    public class ResizableMixin : IResizable, IProxyAware
    {
        private Shape2D shape;

        public virtual void Resize(double width, double height)
        {
            shape.Width += width;
            shape.Height += height;
            DirtyCanvas();
        }

        public void SetProxy(IAopProxy target)
        {
            Shape2D shape = target as Shape2D;

            if (shape == null)
                throw new ArgumentException("target is not an IShape2D");

            this.shape = shape;
        }

        public virtual Rectangle GetGripBounds()
        {
            Rectangle bounds = shape.GetBounds();
            Rectangle sizeBounds = new Rectangle(bounds.Right - 16, bounds.Bottom - 16, 16, 16);
            return sizeBounds;
        }

        private void DirtyCanvas()
        {
            ICanvasAware canvasAware = shape as ICanvasAware;
            if (canvasAware != null)
                canvasAware.Canvas.IsDirty = true;
        }

        public void RenderResize(CanvasPaintArgs e)
        {

            ControlPaint.DrawSizeGrip(e.g, SystemColors.Control, GetGripBounds());
        }
    }
}
