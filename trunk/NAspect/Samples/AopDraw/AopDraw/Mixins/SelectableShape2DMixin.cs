using System;
using System.Collections.Generic;
using System.Text;
using AopDraw.Interfaces;
using System.Windows.Forms;
using AopDraw.Classes;
using Puzzle.NAspect.Framework.Aop;
using System.Drawing;
using Puzzle.NAspect.Framework;

namespace AopDraw.Mixins
{
    public class SelectableShape2DMixin : ISelectable , IProxyAware
    {
        private IShape2D shape;

        public void SetProxy(IAopProxy target)
        {
            IShape2D shape = target as IShape2D;

            if (shape == null)
                throw new ArgumentException("target is not an IShape2D");

            this.shape = shape;
        }    

        #region Property IsSelected 
        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                this.isSelected = value;
            }
        }                        
        #endregion

        public void RenderSelection(CanvasPaintArgs e)
        {
            Rectangle obounds = shape.GetBounds();
            Rectangle ibounds = shape.GetBounds();
            obounds.Inflate(3, 3);
            ibounds.Inflate(-3, -3);

            ControlPaint.DrawSelectionFrame(e.g, true, obounds, ibounds,Color.Transparent);
        }
    }
}
