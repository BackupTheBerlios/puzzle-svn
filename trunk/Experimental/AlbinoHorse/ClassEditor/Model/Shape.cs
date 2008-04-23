using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AlbinoHorse.Infrastructure;

namespace AlbinoHorse.Model
{
    public abstract class Shape
    {
        #region Property Bounds 
        private Rectangle bounds;
        public virtual Rectangle Bounds
        {
            get
            {
                return this.bounds;
            }
            set
            {
                this.bounds = value;
            }
        }                        
        #endregion

        #region Property Selected
        private bool selected;
        public virtual bool Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                this.selected = value;
            }
        }
        #endregion

        public virtual void Draw(RenderInfo info) { }
        public virtual void DrawBackground(RenderInfo info) { }
        public virtual void DrawPreview(RenderInfo info) { }
        public virtual void PreviewDrawBackground(RenderInfo info) { }


        public virtual void OnMouseDown(ShapeMouseEventArgs args)
        {            
        }

        public virtual void OnMouseUp(ShapeMouseEventArgs args)
        {
        }

        public virtual void OnMouseMove(ShapeMouseEventArgs args)
        {
        }

        public virtual void OnClick(ShapeMouseEventArgs args)
        {
        }

        public virtual void OnDoubleClick(ShapeMouseEventArgs args)
        {
        }

        public virtual void OnKeyPress(ShapeKeyEventArgs args)
        {
        }

        protected void DrawSelectionHandle(RenderInfo info, Point point, object identifier)
        {
            DrawSelectionHandle(info, point);

            Rectangle bounds = new Rectangle(point.X - 4, point.Y - 4, 8, 8);
            BoundingBox bBox = new BoundingBox();
            bBox.Bounds = bounds;
            bBox.Data = identifier;
            bBox.Target = this;
            info.BoundingBoxes.Add(bBox);
        }

        protected void DrawSelectionHandle(RenderInfo info, Point point)
        {
            Rectangle bounds = new Rectangle(point.X - 4, point.Y - 4, 8, 8);
            info.Graphics.FillRectangle(Brushes.Gray, bounds);
            bounds.Inflate(-1, -1);
            info.Graphics.FillRectangle(Brushes.White, bounds);
        }

    }
}
