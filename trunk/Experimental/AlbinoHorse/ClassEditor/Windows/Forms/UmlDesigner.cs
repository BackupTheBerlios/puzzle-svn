using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AlbinoHorse.Model;
using AlbinoHorse.Infrastructure;
using System.Drawing.Drawing2D;


namespace AlbinoHorse.Windows.Forms
{
    public partial class UmlDesigner : Control
    {
        private static class Settings
        {
            public static Font inputFont = new Font("Arial", 10f);
            public static Pen DrawRelation = MakeDrawRelationPen();

            private static Pen MakeDrawRelationPen()
            {
                Pen pen = new Pen(Color.Gray, 3);

                pen.DashStyle = DashStyle.Dash;
                return pen;
            }
        }


        public UmlDesigner()
        {
            
            InitializeComponent();
            this.Zoom = 1;
            Diagram = new UmlDiagram();

            MainCanvas.MouseMove += new MouseEventHandler(MainCanvas_MouseMove);
            MainCanvas.MouseDown += new MouseEventHandler(MainCanvas_MouseDown);
            MainCanvas.MouseUp += new MouseEventHandler(MainCanvas_MouseUp);
            MainCanvas.Paint += new PaintEventHandler(MainCanvas_Paint);
            MainCanvas.DoubleClick += new EventHandler(MainCanvas_DoubleClick);
            MainCanvas.CanvasScroll += new EventHandler(MainCanvas_CanvasScroll);
            MainCanvas.KeyPress += new KeyPressEventHandler(MainCanvas_KeyPress);
            MainCanvas.KeyDown += new KeyEventHandler(MainCanvas_KeyDown);

            PreviewCanvas.Paint += new PaintEventHandler(PreviewCanvas_Paint);
            PreviewCanvas.MouseDown += new MouseEventHandler(PreviewCanvas_MouseDown);
            PreviewCanvas.MouseMove += new MouseEventHandler(PreviewCanvas_MouseMove);
            BoundingBoxes = new List<BoundingBox>();
            GridSize = 21;
            ShowGrid = true;
            SnapToGrid = true;
            EditMode = EditMode.Normal;
        }

        void MainCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (EditMode == EditMode.Normal)
            {
                if (currentShape != null)
                {
                    Shape shape = currentShape;
                    ShapeKeyEventArgs args = new ShapeKeyEventArgs();
                    args.SnapToGrid = this.SnapToGrid;
                    args.Sender = this;
                    args.GridSize = this.GridSize;
                    args.Key = e.KeyCode;

                    shape.OnKeyPress(args);

                    if (args.Redraw)
                        this.Refresh();
                }
            }
        }

