using System;
using System.Collections.Generic;
using System.Text;
using AopDraw.Interfaces;
using System.Drawing;
using Puzzle.NAspect.Framework.Aop;
using Puzzle.NAspect.Framework;

namespace AopDraw.Mixins
{
    public class DesignableMixin : IDesignable, IProxyAware
    {
        private IShape shape;

        public void SetProxy(IAopProxy target)
        {
            IShape shape = target as IShape;

            if (shape == null)
                throw new ArgumentException("target is not an IShape");

            this.shape = shape;
        }    

        #region Property BorderSize 
        private float borderSize = 2;
        public float BorderSize
        {
            get
            {
                return this.borderSize;
            }
            set
            {
                this.borderSize = value;
            }
        }                        
        #endregion

        #region Property BorderColor 
        private Color borderColor = Color.Black;
        public Color BorderColor
        {
            get
            {
                return this.borderColor;
            }
            set
            {
                this.borderColor = value;
            }
        }                        
        #endregion

        #region Property FillColor 
        private Color fillColor = Color.Silver;
        public Color FillColor
        {
            get
            {
                return this.fillColor;
            }
            set
            {
                this.fillColor = value;
            }
        }                        
        #endregion

        private void DirtyCanvas()
        {
            ICanvasAware canvasAware = shape as ICanvasAware;
            if (canvasAware != null)
                canvasAware.Canvas.IsDirty = true;
        }         
    }
}
