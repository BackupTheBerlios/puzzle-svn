using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AlbinoHorse.Infrastructure;
using AlbinoHorse.Windows.Forms;


namespace AlbinoHorse.Model
{
    public class UmlRelation : Shape
    {
        public IUmlRelationData DataSource { get; set; }
        protected object StartPortIdentifier = new object();
        protected object EndPortIdentifier = new object();

        #region Property Start
        public Shape Start
        {
            get
            {
                return DataSource.Start;
            }
        }
        #endregion

        #region Property End
        public Shape End
        {
            get
            {
                return DataSource.End;
            }
        }
        #endregion

        public override void Draw(RenderInfo info)
        {

        }

        public override void DrawBackground(RenderInfo info)
        {
            Shape start = Start;
            Shape end = End;

            if (start == null || end == null)
                return;

            Rectangle startBounds = start.Bounds;
            Rectangle endBounds = end.Bounds;

            



            PointF startPoint = GetPoint(startBounds, DataSource.StartPortOffset, DataSource.StartPortSide);
            PointF endPoint = GetPoint(endBounds, DataSource.EndPortOffset, DataSource.EndPortSide);

            Pen pen = null;

            if (DataSource.AssociationType == UmlRelationType.Inheritance)
                pen = Settings.Pens.InheritanceLine;
            else if (DataSource.AssociationType == UmlRelationType.None)
                pen = Settings.Pens.FakeLine;
            else
                pen = Settings.Pens.AssociationLine;

         //   DrawAssociation(info, startPoint, endPoint, Settings.Pens.AssociationBorder);
            DrawRelationBackground(info, startPoint, endPoint);
            if (Selected)
            {
                DrawRelation(info, startPoint, endPoint, Settings.Pens.AssociationBorder);
            }
            DrawRelation(info, startPoint, endPoint, pen);
            DrawPortSelectionHandles(info, DataSource.StartPortSide, startPoint);
            DrawPortSelectionHandles(info, DataSource.EndPortSide, endPoint);            
        }
        

        private void DrawRelation(RenderInfo info, PointF startPoint, PointF endPoint, Pen pen)
        {
            

            //start
            if (DataSource.AssociationType == UmlRelationType.Aggregation)
            {
                DrawAggregatePort(info, DataSource.StartPortSide, startPoint, pen);
            }
            else
            {
                DrawPort(info, DataSource.StartPortSide, startPoint, pen);
            }

            //middle            
            DrawLine(info, pen, startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);

            //end
            if (DataSource.AssociationType == UmlRelationType.Association)
                DrawArrowPort(info, DataSource.EndPortSide, endPoint, pen);
            if (DataSource.AssociationType == UmlRelationType.Aggregation)
                DrawArrowPort(info, DataSource.EndPortSide, endPoint, pen);
            if (DataSource.AssociationType == UmlRelationType.None)
                DrawPort(info, DataSource.EndPortSide, endPoint, pen);
            if (DataSource.AssociationType == UmlRelationType.Inheritance)
                DrawInheritancePort(info, DataSource.EndPortSide, endPoint, pen);
        }

