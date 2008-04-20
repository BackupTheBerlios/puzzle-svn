using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AlbinoHorse.Infrastructure;
using AlbinoHorse.Windows.Forms;


namespace AlbinoHorse.Model.Settings
{
    #region GFX Settings

    public static class Margins
    {
        public const int typeBoxSideMargin = 10;
        public const int TypeMemberNameIndent = 30;
    }

    public static class Brushes
    {
        public static SolidBrush InterfaceSectionCaption = new SolidBrush(Color.FromArgb(243, 247, 240));
        public static SolidBrush ClassSectionCaption = new SolidBrush(Color.FromArgb(240, 242, 249));
        public static SolidBrush SelectedTypeMember = new SolidBrush(SystemColors.Highlight);
        public static SolidBrush Shadow = new SolidBrush(Color.LightGray);
    }

    public static class Fonts
    {
        private const string fontName = "Tahoma";

        public static Font DefaultTypeName = new Font(fontName, 8f, FontStyle.Bold);
        public static Font AbstractTypeName = new Font(fontName, 8f, FontStyle.Bold | FontStyle.Italic);
        public static Font TypeKind = new Font(fontName, 7f, FontStyle.Regular);
        public static Font InheritsTypeName = new Font(fontName, 7f, FontStyle.Regular);
        public static Font SectionCaption = new Font(fontName, 8f, FontStyle.Regular);
        public static Font TypeMember = new Font(fontName, 8f, FontStyle.Regular);
        public static Font NewTypeMember = new Font(fontName, 8f, FontStyle.Underline);
    }


    public static class Pens
    {
        public static Pen DefaultBorder = new Pen(Color.FromArgb(90, 90, 90), 1f);
        public static Pen AbstractBorder = MakeAbstractBorderPen();
        public static Pen SelectionOuter = MakeSelectonPen();
        public static Pen SelectionInner = new Pen(Color.FromArgb(220, 220, 220), 1);

        private static Pen MakeSelectonPen()
        {
            Pen pen = new Pen(Color.Gray, 1);
            pen.DashStyle = DashStyle.Dash;
            return pen;
        }

        private static Pen MakeAbstractBorderPen()
        {

            Pen pen = new Pen(Color.FromArgb(90, 90, 90), 1.3f);
            pen.DashStyle = DashStyle.Dash;
            pen.Alignment = PenAlignment.Center;
            return pen;
        }
    }
    #endregion
}
