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

namespace Puzzle.SourceCode
{
    /// <summary>
    /// Format ranges can be applied to a syntaxdocument to mark sections such as breakpoints or errors
    /// </summary>
    public class FormatRange
    {
        #region PUBLIC PROPERTY BACKCOLOR

        private Color _BackColor = Color.Empty;

        public Color BackColor
        {
            get { return _BackColor; }
            set
            {
                _BackColor = value;
                Apply();
            }
        }

        #endregion

        #region PUBLIC PROPERTY FORECOLOR

        private Color _ForeColor = Color.Empty;

        public Color ForeColor
        {
            get { return _ForeColor; }
            set
            {
                _ForeColor = value;
                Apply();
            }
        }

        #endregion

        #region PUBLIC PROPERTY WAVECOLOR

        private Color _WaveColor = Color.Empty;

        public Color WaveColor
        {
            get { return _WaveColor; }
            set
            {
                _WaveColor = value;
                Apply();
            }
        }

        #endregion

        #region PUBLIC PROPERTY INFOTIP

        private string _InfoTip = "";

        public string InfoTip
        {
            get { return _InfoTip; }
            set
            {
                _InfoTip = value;
                Apply();
            }
        }

        #endregion

        #region PUBLIC PROPERTY BOUNDS

        private readonly TextRange _OldBounds = new TextRange();
        private TextRange _Bounds;

        public TextRange Bounds
        {
            get { return _Bounds; }
            set { _Bounds = value; }
        }

        #endregion

        #region PUBLIC PROPERTY TAG

        public object Tag { get; set; }

        #endregion

        #region PUBLIC PROPERTY DOCUMENT

        internal SyntaxDocument Document { get; set; }

        #endregion

        public FormatRange()
        {
            Bounds = new TextRange();
            Bounds.Change += BoundsChanged;
        }

        public FormatRange(TextRange Bounds, Color ForeColor, Color BackColor)
        {
            this.BackColor = BackColor;
            this.ForeColor = ForeColor;
            this.Bounds = Bounds;
            this.Bounds.Change += BoundsChanged;
        }

        public FormatRange(TextRange Bounds, Color ForeColor, Color BackColor,
                           Color WaveColor)
        {
            this.BackColor = BackColor;
            this.ForeColor = ForeColor;
            this.WaveColor = WaveColor;
            this.Bounds = Bounds;
            this.Bounds.Change += BoundsChanged;
        }

        public FormatRange(TextRange Bounds, Color WaveColor)
        {
            this.WaveColor = WaveColor;
            this.Bounds = Bounds;
            this.Bounds.Change += BoundsChanged;
        }

        public int Contains(TextPoint tp)
        {
            return Contains(tp.X, tp.Y);
        }

        public int Contains(int x, int y)
        {
            if (y < Bounds.FirstRow)
                return - 1;

            if (y > Bounds.LastRow)
                return 1;

            if (y == Bounds.FirstRow && x < Bounds.FirstColumn)
                return - 1;

            if (y == Bounds.LastRow && x > Bounds.LastColumn)
                return 1;

            return 0;
        }

        public int Contains2(TextPoint tp)
        {
            return Contains2(tp.X, tp.Y);
        }

        public int Contains2(int x, int y)
        {
            if (y < Bounds.FirstRow)
                return - 1;

            if (y > Bounds.LastRow)
                return 1;

            if (y == Bounds.FirstRow && x <= Bounds.FirstColumn)
                return - 1;

            if (y == Bounds.LastRow && x > Bounds.LastColumn)
                return 1;

            return 0;
        }

        public void Apply()
        {
            if (Document == null)
                return;

            ApplyOld();
            for (int i = Bounds.FirstRow; i <= Bounds.LastRow; i++)
            {
                if (i < 0 || i >= Document.Count)
                    return;

                Row r = Document[i];

                if (r == null)
                    return;

                if (r.RowState == RowState.AllParsed)
                {
                    r.AddToParseQueue();
                }
            }
        }

        public void ApplyOld()
        {
            for (int i = _OldBounds.FirstRow; i <= _OldBounds.LastRow; i++)
            {
                if (i < 0 || i >= Document.Count)
                    return;

                Row r = Document[i];

                if (r == null)
                    return;

                if (r.RowState == RowState.AllParsed)
                {
                    r.AddToParseQueue();
                }
            }
        }

        protected virtual void BoundsChanged(object s, EventArgs e)
        {
            Apply();
            _OldBounds.FirstColumn = _Bounds.FirstColumn;
            _OldBounds.LastColumn = _Bounds.LastColumn;
            _OldBounds.FirstRow = _Bounds.FirstRow;
            _OldBounds.LastRow = _Bounds.LastRow;
        }
    }
}