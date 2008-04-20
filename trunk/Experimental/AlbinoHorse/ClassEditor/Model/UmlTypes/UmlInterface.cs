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
        #region TypedDataSource property
        private IUmlInterfaceData TypedDataSource
        {
            get
            {
                return DataSource as IUmlInterfaceData;
            }
        }
        #endregion

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
                return new LinearGradientBrush(renderBounds, Color.FromArgb(220, 230, 209), Color.White, 0, true);
            else
                return new LinearGradientBrush(renderBounds, Color.FromArgb(230, 240, 219), Color.White, 0, true);
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

        protected override Brush GetSectionCaptionBrush()
        {
            return Settings.Brushes.InterfaceSectionCaption;
        }
    }
}
