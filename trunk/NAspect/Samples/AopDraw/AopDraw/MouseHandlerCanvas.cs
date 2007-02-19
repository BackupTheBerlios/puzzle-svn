using System;
using System.Collections.Generic;
using System.Text;
using AopDraw.Classes;
using System.Windows.Forms;
using AopDraw.Interfaces;
using AopDraw.Classes.Shapes;
using AopDraw.Enums;
using System.Drawing;

namespace AopDraw
{
    public class MouseHandlerCanvas : Canvas
    {

        private DrawAction currentAction = DrawAction.None;
        private Point mouseDownPos;
        public void MouseMove(object sender, MouseEventArgs e)
        {
            double x = e.X;
            double y = e.Y;
            Shape shape = GetShapeFromMouse(e);

            if (shape is IMouseHandler)
            {
                IMouseHandler mouseShape = (IMouseHandler)shape;
                mouseShape.MouseMove(shape, e);
            }


            IResizable resizable = shape as IResizable;
            if (resizable != null && resizable.GetGripBounds().Contains(e.X, e.Y) || currentAction == DrawAction.Resize)
            {
                Cursor.Current = Cursors.SizeNWSE;
                if (currentAction == DrawAction.Resize)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        int xdelta = e.X - mouseDownPos.X;
                        int ydelta = e.Y - mouseDownPos.Y;

                        ResizeSelectedShapes(xdelta, ydelta);
                    }
                }
            }
            else
            {
                Cursor.Current = Cursors.Default;
                if (e.Button == MouseButtons.Left)
                {
                    int xdelta = e.X - mouseDownPos.X;
                    int ydelta = e.Y - mouseDownPos.Y;

                    MoveSelectedShapes(xdelta, ydelta);
                }
            }
        }

        public void MouseDown(object sender, MouseEventArgs e)
        {

            Shape shape = GetShapeFromMouse(e);
            if (shape is IMouseHandler)
            {
                IMouseHandler mouseShape = (IMouseHandler)shape;
                mouseShape.MouseDown(shape, e);
            }

            if (e.Button == MouseButtons.Left)
            {


                mouseDownPos = new Point(e.X, e.Y);

                ClearSelection();
                if (shape != null)
                {
                    ISelectable selectable = shape as ISelectable;
                    if (selectable != null)
                    {
                        selectable.IsSelected = true;
                    }

                }

                IResizable resizable = shape as IResizable;
                IMovable movable = shape as IMovable;
                if (resizable != null && resizable.GetGripBounds().Contains(e.X, e.Y))
                {
                    resizable.RememberSize();
                    currentAction = DrawAction.Resize;
                }
                else if (movable != null)
                {
                    movable.RememberLocation();
                    currentAction = DrawAction.Move;
                }
            }
        }

        public void MouseUp(object sender, MouseEventArgs e)
        {
            Shape shape = GetShapeFromMouse(e);
            if (shape is IMouseHandler)
            {
                IMouseHandler mouseShape = (IMouseHandler)shape;
                mouseShape.MouseUp(shape, e);
            }

            currentAction = DrawAction.None;
            if (e.Button == MouseButtons.Right)
            {
                if (shape != null)
                {
                    IDesignable designable = shape as IDesignable;
                    if (designable != null)
                    {
                        designable.BorderSize = 5;
                        designable.BorderColor = Color.Green;
                        designable.FillColor = Color.Magenta;
                    }
                }
            }
        }

        private Shape GetShapeFromMouse(MouseEventArgs e)
        {
            double x = e.X;
            double y = e.Y;
            Shape shape = GetShapeAt(x, y);
            return shape;
        }
    }
}
