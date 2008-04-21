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
    public class UmlAssociation : Shape
    {
        public IUmlAssociationData DataSource { get; set; }

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


            float x1 = start.Bounds.X + start.Bounds.Width / 2;
            float y1 = start.Bounds.Y + start.Bounds.Height / 2;


            float x2 = end.Bounds.X + end.Bounds.Width / 2;
            float y2 = end.Bounds.Y + end.Bounds.Height / 2;

            Pen currentPen = Settings.Pens.DefaultBorder;

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
            Shape start = Start;
            Shape end = End;

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