        private void DrawRelationBackground(RenderInfo info, PointF startPoint, PointF endPoint)
        {
            DrawPortBackground(info, DataSource.StartPortSide, startPoint,  StartPortIdentifier);
            DrawLineBackground(info, startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
            DrawPortBackground(info, DataSource.EndPortSide, endPoint,  EndPortIdentifier);
        }

        private void DrawPortSelectionHandles(RenderInfo info, UmlPortSide portSide, PointF point)
        {
            if (Selected)
            {

                int marginSize = 15;
                if (portSide == UmlPortSide.Left)
                {
                    DrawSelectionHandle(info, new Point((int)point.X + marginSize, (int)point.Y));                    
                }

                if (portSide == UmlPortSide.Right)
                {
                    DrawSelectionHandle(info, new Point((int)point.X - marginSize, (int)point.Y));
                }

                if (portSide == UmlPortSide.Top)
                {
                    DrawSelectionHandle(info, new Point((int)point.X , (int)point.Y + marginSize));
                }

                if (portSide == UmlPortSide.Bottom)
                {
                    DrawSelectionHandle(info, new Point((int)point.X, (int)point.Y - marginSize));
                }
            }
        }

        private void DrawPortBackground(RenderInfo info, UmlPortSide portSide, PointF point, object portIdentifier)
        {
            int marginSize = 15;
            if (portSide == UmlPortSide.Left)
            {
                DrawPortSelector(info, point.X, point.Y, point.X + marginSize, point.Y, portIdentifier);                
            }

            if (portSide == UmlPortSide.Right)
            {
                DrawPortSelector(info, point.X, point.Y, point.X - marginSize, point.Y, portIdentifier);                
            }

            if (portSide == UmlPortSide.Top)
            {
                DrawPortSelector(info, point.X, point.Y, point.X, point.Y + marginSize, portIdentifier);
            }

            if (portSide == UmlPortSide.Bottom)
            {
                DrawPortSelector(info, point.X, point.Y, point.X, point.Y - marginSize,portIdentifier);
            }
        }


        private void DrawPort(RenderInfo info, UmlPortSide portSide, PointF point, Pen pen)
        {
            int marginSize = 15;
            if (portSide == UmlPortSide.Left)
            {
                info.Graphics.DrawLine(pen, point.X, point.Y, point.X + marginSize, point.Y);
            }

            if (portSide == UmlPortSide.Right)
            {
                info.Graphics.DrawLine(pen, point.X, point.Y, point.X - marginSize, point.Y);
            }

            if (portSide == UmlPortSide.Top)
            {
                info.Graphics.DrawLine(pen, point.X, point.Y, point.X, point.Y + marginSize);
            }

            if (portSide == UmlPortSide.Bottom)
            {
                info.Graphics.DrawLine(pen, point.X, point.Y, point.X, point.Y - marginSize);
            }
        }

        private void DrawArrowPort(RenderInfo info, UmlPortSide portSide, PointF point, Pen pen)
        {
            int marginSize = 15;
            int arrowSize = 5;
            if (portSide == UmlPortSide.Left)
            {
                info.Graphics.DrawLine(pen, point.X, point.Y, point.X + marginSize, point.Y);
                info.Graphics.DrawLine(pen, point.X + arrowSize, point.Y - 5, point.X + marginSize, point.Y);
                info.Graphics.DrawLine(pen, point.X + arrowSize, point.Y + 5, point.X + marginSize, point.Y);
            }

            if (portSide == UmlPortSide.Right)
            {
                info.Graphics.DrawLine(pen, point.X, point.Y, point.X - marginSize, point.Y);
                info.Graphics.DrawLine(pen, point.X - arrowSize, point.Y - 5, point.X - marginSize, point.Y);
                info.Graphics.DrawLine(pen, point.X - arrowSize, point.Y + 5, point.X - marginSize, point.Y);
            }

            if (portSide == UmlPortSide.Top)
            {
                info.Graphics.DrawLine(pen, point.X, point.Y, point.X, point.Y + marginSize);
                info.Graphics.DrawLine(pen, point.X-5, point.Y+arrowSize, point.X, point.Y + marginSize);
                info.Graphics.DrawLine(pen, point.X+5, point.Y+arrowSize, point.X, point.Y + marginSize);
            }

            if (portSide == UmlPortSide.Bottom)
            {
                info.Graphics.DrawLine(pen, point.X, point.Y, point.X, point.Y - marginSize);
                info.Graphics.DrawLine(pen, point.X - 5, point.Y - arrowSize, point.X, point.Y - marginSize);
                info.Graphics.DrawLine(pen, point.X + 5, point.Y - arrowSize, point.X, point.Y - marginSize);
            }
        }

        private void DrawInheritancePort(RenderInfo info, UmlPortSide portSide, PointF point, Pen pen)
        {
            int marginSize = 15;
            int arrowSize = 5;
            int x = (int)point.X;
            int y = (int)point.Y;
            if (portSide == UmlPortSide.Left)
            {
                
                info.Graphics.DrawLine(pen, point.X, point.Y, point.X + marginSize, point.Y);

                Point[] points = new Point[] { new Point(x + marginSize, y), new Point(x + arrowSize, y + 5), new Point(x + arrowSize, y - 5) };
                info.Graphics.FillPolygon(Brushes.White, points);
                info.Graphics.DrawPolygon(pen, points);
            }

            if (portSide == UmlPortSide.Right)
            {
                info.Graphics.DrawLine(pen, point.X, point.Y, point.X - marginSize, point.Y);

                Point[] points = new Point[] { new Point(x - marginSize, y), new Point(x - arrowSize, y + 5), new Point(x - arrowSize, y - 5) };
                info.Graphics.FillPolygon(Brushes.White, points);
                info.Graphics.DrawPolygon(pen, points);
            }

            if (portSide == UmlPortSide.Top)
            {
                info.Graphics.DrawLine(pen, point.X, point.Y, point.X, point.Y + marginSize);

                Point[] points = new Point[] { new Point(x, y + marginSize), new Point(x + 5, y + arrowSize), new Point(x - 5, y +arrowSize) };
                info.Graphics.FillPolygon(Brushes.White, points);
                info.Graphics.DrawPolygon(pen, points);
            }

            if (portSide == UmlPortSide.Bottom)
            {
                info.Graphics.DrawLine(pen, point.X, point.Y, point.X, point.Y - marginSize);
                Point[] points = new Point[] { new Point(x, y - marginSize), new Point(x + 5, y - arrowSize), new Point(x - 5, y - arrowSize) };
                info.Graphics.FillPolygon(Brushes.White, points);
                info.Graphics.DrawPolygon(pen, points);
            }
        }

        private void DrawAggregatePort(RenderInfo info, UmlPortSide portSide, PointF point, Pen pen)
        {
            int marginSize = 16;
            int x = (int)point.X;
            int y = (int)point.Y;
            if (portSide == UmlPortSide.Left)
            {

                info.Graphics.DrawLine(pen, point.X, point.Y, point.X + marginSize, point.Y);

                Point[] points = new Point[] 
                { 
                    new Point(x + marginSize, y), 
                    new Point(x + marginSize/2, y + 5), 
                    new Point(x + 0, y), 
                    new Point(x + marginSize/2, y - 5) 
                };

                info.Graphics.FillPolygon(Brushes.White, points);
                info.Graphics.DrawPolygon(pen, points);
            }

            if (portSide == UmlPortSide.Right)
            {
                info.Graphics.DrawLine(pen, point.X, point.Y, point.X - marginSize, point.Y);

                Point[] points = new Point[] 
                { 
                    new Point(x - marginSize, y), 
                    new Point(x - marginSize/2, y + 5), 
                    new Point(x - 0, y),
                    new Point(x - marginSize/2, y - 5) 
                };
                info.Graphics.FillPolygon(Brushes.White, points);
                info.Graphics.DrawPolygon(pen, points);
            }

            if (portSide == UmlPortSide.Top)
            {
                info.Graphics.DrawLine(pen, point.X, point.Y, point.X, point.Y + marginSize);

                Point[] points = new Point[] 
                { 
                    new Point(x, y + marginSize), 
                    new Point(x + 5, y + marginSize/2), 
                    new Point(x, y ), 
                    new Point(x - 5, y + marginSize/2) 
                };

                info.Graphics.FillPolygon(Brushes.White, points);
                info.Graphics.DrawPolygon(pen, points);
            }

            if (portSide == UmlPortSide.Bottom)
            {
                info.Graphics.DrawLine(pen, point.X, point.Y, point.X, point.Y - marginSize);

                Point[] points = new Point[] 
                { 
                    new Point(x, y - marginSize), 
                    new Point(x + 5, y - marginSize/2), 
                    new Point(x, y ), 
                    new Point(x - 5, y - marginSize/2) 
                };

                info.Graphics.FillPolygon(Brushes.White, points);
                info.Graphics.DrawPolygon(pen, points);
            }
        }

        //private static void DrawLine(RenderInfo info, PointF startPoint, PointF endPoint, Pen pen)
        //{
        //    info.Graphics.DrawLine(pen, startPoint, endPoint);
        //}

        private PointF GetPoint(Rectangle bounds, int portOffset, UmlPortSide portSide)
        {
            int portCount = 4;
            int marginSize = 20;

            PointF point = new PointF();
            if (portSide == UmlPortSide.Left)
            {
                point.X = bounds.Left - marginSize;
                point.Y = bounds.Top + portOffset;
            }
            else if (portSide == UmlPortSide.Right)
            {
                point.X = bounds.Right + marginSize;
                point.Y = bounds.Top + portOffset;
            }
            else if (portSide == UmlPortSide.Top)
            {
                point.X = bounds.Left + portOffset;
                point.Y = bounds.Top - marginSize;
            }
            else if (portSide == UmlPortSide.Bottom)
            {
                point.X = bounds.Left + portOffset;
                point.Y = bounds.Bottom + marginSize;
            }
            else
            {
                throw new NotSupportedException();
            }

            return point;
        }

        public override void PreviewDrawBackground(RenderInfo info)
        {
            Shape start = Start;
            Shape end = End;

            if (start == null || end == null)
                return;

            Rectangle startBounds = start.Bounds;
            Rectangle endBounds = end.Bounds;

            float x1 = startBounds.X + startBounds.Width / 2;
            float y1 = startBounds.Y + startBounds.Height / 2;


            float x2 = endBounds.X + endBounds.Width / 2;
            float y2 = endBounds.Y + endBounds.Height / 2;

            info.Graphics.DrawLine(Pens.DarkGray, x1, y1, x2, y2);
        }

        private void DrawLine(RenderInfo info,Pen pen, float x1,float y1,float x2,float y2)
        {
            //info.Graphics.DrawLine(pen, x1, y1, x2, y2);
            if (Math.Abs(x2 - x1) > Math.Abs(y2 - y1))
            {
                float x3 = (x1 + x2) / 2;

                DrawStraightLine(info, pen, x1, y1, x3, y1);
                DrawStraightLine(info, pen, x2, y2, x3, y2);
                DrawStraightLine(info, pen, x3, y1, x3, y2);
            }
            else
            {
                float y3 = (y1 + y2) / 2;

                DrawStraightLine(info,pen, x1, y1, x1, y3);
                DrawStraightLine(info, pen, x2, y2, x2, y3);
                DrawStraightLine(info, pen, x1, y3, x2, y3);
            }
        }

        private void DrawLineBackground(RenderInfo info, float x1, float y1, float x2, float y2)
        {
            //info.Graphics.DrawLine(pen, x1, y1, x2, y2);
            if (Math.Abs(x2 - x1) > Math.Abs(y2 - y1))
            {
                float x3 = (x1 + x2) / 2;

                DrawStraightLineSelector(info,  x1, y1, x3, y1);
                DrawStraightLineSelector(info,  x2, y2, x3, y2);
                DrawStraightLineSelector(info,  x3, y1, x3, y2);
            }
            else
            {
                float y3 = (y1 + y2) / 2;

                DrawStraightLineSelector(info,  x1, y1, x1, y3);
                DrawStraightLineSelector(info,  x2, y2, x2, y3);
                DrawStraightLineSelector(info,  x1, y3, x2, y3);
            }
        }

        private void DrawPortSelector(RenderInfo info, float x1, float y1, float x2, float y2,object portIdentifier)
        {
            #region Add BBox
            BoundingBox bbox = new BoundingBox();
            bbox.Target = this;
            bbox.Data = portIdentifier;
            Rectangle tmp = new Rectangle((int)Math.Min(x1, x2), (int)Math.Min(y1, y2), (int)Math.Abs(x2 - x1), (int)Math.Abs(y2 - y1));

            tmp.Inflate(6, 6);

            bbox.Bounds = tmp;
            info.BoundingBoxes.Add(bbox);
            #endregion

            if (Selected)
            {
                tmp.Inflate(-2, -2);
                info.Graphics.FillRectangle(Settings.Brushes.SelectedRelation, tmp);
            }
        }

        private void DrawStraightLineSelector(RenderInfo info, float x1, float y1, float x2, float y2)
        {
            #region Add BBox
            BoundingBox bbox = new BoundingBox();
            bbox.Target = this;
            bbox.Data = this;
            Rectangle tmp = new Rectangle((int)Math.Min(x1, x2), (int)Math.Min(y1, y2), (int)Math.Abs(x2 - x1), (int)Math.Abs(y2 - y1));
            tmp.Inflate(6, 6);
            bbox.Bounds = tmp;
            info.BoundingBoxes.Add(bbox);
            #endregion

            if (Selected)
            {
                tmp.Inflate(-2, -2);
                info.Graphics.FillRectangle(Settings.Brushes.SelectedRelation, tmp);
            }
        }

        private void DrawStraightLine(RenderInfo info,Pen pen, float x1, float y1, float x2, float y2)
        {
            info.Graphics.DrawLine(pen, x1, y1, x2, y2);
        }

        Point mouseDownPoint;
        public override void OnMouseDown(ShapeMouseEventArgs args)
        {
            args.Sender.ClearSelection();
            this.Selected = true;
            args.Redraw = true;

            mouseDownPoint = new Point(args.X, args.Y);
            
            
        }

        public override void OnMouseMove(ShapeMouseEventArgs args)
        {
            if (args.BoundingBox.Data == StartPortIdentifier && args.Button == MouseButtons.Left)
            {
                Rectangle bounds = DataSource.Start.Bounds;

                int offset = 0;
                UmlPortSide side = DataSource.StartPortSide;

                MovePort(args, bounds, ref offset, ref side);

                DataSource.StartPortSide = side;
                DataSource.StartPortOffset = offset;
                args.Redraw = true;
            }  

            if (args.BoundingBox.Data == EndPortIdentifier && args.Button == MouseButtons.Left)
            {
                Rectangle bounds = DataSource.End.Bounds;

                int offset = 0;
                UmlPortSide side = DataSource.EndPortSide;

                MovePort(args, bounds, ref offset, ref side);

                DataSource.EndPortSide = side;
                DataSource.EndPortOffset = offset;
                args.Redraw = true;
            }            
        }

        private static void MovePort(ShapeMouseEventArgs args, Rectangle bounds, ref int offset, ref UmlPortSide side)
        {
            int topDist = Math.Abs(bounds.Top - args.Y);
            int leftDist = Math.Abs(bounds.Left - args.X);
            int bottomDist = Math.Abs(bounds.Bottom - args.Y);
            int rightDist = Math.Abs(bounds.Right - args.X);

            if (topDist <= leftDist && topDist <= rightDist && topDist <= bottomDist)
            {
                side = UmlPortSide.Top;
                offset = args.X - bounds.Left;

                if (args.X < bounds.Left)
                    offset = 0;

                if (args.X > bounds.Right)
                    offset = bounds.Width;
            }

            if (leftDist <= topDist && leftDist <= rightDist && leftDist <= bottomDist)
            {
                side = UmlPortSide.Left;
                offset = args.Y - bounds.Top;

                if (args.Y < bounds.Top)
                    offset = 0;

                if (args.Y > bounds.Bottom)
                    offset = bounds.Height;
            }

            if (bottomDist <= leftDist && bottomDist <= rightDist && bottomDist <= topDist)
            {
                side = UmlPortSide.Bottom;
                offset = args.X - bounds.Left;

                if (args.X < bounds.Left)
                    offset = 0;

                if (args.X > bounds.Right)
                    offset = bounds.Width;
            }


            if (rightDist <= topDist && rightDist <= leftDist && rightDist <= bottomDist)
            {
                side = UmlPortSide.Right;
                offset = args.Y - bounds.Top;

                if (args.Y < bounds.Top)
                    offset = 0;

                if (args.Y > bounds.Bottom)
                    offset = bounds.Height;
            }
        }
    }
}
