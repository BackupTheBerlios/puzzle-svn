﻿using System;
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
        #endregion

        protected virtual void OnSelectedObjectChanged(EventArgs eventArgs)
        {

        }
    }
}
