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
    public class UmlInterface : UmlInstanceType
    {
        public UmlInterface()
        {

        }

        protected override IList<UmlTypeMemberSection> GetTypeMemberSections()
        {
            return new List<UmlTypeMemberSection>()
            {
                new UmlTypeMemberSection(this,"Properties"),
                new UmlTypeMemberSection(this,"Methods")
            };
        }

        protected override Brush GetCaptionBrush(Rectangle renderBounds)
        {
            if (Selected)
                return new LinearGradientBrush(renderBounds, Color.FromArgb(190, 230, 202), Color.White, 0, true);
            else
                return new LinearGradientBrush(renderBounds, Color.FromArgb(210, 240, 222), Color.White, 0, true);
        }

        protected override string GetTypeKind()
        {
             return "Interface";
        }

        protected override Font GetTypeNameFont()
        {
             return Settings.Fonts.DefaultTypeName;
        }

        protected override Pen GetBorderPen()
        {
             return Settings.Pens.DefaultBorder;
        }
    }
}
