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
    public abstract class UmlType : Shape
    {
        protected Point mouseDownPos;
        protected Point mouseDownShapePos;

        #region Properties

        #region DataSource property
        public IUmlTypeData DataSource { get; set; }
        #endregion

        #region TypeName property
        public string TypeName
        {
            get
            {
                return DataSource.TypeName;
            }
            set
            {
                DataSource.TypeName = value;
            }
        }

        #endregion

        #region Expanded property
        public bool Expanded
        {
            get
            {
                return DataSource.Expanded;
            }
            set
            {
                DataSource.Expanded = value;
            }
        }
        #endregion

        #region SelectedObject property
        private object selectedObject;
        public object SelectedObject
        {
            get
            {
                return selectedObject;
            }

            set
            {
                selectedObject = value;
                OnSelectedObjectChanged(EventArgs.Empty);
            }
        }
        #endregion

        #endregion

        #region Identifiers
        //bounding box identifiers
        protected readonly object CaptionIdentifier = new object();
        protected readonly object TypeExpanderIdentifier = new object();
        protected readonly object LeftResizeIdentifier = new object();
        protected readonly object RightResizeIdentifier = new object();
        protected readonly object BodyIdentifier = new object();
        #endregion

        #region Draw
        public override void DrawPreview(RenderInfo info)
        {
            info.Graphics.FillRectangle(Brushes.White, this.Bounds);
            info.Graphics.DrawRectangle(Pens.Black, this.Bounds);
        }

        public override void DrawBackground(RenderInfo info)
        {
            int grid = info.GridSize;
            Rectangle renderBounds = Bounds;
           
            int x = renderBounds.X + 4;
            int y = renderBounds.Y + 3;
            int radius = 16;
            int width = renderBounds.Width;
            int height = renderBounds.Height;

            GraphicsPath shadowPath = GetOutlinePath(radius, x, y, width, height); 

            try
            {
                info.Graphics.FillPath(Brushes.LightGray, shadowPath);
            }
            catch
            {
            }
        }

        protected GraphicsPath GetOutlinePath(int radius, int x, int y, int width, int height)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(x + radius, y, x + width - radius, y);
            path.AddArc(x + width - radius, y, radius, radius, 270, 90);
            path.AddLine(x + width, y + radius, x + width, y + height - radius);
            path.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
            path.AddLine(x + width - radius, y + height, x + radius, y + height);
            path.AddArc(x, y + height - radius, radius, radius, 90, 90);
            path.AddLine(x, y + height - radius, x, y + radius);
            path.AddArc(x, y, radius, radius, 180, 90);

            path.CloseFigure();

            return path;
        }
        
        protected void DrawTypeExpander(RenderInfo info, int x, int y, int width)
        {
            Rectangle typeExpanderBounds = new Rectangle(x + width - 20, y + 6, 13, 13);

            #region add type expander bbox
            BoundingBox bboxTypeExpander = new BoundingBox();
            bboxTypeExpander.Target = this;
            bboxTypeExpander.Bounds = typeExpanderBounds;
            bboxTypeExpander.Data = this.TypeExpanderIdentifier;
            info.BoundingBoxes.Add(bboxTypeExpander);
            #endregion

            if (Expanded)
                info.Graphics.DrawImage(global::AlbinoHorse.ClassDesigner.Properties.Resources.Collapse, typeExpanderBounds);
            else
                info.Graphics.DrawImage(global::AlbinoHorse.ClassDesigner.Properties.Resources.Expand, typeExpanderBounds);
        }

        protected virtual void DrawExpanded(RenderInfo info, GraphicsPath path, int x, int y, int width, int height, LinearGradientBrush captionBrush, Pen borderPen)
        {
            int currentY = y + DrawExpandedCaption(info, path, x, y, width, height, captionBrush);

            currentY = DrawExpandedBody(info, x, width, currentY);

            info.Graphics.DrawPath(borderPen, path);
        }

        protected abstract int DrawExpandedBody(RenderInfo info, int x, int width, int currentY);        

        protected int DrawExpandedCaption(RenderInfo info, GraphicsPath path, int x, int y, int width, int height, Brush captionBrush)
        {
            int captionHeight = 48;
            Rectangle captionBounds = new Rectangle(x, y, width, captionHeight);
            #region add caption bbox
            BoundingBox bboxCaption = new BoundingBox();
            bboxCaption.Bounds = captionBounds;
            bboxCaption.Target = this;
            bboxCaption.Data = this.CaptionIdentifier;
            info.BoundingBoxes.Add(bboxCaption);
            #endregion

            info.Graphics.SetClip(path);
            info.Graphics.FillRectangle(captionBrush, captionBounds);
            info.Graphics.FillRectangle(Brushes.White, x, y + captionHeight, width, height - captionHeight);
            info.Graphics.DrawLine(Pens.LightGray, x, y + captionHeight, x + width, y + captionHeight);
            info.Graphics.ResetClip();
            return captionHeight;
        }

        protected void DrawCollapsed(RenderInfo info, GraphicsPath path, int x, int y, int width, int height, Brush captionBrush, Pen borderPen)
        {
            Rectangle captionBounds = new Rectangle(x, y, width, height);

            BoundingBox bboxCaption = new BoundingBox();
            bboxCaption.Bounds = captionBounds;
            bboxCaption.Target = this;
            bboxCaption.Data = this.CaptionIdentifier;
            info.BoundingBoxes.Add(bboxCaption);

            info.Graphics.FillPath(captionBrush, path);
            info.Graphics.DrawPath(borderPen, path);
        }

        protected void DrawSelection(RenderInfo info)
        {
            if (Selected && SelectedObject == null)
            {
                Rectangle outerBounds = this.Bounds;
                outerBounds.Inflate(4, 4);
                outerBounds.Offset(1, 0);
                info.Graphics.DrawRectangle(Settings.selectionPen2, outerBounds);
                outerBounds.Offset(-1, 1);
                info.Graphics.DrawRectangle(Settings.selectionPen1, outerBounds);

                Rectangle leftHandle = new Rectangle(outerBounds.X - 4, (outerBounds.Top + outerBounds.Bottom) / 2 - 4, 8, 8);
                info.Graphics.FillRectangle(Brushes.Gray, leftHandle);
                leftHandle.Inflate(-1, -1);
                info.Graphics.FillRectangle(Brushes.White, leftHandle);

                BoundingBox leftResizeHandle = new BoundingBox();
                leftResizeHandle.Bounds = leftHandle;
                leftResizeHandle.Data = this.LeftResizeIdentifier;
                leftResizeHandle.Target = this;
                info.BoundingBoxes.Add(leftResizeHandle);

                Rectangle rightHandle = new Rectangle(outerBounds.Right - 4, (outerBounds.Top + outerBounds.Bottom) / 2 - 4, 8, 8);
                info.Graphics.FillRectangle(Brushes.Gray, rightHandle);
                rightHandle.Inflate(-1, -1);
                info.Graphics.FillRectangle(Brushes.White, rightHandle);

                BoundingBox rightResizeHandle = new BoundingBox();
                rightResizeHandle.Bounds = rightHandle;
                rightResizeHandle.Data = this.RightResizeIdentifier;
                rightResizeHandle.Target = this;
                info.BoundingBoxes.Add(rightResizeHandle);
            }
        }
        #endregion

        protected virtual void OnSelectedObjectChanged(EventArgs eventArgs)
        {

        }
    }
}