        void MainCanvas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (EditMode == EditMode.Normal)
            {

            }
        }

        private void SetViewPort(int x, int y)
        {
            

            float width = 0;
            float height = 0;
            foreach (Shape type in diagram.Shapes)
            {
                if (type is UmlInstanceType)
                {
                    width = Math.Max(type.Bounds.Right, width);
                    height = Math.Max(type.Bounds.Bottom, height);
                }
            }

            float max = Math.Max(width, height) + 700;
            float zoom = PreviewCanvas.Width / max;


            int xx = (int)((double)x / zoom);
            int yy = (int)((double)y / zoom);


            xx -= MainCanvas.Width / 2;
            yy -= MainCanvas.Height / 2;

            MainCanvas.AutoScrollPosition = new Point(xx, yy);
        }

        void PreviewCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                SetViewPort(e.X, e.Y);
        }
        
        void PreviewCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                SetViewPort(e.X, e.Y);
        }

        void MainCanvas_CanvasScroll(object sender, EventArgs e)
        {
            EndInput();
            PreviewCanvas.Invalidate();
        }

        void MainCanvas_DoubleClick(object sender, EventArgs e)
        {
            int x = (int)((double)(mouseDownPoint.X - MainCanvas.AutoScrollPosition.X) / Zoom);
            int y = (int)((double)(mouseDownPoint.Y - MainCanvas.AutoScrollPosition.Y) / Zoom);

            for (int i = BoundingBoxes.Count - 1; i >= 0; i--)
            {
                BoundingBox bbox = BoundingBoxes[i];
                if (bbox.Bounds.Contains(x, y))
                {
                    if (bbox.Target is Shape)
                    {
                        currentBoundingBox = bbox;
                        Shape shape = (Shape)bbox.Target;
                        ShapeMouseEventArgs args = new ShapeMouseEventArgs();
                        args.BoundingBox = bbox;
                        args.X = x;
                        args.Y = y;
                        args.Button = MouseButtons.Left;
                        args.Sender = this;
                        shape.OnDoubleClick(args);
                        if (args.Redraw)
                            this.Refresh();
                    }

                    return;
                }
            }           
        }

        public void AutoLayout()
        {
            //Diagram.AutoLayout();
            this.Refresh();
            this.Refresh();
        }

        #region Property BoundingBoxes
        private List<BoundingBox> boundingBoxes;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<BoundingBox> BoundingBoxes
        {
            get
            {
                return this.boundingBoxes;
            }
            set
            {
                this.boundingBoxes = value;
            }
        }
        #endregion

        public int GridSize { get; set; }
        public bool ShowGrid { get; set; }
        public bool SnapToGrid { get; set; }
        public EditMode EditMode { get; set; }

        public Rectangle GetItemBounds(object item)
        {
            foreach (BoundingBox bbox in BoundingBoxes)
            {
                if (bbox.Target == item)
                    return bbox.Bounds;

                if (bbox.Data == item)
                    return bbox.Bounds;
            }

            return Rectangle.Empty;
        }

        public int TransformToZoom(int x)
        {
            double xx = x;
            xx *= Zoom;
            return (int)xx;
        }

        public float TransformToZoom(float x)
        {
            double xx = x;
            xx *= Zoom;
            return (float)xx;
        }

        private DrawRelation endDrawRelation = null;
        private Shape relationStart = null;
        private Shape relationEnd = null;
        public void BeginDrawRelation(DrawRelation endDrawRelation)
        {
            this.endDrawRelation = endDrawRelation;
            this.EditMode = EditMode.BeginDrawRelation;
        }

        private Action endInputAction = null;
        private string originalText;
        public void BeginInput(Rectangle bounds, string text, Font font,Action endInputAction)
        {
            originalText = text;
            this.endInputAction = endInputAction;
            txtInput.Visible = false;
            txtInput.Multiline = false;
            txtInput.ScrollBars = ScrollBars.None;
            int x = bounds.Left;
            int y = bounds.Top;
            int width = bounds.Width;

            

            x = TransformToZoom(x)-2;
            y = TransformToZoom(y)-2;
            width = TransformToZoom(width);

            x += MainCanvas.AutoScrollPosition.X;
            y += MainCanvas.AutoScrollPosition.Y;

            float newFontSize = TransformToZoom(font.Size);
            Font newFont = new Font(font.Name, newFontSize, font.Style);


            txtInput.Left = x;
            txtInput.Top = y;
            txtInput.Width = width;
            txtInput.Height = 1;
            txtInput.Text = text;
            txtInput.Font = newFont;            
            txtInput.Visible = true;
            txtInput.SelectAll();
            txtInput.Focus();
        }

        public void BeginInputMultiLine(Rectangle bounds, string text, Font font, Action endInputAction)
        {
            originalText = text;
            this.endInputAction = endInputAction;
            txtInput.Visible = false;
            txtInput.Multiline = true;
            txtInput.ScrollBars = ScrollBars.None;
            int x = bounds.Left;
            int y = bounds.Top;
            int width = bounds.Width;
            int height = bounds.Height;


            x = TransformToZoom(x) ;
            y = TransformToZoom(y) ;
            width = TransformToZoom(width);
            height = TransformToZoom(height);

            x += MainCanvas.AutoScrollPosition.X;
            y += MainCanvas.AutoScrollPosition.Y;

            float newFontSize = TransformToZoom(font.Size);
            Font newFont = new Font(font.Name, newFontSize, font.Style);


            txtInput.Left = x;
            txtInput.Top = y;
            txtInput.Width = width;
            txtInput.Height = height;
            txtInput.Text = text;
            txtInput.Font = newFont;
            txtInput.Visible = true;
            txtInput.SelectAll();
            txtInput.Focus();
        }

        public void EndInput()
        {
            if (endInputAction != null)
                endInputAction();
            else
                return;

            endInputAction = null;
           
            Font oldFont = txtInput.Font;

            txtInput.Font = Settings.inputFont;
            txtInput.Visible = false;

            oldFont.Dispose(); // throw away the zoomed font            
            this.Refresh();
            MainCanvas.Focus();
        }


        void PreviewCanvas_Paint(object sender, PaintEventArgs e)
        {
            int x = 0;
            int y = 0;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            float width=0;
            float height=0;
            foreach (Shape type in diagram.Shapes)
            {
                if (type is UmlInstanceType)
                {
                    width = Math.Max(type.Bounds.Right, width);
                    height = Math.Max(type.Bounds.Bottom, height);
                }
            }

            float max = Math.Max(width, height)+700;
            float zoom = PreviewCanvas.Width / max;
 
            Rectangle visibleBounds = new Rectangle(x, y, PreviewCanvas.ClientSize.Width, PreviewCanvas.ClientSize.Height);


            RenderInfo renderInfo = new RenderInfo();
            renderInfo.Graphics = e.Graphics;
            renderInfo.Preview = true;

            renderInfo.VisualBounds = visibleBounds;
            renderInfo.GridSize = GridSize;
            e.Graphics.ScaleTransform(zoom, zoom);
            e.Graphics.TranslateTransform((float)(-x  / zoom + 50), (float)(-y  / zoom+50));
            renderInfo.Zoom = Zoom;
            Diagram.Draw(renderInfo);

            double vpWidth = (double)MainCanvas.Width / Zoom;
            double vpHeight = (double)MainCanvas.Height / Zoom;

            Rectangle viewPort = new Rectangle((int)(-MainCanvas.AutoScrollPosition.X / Zoom), (int)(-MainCanvas.AutoScrollPosition.Y / Zoom), (int)(vpWidth), (int)(vpHeight));

            SolidBrush viewPortBrush = new SolidBrush(Color.FromArgb(100, 200, 200, 240));
            e.Graphics.FillRectangle(viewPortBrush, viewPort);
            e.Graphics.DrawRectangle(Pens.DarkBlue, viewPort);
            viewPortBrush.Dispose();
        }

        

        void MainCanvas_Paint(object sender, PaintEventArgs e)
        {
            int x = (int)(-MainCanvas.AutoScrollPosition.X);
            int y = (int)(-MainCanvas.AutoScrollPosition.Y);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                       
            Rectangle visibleBounds = new Rectangle(x, y, this.ClientSize.Width, this.ClientSize.Height);
            RenderInfo renderInfo = new RenderInfo();
            renderInfo.Graphics = e.Graphics;
            renderInfo.VisualBounds = visibleBounds;
            renderInfo.GridSize = GridSize;
            renderInfo.ShowGrid = ShowGrid;
            e.Graphics.ScaleTransform((float)Zoom, (float)Zoom);
            e.Graphics.TranslateTransform((float)(-x / zoom), (float)(-y / zoom));
            renderInfo.Zoom = Zoom;
            Diagram.Draw(renderInfo);
            BoundingBoxes = renderInfo.BoundingBoxes;

            if (EditMode == EditMode.DrawRelation)
            {                
                e.Graphics.DrawLine(Settings.DrawRelation, mouseDownPoint, mouseCurrentPoint);
            }

            SetCanvasScrollSize(renderInfo);
        }

        private void SetCanvasScrollSize(RenderInfo renderInfo)
        {
            Size newSize = renderInfo.ReturnedBounds.Size;
            newSize.Height += 600;
            newSize.Width += 600;

            MainCanvas.AutoScrollMinSize = newSize;
        }
        

        #region Property Diagram 
        private UmlDiagram diagram;
        [Browsable (false)]
        [DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
        public UmlDiagram Diagram
        {
            get
            {
                return this.diagram;
            }
            set
            {
                this.diagram = value;
            }
        }                        
        #endregion

        #region Property Zoom 
        private double zoom;
        public double Zoom
        {
            get
            {
                return this.zoom;
            }
            set
            {
                if (value < 0.000001)
                    return;

                if (value > 3)
                    return;

                this.zoom = value;
                this.Refresh();
            }
        }                        
        #endregion


        private BoundingBox currentBoundingBox;
        private Shape currentShape;
        void MainCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            int x = (int)((double)(e.X - MainCanvas.AutoScrollPosition.X) / Zoom);
            int y = (int)((double)(e.Y - MainCanvas.AutoScrollPosition.Y) / Zoom);

            if (EditMode == EditMode.DrawRelation)
            {



                relationEnd = null;


                for (int i = BoundingBoxes.Count - 1; i >= 0; i--)
                {
                    BoundingBox bbox = BoundingBoxes[i];
                    if (bbox.Bounds.Contains(x, y))
                    {
                        if (bbox.Target is Shape)
                        {
                            relationEnd = bbox.Target as Shape;
                        }
                    }
                }

                endDrawRelation(relationStart, relationEnd);

                //end drawing
                EditMode = EditMode.Normal;
                MainCanvas.Refresh();
            }
            else if (EditMode == EditMode.Normal)
            {
                Cursor = System.Windows.Forms.Cursors.Default;
                currentBoundingBox = null;
                isPanning = false;
                for (int i = BoundingBoxes.Count - 1; i >= 0; i--)
                {
                    BoundingBox bbox = BoundingBoxes[i];
                    if (bbox.Bounds.Contains(x, y))
                    {
                        if (bbox.Target is Shape)
                        {
                            Shape shape = (Shape)bbox.Target;
                            ShapeMouseEventArgs args = new ShapeMouseEventArgs();
                            args.BoundingBox = bbox;
                            args.X = x;
                            args.Y = y;
                            args.Button = e.Button;
                            args.Sender = this;
                            args.GridSize = GridSize;
                            args.SnapToGrid = SnapToGrid;
                            shape.OnMouseUp(args);
                            if (args.Redraw)
                                this.Refresh();
                        }

                        return;
                    }
                }
            }
        }

        private Point mouseDownPoint;
        private Point mouseCurrentPoint;
        private Point mouseDownAutoscrollPoint;
        private bool isPanning = false;
        void MainCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            EndInput();
            int x = (int)((double)(e.X - MainCanvas.AutoScrollPosition.X) / Zoom);
            int y = (int)((double)(e.Y - MainCanvas.AutoScrollPosition.Y) / Zoom);

            if (EditMode == EditMode.BeginDrawRelation)
            {
                mouseDownPoint = new Point(e.X, e.Y);
                EditMode = EditMode.DrawRelation;
                relationStart = null;

                for (int i = BoundingBoxes.Count - 1; i >= 0; i--)
                {
                    BoundingBox bbox = BoundingBoxes[i];
                    if (bbox.Bounds.Contains(x, y))
                    {
                        if (bbox.Target is Shape)
                        {
                            relationStart = bbox.Target as Shape;
                        }
                    }
                }

            }
            else if (EditMode == EditMode.Normal)
            {
                mouseDownPoint = new Point(e.X, e.Y);
                mouseDownAutoscrollPoint = new Point(-MainCanvas.AutoScrollPosition.X, -MainCanvas.AutoScrollPosition.Y);

                for (int i = BoundingBoxes.Count - 1; i >= 0; i--)
                {
                    BoundingBox bbox = BoundingBoxes[i];
                    if (bbox.Bounds.Contains(x, y))
                    {
                        if (bbox.Target is Shape)
                        {
                            currentBoundingBox = bbox;
                            Shape shape = (Shape)bbox.Target;
                            currentShape = shape;
                            ShapeMouseEventArgs args = new ShapeMouseEventArgs();
                            args.BoundingBox = bbox;
                            args.X = x;
                            args.Y = y;
                            args.Button = e.Button;
                            args.Sender = this;
                            args.GridSize = GridSize;
                            args.SnapToGrid = SnapToGrid;
                            shape.OnMouseDown(args);
                            if (args.Redraw)
                                this.Refresh();
                        }

                        return;
                    }
                }
                isPanning = true;
            }
        }

        void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (EditMode == EditMode.DrawRelation)
            {
                mouseCurrentPoint = new Point(e.X, e.Y);
                MainCanvas.Refresh();
            }
            else if (EditMode == EditMode.Normal)
            {
                int x = (int)((double)(e.X - MainCanvas.AutoScrollPosition.X) / Zoom);
                int y = (int)((double)(e.Y - MainCanvas.AutoScrollPosition.Y) / Zoom);

                if (e.Button != MouseButtons.None)
                {
                    if (currentBoundingBox == null)
                    {
                        if (isPanning)
                        {
                            int dx = mouseDownPoint.X - e.X;
                            int dy = mouseDownPoint.Y - e.Y;

                            Point newPos = new Point(mouseDownAutoscrollPoint.X + dx, mouseDownAutoscrollPoint.Y + dy);
                            MainCanvas.AutoScrollPosition = newPos;
                            Cursor = System.Windows.Forms.Cursors.SizeAll;
                        }
                    }
                    else
                    {

                        Shape shape = (Shape)currentBoundingBox.Target;
                        ShapeMouseEventArgs args = new ShapeMouseEventArgs();
                        args.BoundingBox = currentBoundingBox;
                        args.X = x;
                        args.Y = y;
                        args.Button = e.Button;
                        args.Sender = this;
                        args.GridSize = GridSize;
                        args.SnapToGrid = SnapToGrid;
                        shape.OnMouseMove(args);
                        if (args.Redraw)
                            MainCanvas.Refresh();
                    }
                }
                else
                {
                    for (int i = BoundingBoxes.Count - 1; i >= 0; i--)
                    {
                        BoundingBox bbox = BoundingBoxes[i];
                        if (bbox.Bounds.Contains(x, y))
                        {
                            if (bbox.Target is Shape)
                            {
                                Shape shape = (Shape)bbox.Target;
                                ShapeMouseEventArgs args = new ShapeMouseEventArgs();
                                args.BoundingBox = bbox;
                                args.X = x;
                                args.Y = y;
                                args.Button = e.Button;
                                args.Sender = this;
                                shape.OnMouseMove(args);
                                if (args.Redraw)
                                    MainCanvas.Refresh();
                            }

                            return;
                        }
                    }
                }
            }
        }

        public virtual string GetInput()
        {
            return txtInput.Text;
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!txtInput.Multiline)
            {
                if (e.KeyChar == '\r')
                    e.Handled = true;
            }
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (!txtInput.Multiline)
                {
                    txtInput.Text = originalText;
                    EndInput();
                }
                else
                {
                    EndInput();
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (!txtInput.Multiline)
                {
                    e.Handled = true;
                    EndInput();
                }  
                else if (e.Control && txtInput.Multiline)
                {
                    e.Handled = true;
                    EndInput();
                }                              
            }
        }

        //break out to a selection class
        public virtual void ClearSelection()
        {
            foreach (Shape shape in Diagram.Shapes)
            {
                shape.Selected = false;
            }
        }
    }
}
