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
    public class UmlEnum : UmlInstanceType
    {
        #region TypedDataSource property
        private IUmlClassData TypedDataSource
        {
            get
            {
                return DataSource as IUmlClassData;
            }
        }
        #endregion


        public UmlEnum()
        {

        }

        protected override IList<UmlTypeMemberSection> GetTypeMemberSections()
        {
            return new List<UmlTypeMemberSection>()
            {
                new UmlTypeMemberSection(this,"Values")
            };
        }

        protected override Brush GetCaptionBrush(Rectangle renderBounds)
        {
            if (Selected)
                return new LinearGradientBrush(renderBounds, Color.FromArgb(211, 204, 229), Color.White, 0, true);
            else
                return new LinearGradientBrush(renderBounds, Color.FromArgb(221, 214, 239), Color.White, 0, true);
        }

        protected override Brush GetSectionCaptionBrush()
        {
            return Settings.Brushes.EnumSectionCaption;
        }

        protected override string GetTypeKind()
        {
            return "Enum";
        }

        protected override Font GetTypeNameFont()
        {
            return Settings.Fonts.DefaultTypeName;
        }

        protected override Pen GetBorderPen()
        {
            return Settings.Pens.DefaultBorder;
        }

        protected override int GetRadius()
        {
            return 1;
        }

        protected override Font GetTypeMemberFont()
        {
            return Settings.Fonts.ClassTypeMember;
        }
    }
}
