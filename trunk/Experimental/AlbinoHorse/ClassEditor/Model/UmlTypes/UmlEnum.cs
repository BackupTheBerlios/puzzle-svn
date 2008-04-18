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
    public class UmlEnum : UmlType
    {
        protected override int DrawExpandedBody(RenderInfo info, int x, int width, int currentY)
        {
            throw new NotImplementedException();
        }

        protected override Brush GetCaptionBrush(Rectangle renderBounds)
        {
            throw new NotImplementedException();
        }

        protected override string GetTypeKind()
        {
            return "Enum";
        }
    }
}
