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

        //#region Property ScreenBounds 
        //private Rectangle screenBounds;
        //public virtual Rectangle ScreenBounds
        //{
        //    get
        //    {
        //        return this.screenBounds;
        //    }
        //    set
        //    {
        //        this.screenBounds = value;
        //    }
        //}                        
        //#endregion

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
        public virtual void PreviewDraw(RenderInfo info) { }
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

    }
}
