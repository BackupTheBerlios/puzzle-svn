// *
// * Copyright (C) 2005 Roger Alsing : http://www.puzzleframework.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System;
using Puzzle.Windows;

namespace Puzzle.Drawing.GDI
{
    public class GDIFont : GDIObject
    {
        public bool Bold;
        public byte Charset;
        public string FontName;
        public IntPtr hFont;
        public bool Italic;
        public float Size;
        public bool Strikethrough;
        public bool Underline;


        public GDIFont()
        {
            Create();
        }

        public GDIFont(string fontname, float size)
        {
            Init(fontname, size, false, false, false, false);
            Create();
        }

        public GDIFont(string fontname, float size, bool bold, bool italic, bool underline, bool strikethrough)
        {
            Init(fontname, size, bold, italic, underline, strikethrough);
            Create();
        }

        protected void Init(string fontname, float size, bool bold, bool italic, bool underline, bool strikethrough)
        {
            FontName = fontname;
            Size = size;
            Bold = bold;
            Italic = italic;
            Underline = underline;
            Strikethrough = strikethrough;

            var tFont = new LogFont();
            tFont.lfItalic = (byte) (Italic ? 1 : 0);
            tFont.lfStrikeOut = (byte) (Strikethrough ? 1 : 0);
            tFont.lfUnderline = (byte) (Underline ? 1 : 0);
            tFont.lfWeight = Bold ? 700 : 400;
            tFont.lfWidth = 0;
            tFont.lfHeight = (int) (-Size*1.3333333333333);
            tFont.lfCharSet = 1;

            tFont.lfFaceName = FontName;


            hFont = NativeMethods.CreateFontIndirect(tFont);
        }

        ~GDIFont()
        {
            Destroy();
        }

        protected override void Destroy()
        {
            if (hFont != (IntPtr) 0)
                NativeMethods.DeleteObject(hFont);
            base.Destroy();
            hFont = (IntPtr) 0;
        }

        protected override void Create()
        {
            base.Create();
        }
    }
}