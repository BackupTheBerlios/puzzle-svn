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
    public class UmlClass : UmlInstanceType
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

        #region IsAbstract property
        public bool IsAbstract
        {
            get
            {
                return TypedDataSource.IsAbstract;
            }
            set
            {
                TypedDataSource.IsAbstract = value;
            }
        }
        #endregion

        #region InheritsTypeName property
        public string InheritsTypeName
        {
            get
            {
                return TypedDataSource.InheritsTypeName;
            }
            set
            {
                TypedDataSource.InheritsTypeName = value;
            }
        }
        #endregion

        public UmlClass()
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
                return new LinearGradientBrush(renderBounds, Color.FromArgb(190, 202, 230), Color.White, 0, true);
            else
                return new LinearGradientBrush(renderBounds, Color.FromArgb(210, 222, 240), Color.White, 0, true);
        }

        protected override string GetTypeKind()
        {
            if (IsAbstract)
                return "Abstract class";
            else
                return "Class";
        }

        protected override Font GetTypeNameFont()
        {
            if (IsAbstract)
                return Settings.Fonts.AbstractTypeName;
            else
                return Settings.Fonts.DefaultTypeName;
        }

        protected override Pen GetBorderPen()
        {
            if (IsAbstract)
                return Settings.Pens.AbstractBorder;
            else
                return Settings.Pens.DefaultBorder;
        }



        protected override void DrawCustomCaptionInfo(RenderInfo info, int x, int y, int width)
        {
            if (InheritsTypeName != null)
            {
                info.Graphics.DrawImage(global::AlbinoHorse.ClassDesigner.Properties.Resources.InheritanceArrow, x + Settings.Margins.typeBoxSideMargin, y + 35);
                Rectangle typeInheritsBounds = new Rectangle(x + 24, y + 33, width - 26, 10);
                info.Graphics.DrawString(InheritsTypeName, Settings.Fonts.InheritsTypeName, Brushes.Black, typeInheritsBounds, StringFormat.GenericTypographic);
            }
        }

        protected override Brush GetSectionCaptionBrush()
        {
            return Settings.Brushes.ClassSectionCaption;
        }
    }
}
