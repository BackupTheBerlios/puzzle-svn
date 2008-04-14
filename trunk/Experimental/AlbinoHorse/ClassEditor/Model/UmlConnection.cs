using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AlbinoHorse.Infrastructure;

namespace AlbinoHorse.Model
{
    public class UmlConnection : Shape
    {
        private static class Settings
        {
            public static Pen DefaultPen = new Pen(Color.Gray, 1);
        }

        #region Property Start
        private Shape start;
        public Shape Start
        {
            get
            {
                return this.start;
            }
            set
            {
                this.start = value;
            }
        }
        #endregion

        #region Property End
        private Shape end;
        public Shape End
        {
            get
            {
                return this.end;
            }
            set
            {
                this.end = value;
            }
        }
        #endregion

        public Pen Pen { get; set; }

        public override void Draw(RenderInfo info)
        {
            
        }

        public override void DrawBackground(RenderInfo info)
        {
            if (start == null || end == null)
                return;
            

            float x1 = start.Bounds.X + start.Bounds.Width / 2;
            float y1 = start.Bounds.Y + start.Bounds.Height / 2;


            float x2 = end.Bounds.X + end.Bounds.Width / 2;
            float y2 = end.Bounds.Y + end.Bounds.Height / 2;

            Pen currentPen = null;
            if (Pen == null)
                currentPen = Settings.DefaultPen;
            else
                currentPen = Pen;

            if (Math.Abs(x2 - x1) > Math.Abs(y2 - y1))
            {
                float x3 = (x1 + x2) / 2;

                info.Graphics.DrawLine(currentPen, x1, y1, x3, y1);
                info.Graphics.DrawLine(currentPen, x2, y2, x3, y2);
                info.Graphics.DrawLine(currentPen, x3, y1, x3, y2);
            }
            else
            {
                float y3 = (y1 + y2) / 2;

                if (start.Bounds.Top < end.Bounds.Top)
                {
                    y3 = (start.Bounds.Bottom + end.Bounds.Top) / 2;
                }
                else
                {
                    y3 = (end.Bounds.Bottom + start.Bounds.Top) / 2;
                }

                info.Graphics.DrawLine(currentPen, x1, y1, x1, y3);
                info.Graphics.DrawLine(currentPen, x2, y2, x2, y3);
                info.Graphics.DrawLine(currentPen, x1, y3, x2, y3);
            }

        //    info.Graphics.DrawLine(Pens.Gray, x1, y1, x2, y2);
        }

        public override void PreviewDrawBackground(RenderInfo info)
        {
            if (start == null || end == null)
                return;

            float x1 = start.Bounds.X + start.Bounds.Width / 2;
            float y1 = start.Bounds.Y + start.Bounds.Height / 2;


            float x2 = end.Bounds.X + end.Bounds.Width / 2;
            float y2 = end.Bounds.Y + end.Bounds.Height / 2;

            info.Graphics.DrawLine(Pens.DarkGray, x1, y1, x2, y2);
        }
    }
}
