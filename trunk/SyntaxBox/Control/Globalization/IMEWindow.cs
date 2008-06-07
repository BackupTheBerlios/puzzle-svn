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
using System.Drawing;
using Puzzle.Windows;

namespace Puzzle.Globalization
{
    public class IMEWindow
    {
        private const int CFS_POINT = 0x0002;
        private const byte FF_MODERN = 48;
        private const byte FIXED_PITCH = 1;
        private const int IMC_SETCOMPOSITIONFONT = 0x000a;
        private const int IMC_SETCOMPOSITIONWINDOW = 0x000c;
        private readonly IntPtr hIMEWnd;

        #region ctor

        public IMEWindow(IntPtr hWnd, string fontname, float fontsize)
        {
            hIMEWnd = NativeMethods.ImmGetDefaultIMEWnd(hWnd);
            SetFont(fontname, fontsize);
        }

        #endregion

        #region PUBLIC PROPERTY FONT

        private Font _Font;

        public Font Font
        {
            get { return _Font; }
            set
            {
                if (_Font.Equals(value) == false)
                {
                    SetFont(value);
                    _Font = value;
                }
            }
        }

        public void SetFont(Font font)
        {
            var lf = new LogFont();
            font.ToLogFont(lf);
            lf.lfPitchAndFamily = FIXED_PITCH | FF_MODERN;

            NativeMethods.SendMessage(
                hIMEWnd, (int) WindowMessage.WM_IME_CONTROL,
                IMC_SETCOMPOSITIONFONT,
                lf
                );
        }

        public void SetFont(string fontname, float fontsize)
        {
            var tFont = new LogFont();
            tFont.lfItalic = 0;
            tFont.lfStrikeOut = 0;
            tFont.lfUnderline = 0;
            tFont.lfWeight = 400;
            tFont.lfWidth = 0;
            tFont.lfHeight = (int) (-fontsize*1.3333333333333);
            tFont.lfCharSet = 1;
            tFont.lfPitchAndFamily = FIXED_PITCH | FF_MODERN;
            tFont.lfFaceName = fontname;

            LogFont lf = tFont;

            NativeMethods.SendMessage(
                hIMEWnd, (int) WindowMessage.WM_IME_CONTROL,
                IMC_SETCOMPOSITIONFONT,
                lf
                );
        }

        #endregion

        #region PUBLIC PROPERTY LOATION

        private Point _Loation;

        public Point Loation
        {
            get { return _Loation; }
            set
            {
                _Loation = value;

                var p = new APIPoint();
                p.x = value.X;
                p.y = value.Y;

                var lParam = new COMPOSITIONFORM();
                lParam.dwStyle = CFS_POINT;
                lParam.ptCurrentPos = p;
                lParam.rcArea = new APIRect();

                NativeMethods.SendMessage(
                    hIMEWnd,
                    (int) WindowMessage.WM_IME_CONTROL,
                    IMC_SETCOMPOSITIONWINDOW,
                    lParam
                    );
            }
        }

        #endregion
    }
}