using System;
using System.Collections.Generic;
using System.Text;
using AopDraw.Interfaces;
using System.Drawing;

namespace AopDraw.Classes
{
    public class Canvas : ICanvas
    {
        #region Property Shapes
        private IList<IShape> shapes = new List<IShape>();
        public IList<IShape> Shapes
        {
            get
            {
                return this.shapes;
            }
            set
            {
                this.shapes = value;
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

        #region Property IsDirty 
        private bool isDirty;
        public bool IsDirty
        {
            get
            {
                return this.isDirty;
            }
            set
            {
                this.isDirty = value;
            }
        }                        
        #endregion

        public virtual void Render(Graphics g)
        {
            CanvasPaintArgs e = new CanvasPaintArgs();
            e.g = g;


            #region RenderShapes
            foreach (IShape shape in shapes)
            {
                e.BorderPen = Pens.Black;
                e.FillBrush = Brushes.White;

                IDesignable designable = shape as IDesignable;

                if (designable != null)
                {
                    e.FillBrush = new SolidBrush(designable.FillColor);

                    e.BorderPen = new Pen(designable.BorderColor, designable.BorderSize);
                }



                shape.Render(e);
            } 
            #endregion

            #region RenderSelection
            foreach (IShape shape in shapes)
            {
                ISelectable selectable = shape as ISelectable;
                if (selectable != null && selectable.IsSelected)
                {
                    selectable.RenderSelection(e);

                    IResizable resizable = shape as IResizable;
                    if (resizable != null)
                    {
                        resizable.RenderResize(e);
                    }
                }
            } 
            #endregion

            IsDirty = false;
        }

        public virtual void MoveSelectedShapes(double xOffset, double yOffset)
        {
            foreach (IShape shape in shapes)
            {
                ISelectable selectable = shape as ISelectable;
                if (selectable != null && selectable.IsSelected)
                {
                    IMovable movable = shape as IMovable;
                    if (movable != null)
                    {
                        movable.MoveTo(shape.X + xOffset, shape.Y + yOffset);
                    }
                }
            }

            IsDirty = true;
        }

        public virtual void ResizeSelectedShapes(double width, double height)
        {
            foreach (IShape shape in shapes)
            {
                ISelectable selectable = shape as ISelectable;
                if (selectable != null && selectable.IsSelected)
                {
                    IResizable resizable = shape as IResizable;
                    if (resizable != null)
                    {
                        resizable.Resize( width,height);
                    }
                }
            }

            IsDirty = true;
        }

        public virtual void DeleteSelectedShapes()
        {
            for (int i = shapes.Count - 1; i >= 0; i--)
            {
                IShape shape = shapes[i];
                ISelectable selectable = shape as ISelectable;

                if (selectable != null && selectable.IsSelected)
                {
                    shapes.RemoveAt(i);
                }
            }

            IsDirty = true;
        }

        public virtual void AddShape(IShape shape)
        {
            shapes.Add(shape);
            ICanvasAware canvasAware = shape as ICanvasAware;
            if (canvasAware != null)
                canvasAware.Canvas = this;
            IsDirty = true;
        }

        public virtual IShape GetShapeAt(double x, double y)
        {
            for (int i = shapes.Count - 1; i >= 0; i--)
            {
                IShape shape = shapes[i];

                if (shape.HitTest(x, y))
                {
                    return shape;
                }
            }
            return null;
        }

        public virtual IList<IShape> GetShapesAt(double x, double y, double width, double height)
        {
            IList<IShape> matchedShapes = new List<IShape>();
            RectangleF bounds = new RectangleF((float)x, (float)y, (float)width, (float)height);
            foreach (IShape shape in shapes)
            {
                 IShape2D shape2D = shape as IShape2D;
                 if (shape2D != null)
                 {
                     if (shape2D.GetBoundsF().IntersectsWith(bounds))
                     {
                         matchedShapes.Add(shape);
                     }
                 }
            }
            return matchedShapes;
        }

        public virtual void ClearSelection()
        {
            foreach (IShape shape in shapes)
            {
                ISelectable selectable = shape as ISelectable;
                if (selectable != null)
                {
                    selectable.IsSelected = false;
                }
            }
            IsDirty = true;
        }
    }
}
