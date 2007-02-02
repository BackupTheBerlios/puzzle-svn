using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AopDraw.Interfaces;
using AopDraw.Classes;
using AopDraw.Classes.Shapes;
using Puzzle.NAspect.Framework;
using Puzzle.NAspect.Framework.Aop;
using AopDraw.Mixins;
using AopDraw.Enums;
using Puzzle.NAspect.Framework.Interception;
using AopDraw.Interceptors;
using System.Drawing.Drawing2D;
using AopDraw.Attributes;

namespace AopDraw
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        ICanvas canvas = new Canvas();

        

        RectangleShape rectangle;
        private void Form1_Load(object sender, EventArgs e)
        {
            Application.Idle += new EventHandler(Application_Idle);

            IEngine engine = new Engine("AopDraw");

            InterfaceAspect typeDescriptorAspect = new InterfaceAspect("typeDescriptorAspect", typeof(Shape), new Type[] { typeof(CustomTypeDescriptorMixin) }, new IPointcut[] { });
            engine.Configuration.Aspects.Add(typeDescriptorAspect);

            InterfaceAspect guidAspect = new InterfaceAspect("guidAspect", typeof(Shape), new Type[] { typeof(GuidObject) }, new IPointcut[] { });
            engine.Configuration.Aspects.Add(guidAspect);

            AttributeAspect selectable2DAspect = new AttributeAspect("selectable2DAspect", typeof(SelectableAttribute), new Type[] { typeof(SelectableShape2DMixin) }, new IPointcut[] { });
            engine.Configuration.Aspects.Add(selectable2DAspect);

            InterfaceAspect selectable1DAspect = new InterfaceAspect("selectable1DAspect", typeof(Shape1D), new Type[] { typeof(SelectableShape1DMixin) }, new IPointcut[] { });
            engine.Configuration.Aspects.Add(selectable1DAspect);


            AttributeAspect movableAspect = new AttributeAspect("movableAspect", typeof(MovableAttribute), new Type[] { typeof(MovableShape2DMixin) }, new IPointcut[] { });
            engine.Configuration.Aspects.Add(movableAspect);

            AttributeAspect designableAspect = new AttributeAspect("designableAspect", typeof(DesignableAttribute), new Type[] { typeof(DesignableMixin) }, new IPointcut[] { });
            engine.Configuration.Aspects.Add(designableAspect);

            AttributeAspect resizableAspect = new AttributeAspect("resizableAspect", typeof(ResizableAttribute), new Type[] { typeof(ResizableMixin) }, new IPointcut[] { });
            engine.Configuration.Aspects.Add(resizableAspect);

            InterfaceAspect canvasAwareAspect = new InterfaceAspect("canvasAwareAspect", typeof(Shape), new Type[] { typeof(CanvasAwareMixin) }, new IPointcut[] {new SignaturePointcut ("set_*", new ShapePropertyInterceptor())});
            engine.Configuration.Aspects.Add(canvasAwareAspect);

            SquareShape square = engine.CreateProxy<SquareShape>();
            square.X = 10;
            square.Y = 10;
            square.Width = 100;
            square.Height = 100;

            canvas.AddShape(square);


            CircleShape circle = engine.CreateProxy<CircleShape>();
            circle.X = 240;
            circle.Y = 120;
            circle.Width = 200;
            circle.Height = 200;

            canvas.AddShape(circle);


            rectangle = engine.CreateProxy<RectangleShape>();
            rectangle.X = 50;
            rectangle.Y = 90;
            rectangle.Width = 200;
            rectangle.Height = 50;

            canvas.AddShape(rectangle);

            Shape1D line = engine.CreateProxy<Shape1D>();
            line.X = 200;
            line.Y = 200;
            line.X2 = 400;
            line.Y2 = 340;
            canvas.AddShape(line);


            TextShape text = engine.CreateProxy<TextShape>();
            text.X = 140;
            text.Y = 3;
            text.Width = 200;
            text.Height = 100;
            canvas.AddShape(text);
        }

        


        private Point mouseDownPos;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            double x = e.X;
            double y = e.Y;




            Shape shape = canvas.GetShapeAt(x, y);

            #region Displaycaption
            if (shape != null)
            {
                IGuidObject guidObject = shape as IGuidObject;
                if (guidObject != null)
                {
                    this.Text = shape.ToString() + guidObject.Guid;
                }
                else
                {
                    this.Text = shape.ToString();
                }
            }
            else
            {
                this.Text = "";
            }
            #endregion


            IResizable resizable = shape as IResizable;
            if (resizable != null && resizable.GetGripBounds().Contains(e.X, e.Y) || currentAction == DrawAction.Resize)
            {
                Cursor = Cursors.SizeNWSE;
                if (currentAction == DrawAction.Resize)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        int xdelta = e.X - mouseDownPos.X;
                        int ydelta = e.Y - mouseDownPos.Y;

                        mouseDownPos = new Point(e.X, e.Y);
                        canvas.ResizeSelectedShapes(xdelta, ydelta);
                    }
                }
            }           
            else
            {
                Cursor = Cursors.Default;
                if (e.Button == MouseButtons.Left)
                {
                    int xdelta = e.X - mouseDownPos.X;
                    int ydelta = e.Y - mouseDownPos.Y;

                    mouseDownPos = new Point(e.X, e.Y);
                    canvas.MoveSelectedShapes(xdelta, ydelta);
                }
            }
        }

        private DrawAction currentAction = DrawAction.None;
        
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Shape shape = GetShapeFromMouse(e);
                
                
                mouseDownPos = new Point(e.X, e.Y);

                canvas.ClearSelection();
                if (shape != null)
                {
                    ISelectable selectable = shape as ISelectable;
                    if (selectable != null)
                    {
                        selectable.IsSelected = true;
                    }
                    
                }

                IResizable resizable = shape as IResizable;
                if (resizable != null && resizable.GetGripBounds().Contains(e.X, e.Y))
                {
                    currentAction = DrawAction.Resize;
                }
                else
                {
                    currentAction = DrawAction.Move;                    
                }

                propertyGrid1.SelectedObject = shape;
            }
        }

        private Shape GetShapeFromMouse(MouseEventArgs e)
        {
            double x = e.X;
            double y = e.Y;
            Shape shape = canvas.GetShapeAt(x, y);
            return shape;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            currentAction = DrawAction.None;
            if (e.Button == MouseButtons.Right)
            {
                Shape shape = GetShapeFromMouse(e);
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

        private void button1_Click(object sender, EventArgs e)
        {
            rectangle.Width = 300;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            if (canvas.IsDirty)
            {
                panel1.Invalidate();
                canvas.IsDirty = false;
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            canvas.Render(e.Graphics);
        }
    }
}