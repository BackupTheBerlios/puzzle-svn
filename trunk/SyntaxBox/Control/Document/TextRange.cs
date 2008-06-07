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

namespace Puzzle.SourceCode
{
    /// <summary>
    /// A range of text
    /// </summary>
    public class TextRange
    {
        public TextRange() {}

        public TextRange(int FirstColumn, int FirstRow, int LastColumn, int LastRow)
        {
            _FirstColumn = FirstColumn;
            _FirstRow = FirstRow;
            _LastColumn = LastColumn;
            _LastRow = LastRow;
        }

        public event EventHandler Change = null;

        protected virtual void OnChange()
        {
            if (Change != null)
                Change(this, EventArgs.Empty);
        }

        /// <summary>
        /// The start row of the range
        /// </summary>

        /// <summary>
        /// The start column of the range
        /// </summary>

        /// <summary>
        /// The end row of the range
        /// </summary>

        /// <summary>
        /// The end column of the range
        /// </summary>

        public void SetBounds(int FirstColumn, int FirstRow, int LastColumn, int LastRow)
        {
            _FirstColumn = FirstColumn;
            _FirstRow = FirstRow;
            _LastColumn = LastColumn;
            _LastRow = LastRow;
            OnChange();
        }

        #region PUBLIC PROPERTY FIRSTROW

        private int _FirstRow;

        public int FirstRow
        {
            get { return _FirstRow; }
            set
            {
                _FirstRow = value;
                OnChange();
            }
        }

        #endregion

        #region PUBLIC PROPERTY FIRSTCOLUMN

        private int _FirstColumn;

        public int FirstColumn
        {
            get { return _FirstColumn; }
            set
            {
                _FirstColumn = value;
                OnChange();
            }
        }

        #endregion

        #region PUBLIC PROPERTY LASTROW

        private int _LastRow;

        public int LastRow
        {
            get { return _LastRow; }
            set
            {
                _LastRow = value;
                OnChange();
            }
        }

        #endregion

        #region PUBLIC PROPERTY LASTCOLUMN

        private int _LastColumn;

        public int LastColumn
        {
            get { return _LastColumn; }
            set
            {
                _LastColumn = value;
                OnChange();
            }
        }

        #endregion
    }
}