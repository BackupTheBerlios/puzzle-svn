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
    #region GFX Settings
    public static class Settings
    {
        private const string fontName = "Tahoma";
        public const int typeBoxSideMargin = 10;
        public const int memberNameIndent = 30;

        public static Font normalTypeNameFont = new Font(fontName, 8f, FontStyle.Bold);
        public static Font abstractTypeNameFont = new Font(fontName, 8f, FontStyle.Bold | FontStyle.Italic);
        public static Font typeKindFont = new Font(fontName, 7f, FontStyle.Regular);
        public static Font typeInheritsFont = new Font(fontName, 7f, FontStyle.Regular);
        public static Font sectionCaptionFont = new Font(fontName, 8f, FontStyle.Regular);
        public static Font memberFont = new Font(fontName, 8f, FontStyle.Regular);
        public static Font newMemberFont = new Font(fontName, 8f, FontStyle.Underline);
        public static SolidBrush sectionCaptionBrush = new SolidBrush(Color.FromArgb(240, 242, 249));
        public static SolidBrush selectedMemberBrush = new SolidBrush(SystemColors.Highlight);
        public static Pen normalBorderPen = new Pen(Color.FromArgb(100, 100, 100), 1);
        public static Pen abstractBorderPen = MakeAbstractBorderPen();
        public static Pen selectionPen1 = MakeSelectonPen();
        public static Pen selectionPen2 = new Pen(Color.FromArgb(220, 220, 220), 1);

        private static Pen MakeSelectonPen()
        {
            Pen pen = new Pen(Color.Gray, 1);
            pen.DashStyle = DashStyle.Dash;
            return pen;
        }

        private static Pen MakeAbstractBorderPen()
        {
            Pen pen = new Pen(Color.Black, 1);
            pen.DashStyle = DashStyle.Dash;
            pen.Width = 2;
            pen.Alignment = PenAlignment.Center;
            return pen;
        }
    }
    #endregion
}
