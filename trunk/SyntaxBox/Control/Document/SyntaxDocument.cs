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
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using Puzzle.SourceCode.SyntaxDocumentParsers;

namespace Puzzle.SourceCode
{
    /// <summary>
    /// The SyntaxDocument is a component that is responsible for Parsing , Folding , Undo / Redo actions and various text actions.
    /// </summary>
    public class SyntaxDocument : Component, IEnumerable
    {
        #region General declarations

        private readonly RowList mDocument = new RowList();
        private Container components;

        /// <summary>
        /// 
        /// </summary>
        public RowList KeywordQueue = new RowList();

        private UndoBlockCollection mCaptureBlock;
        private bool mCaptureMode;
        private bool mFolding = true;

        /// <summary>
        /// For public use only
        /// </summary>
        private bool mIsParsed = true;

        private bool mModified;

        private string mSyntaxFile = "";


        /// <summary>
        /// Gets or Sets if folding needs to be recalculated
        /// </summary>
        public bool NeedResetRows;

        /// <summary>
        /// List of rows that should be parsed
        /// </summary>
        public RowList ParseQueue = new RowList();

        /// <summary>
        /// The active parser of the document
        /// </summary>
        public IParser Parser = new DefaultParser();

        /// <summary>
        /// Tag property , lets the user store custom data in the row.
        /// </summary>
        public object Tag;

        /// <summary>
        /// Buffer containing undo actions
        /// </summary>
        public UndoBuffer UndoBuffer = new UndoBuffer();

        /// <summary>
        /// List of rows that is not hidden by folding
        /// </summary>
        public RowList VisibleRows = new RowList();

        #region PUBLIC PROPERTY UNDOSTEP

        private int _UndoStep;

        public int UndoStep
        {
            get
            {
                if (_UndoStep > UndoBuffer.Count)
                    _UndoStep = UndoBuffer.Count;

                return _UndoStep;
            }
            set { _UndoStep = value; }
        }

        #endregion

        /// <summary>
        /// Event that is raised when there is no more rows to parse
        /// </summary>
        public event EventHandler ParsingCompleted;

        public event EventHandler UndoBufferChanged = null;

        /// <summary>
        /// Raised when the parser is active
        /// </summary>
        public event EventHandler Parsing;

        /// <summary>
        /// Raised when the document content is changed
        /// </summary>
        public event EventHandler Change;

        public event RowEventHandler BreakPointAdded;
        public event RowEventHandler BreakPointRemoved;

        public event RowEventHandler BookmarkAdded;
        public event RowEventHandler BookmarkRemoved;

        protected virtual void OnBreakPointAdded(Row r)
        {
            if (BreakPointAdded != null)
                BreakPointAdded(this, new RowEventArgs(r));
        }

        protected virtual void OnBreakPointRemoved(Row r)
        {
            if (BreakPointRemoved != null)
                BreakPointRemoved(this, new RowEventArgs(r));
        }

        protected virtual void OnBookmarkAdded(Row r)
        {
            if (BookmarkAdded != null)
                BookmarkAdded(this, new RowEventArgs(r));
        }

        protected virtual void OnBookmarkRemoved(Row r)
        {
            if (BookmarkRemoved != null)
                BookmarkRemoved(this, new RowEventArgs(r));
        }

        protected virtual void OnUndoBufferChanged()
        {
            if (UndoBufferChanged != null)
                UndoBufferChanged(this, EventArgs.Empty);
        }


        public virtual void InvokeBreakPointAdded(Row r)
        {
            OnBreakPointAdded(r);
        }

        public virtual void InvokeBreakPointRemoved(Row r)
        {
            OnBreakPointRemoved(r);
        }

        public virtual void InvokeBookmarkAdded(Row r)
        {
            OnBookmarkAdded(r);
        }

        public virtual void InvokeBookmarkRemoved(Row r)
        {
            OnBookmarkRemoved(r);
        }


        //public event System.EventHandler CreateParser;

        /// <summary>
        /// Raised when the modified flag has changed
        /// </summary>
        public event EventHandler ModifiedChanged;

        //----------------------------------------------

        /// <summary>
        /// Raised when a row have been parsed
        /// </summary>
        public event ParserEventHandler RowParsed;

        //	public event ParserEventHandler	 RowAdded;
        /// <summary>
        /// Raised when a row have been deleted
        /// </summary>
        public event ParserEventHandler RowDeleted;

        #endregion

        #region PUBLIC PROPERTY MAXUNDOBUFFERSIZE

        /// <summary>
        /// Gets or Sets the Maximum number of entries in the undobuffer
        /// </summary>
        public int MaxUndoBufferSize
        {
            get { return UndoBuffer.MaxSize; }
            set { UndoBuffer.MaxSize = value; }
        }

        #endregion

        #region PUBLIC PROPERTY VERSION

        private long _Version = long.MinValue;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public long Version
        {
            get { return _Version; }
            set { _Version = value; }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public SyntaxDocument(IContainer container) : this()
        {
            container.Add(this);
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public SyntaxDocument()
        {
            Parser.Document = this;
            Text = "";
            ResetVisibleRows();
            Init();
        }

        /// <summary>
        /// Get or Set the Modified flag
        /// </summary>
        public bool Modified
        {
            get { return mModified; }
            set
            {
                mModified = value;
                OnModifiedChanged();
            }
        }

        /// <summary>
        /// Get or Set the Name of the Syntaxfile to use
        /// </summary>
        [DefaultValue("")]
        public string SyntaxFile
        {
            get { return mSyntaxFile; }
            set
            {
                mSyntaxFile = value;
                //	this.Parser=new Parser_Default();
                Parser.Init(value);
                Text = Text;
            }
        }

        /// <summary>
        /// Gets or Sets if the document should use folding or not
        /// </summary>
        [DefaultValue(true)]
        public bool Folding
        {
            get { return mFolding; }
            set
            {
                mFolding = value;
                if (!value)
                {
                    foreach (Row r in this)
                    {
                        r.Expanded = true;
                    }
                }
                ResetVisibleRows();

                OnChange();
            }
        }

        /// <summary>
        /// Gets if the document is fully parsed
        /// </summary>
        [Browsable(false)]
        public bool IsParsed
        {
            get { return mIsParsed; }
        }

        /// <summary>
        /// Returns the row at the specified index
        /// </summary>
        public Row this[int index]
        {
            get
            {
                if (index < 0 || index >= mDocument.Count)
                {
                    //	System.Diagnostics.Debugger.Break ();
                    return null;
                }
                return mDocument[index];
            }

            set { mDocument[index] = value; }
        }

        /// <summary>
        /// Gets the row count of the document
        /// </summary>
        [Browsable(false)]
        public int Count
        {
            get { return mDocument.Count; }
        }

        /// <summary>
        /// Gets or Sets the text of the entire document
        /// </summary>		
        [Browsable(false)]
        //	[RefreshProperties (RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Text
        {
            get
            {
                int i = 0;
                var sb = new StringBuilder();

                ParseAll(true);
                foreach (Row tr in mDocument)
                {
                    if (i > 0)
                        sb.Append(Environment.NewLine);
                    tr.MatchCase();
                    sb.Append(tr.Text);
                    i++;
                }
                return sb.ToString();
            }

            set
            {
                clear();
                Add("");
                InsertText(value, 0, 0);
                UndoBuffer.Clear();
                UndoStep = 0;
                Modified = false;
                mIsParsed = false;
                //OnChange();
                InvokeChange();
            }
        }

        /// <summary>
        /// Gets and string array containing the text of all rows.
        /// </summary>
        public string[] Lines
        {
            get
            {
                return Text.Split("\n".ToCharArray());
            }
            set
            {
                string s = "";
                foreach (string sl in value)
                    s += sl + "\n";
                Text = s.Substring(0, s.Length - 1);
            }
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return mDocument.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// For internal use only
        /// </summary>
        public void ChangeVersion()
        {
            Version ++;
            if (Version > long.MaxValue - 10)
                Version = long.MinValue;
        }

        /// <summary>
        /// Starts an Undo Capture.
        /// This method can be called if you with to collect multiple text operations into one undo action
        /// </summary>
        public void StartUndoCapture()
        {
            mCaptureMode = true;
            mCaptureBlock = new UndoBlockCollection();
        }

        /// <summary>
        /// Ends an Undo capture and pushes the collected actions onto the undostack
        /// <seealso cref="StartUndoCapture"/>
        /// </summary>
        /// <returns></returns>
        public UndoBlockCollection EndUndoCapture()
        {
            mCaptureMode = false;
            AddToUndoList(mCaptureBlock);
            return mCaptureBlock;
        }

        /// <summary>
        /// ReParses the document
        /// </summary>
        public void ReParse()
        {
            Text = Text;
        }

        /// <summary>
        /// Removes all bookmarks in the document
        /// </summary>
        public void ClearBookmarks()
        {
            foreach (Row r in this)
            {
                r.Bookmarked = false;
            }
            InvokeChange();
        }

        /// <summary>
        /// Removes all breakpoints in the document.
        /// </summary>
        public void ClearBreakpoints()
        {
            foreach (Row r in this)
            {
                r.Breakpoint = false;
            }
            InvokeChange();
        }


        /// <summary>
        /// Call this method to ensure that a specific row is fully parsed
        /// </summary>
        /// <param name="Row"></param>
        public void EnsureParsed(Row Row)
        {
            ParseAll();
            Parser.ParseLine(Row.Index, true);
        }

        private void Init()
        {
            var l = new SyntaxDefinition();
            l.mainSpanDefinition = new SpanDefinition(l)
                          {
                              MultiLine = true
                          };
            Parser.Init(l);
        }

        /// <summary>
        /// Call this method to make the SyntaxDocument raise the Changed event
        /// </summary>
        public void InvokeChange()
        {
            OnChange();
        }

        /// <summary>
        /// Performs a span parse on all rows. No Keyword colorizing
        /// </summary>
        public void ParseAll()
        {
            while (ParseQueue.Count > 0)
                ParseSome();

            ParseQueue.Clear();
        }

        /// <summary>
        /// Parses all rows , either a span parse or a full parse with keyword colorizing
        /// </summary>
        public void ParseAll(bool ParseKeywords)
        {
            ParseAll();
            if (ParseKeywords)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (this[i].RowState != RowState.AllParsed)
                        Parser.ParseLine(i, true);
                }
                ParseQueue.Clear();
                KeywordQueue.Clear();
            }
        }

        /// <summary>
        /// Folds all foldable rows
        /// </summary>
        public void FoldAll()
        {
            ParseAll(false);
            foreach (Row r in this)
            {
                r.Expanded = false;
            }
            ResetVisibleRows();
            OnChange();
        }

        /// <summary>
        /// UnFolds all foldable rows
        /// </summary>
        public void UnFoldAll()
        {
            ParseAll(false);
            foreach (Row r in this)
            {
                r.Expanded = true;
            }
            ResetVisibleRows();
            OnChange();
        }


        /// <summary>
        /// Parses a chunk of 1000 rows , this is not thread safe
        /// </summary>
        public void ParseSome()
        {
            ParseSome(1000);
        }

        /// <summary>
        /// Parse a chunk of rows, this is not thread safe
        /// </summary>
        /// <param name="RowCount">The number of rows to parse</param>
        public void ParseSome(int RowCount)
        {
            if (ParseQueue.Count > 0)
            {
                mIsParsed = false;
                int i = 0;
                while (i < RowCount && ParseQueue.Count > 0)
                {
                    Row row = ParseQueue[0];
                    i += ParseRows(row);
                }

                if (NeedResetRows)
                    ResetVisibleRows();

                if (Parsing != null)
                    Parsing(this, new EventArgs());
            }
            else
            {
                if (!mIsParsed && !Modified)
                {
                    mIsParsed = true;

                    foreach (Row r in this)
                    {
                        if (r.expansion_StartSpan != null && r.Expansion_EndRow != null)
                        {
                            if (r.expansion_StartSpan.Scope.DefaultExpanded == false)
                                r.Expanded = false;
                        }
                    }
                    ResetVisibleRows();
                    if (ParsingCompleted != null)
                        ParsingCompleted(this, new EventArgs());
                }
            }

            if (ParseQueue.Count == 0 && KeywordQueue.Count > 0)
            {
//				Console.WriteLine (this.KeywordQueue.Count.ToString ());
                int i = 0;
                while (i < RowCount/20 && KeywordQueue.Count > 0)
                {
                    Row row = KeywordQueue[0];
                    i += ParseRows(row, true);
                }
            }
        }


        /// <summary>
        /// Add a new row with the specified text to the bottom of the document
        /// </summary>
        /// <param name="Text">Text to add</param>
        /// <returns>The row that was added</returns>		
        public Row Add(string Text)
        {
            return Add(Text, true);
        }

        /// <summary>
        /// Add a new row with the specified text to the bottom of the document
        /// </summary>
        /// <param name="Text">Text to add</param>
        /// <param name="StoreUndo">true if and undo action should be added to the undo stack</param>
        /// <returns>The row that was added</returns>
        public Row Add(string Text, bool StoreUndo)
        {
            var xtl = new Row();
            mDocument.Add(xtl);
            xtl.Document = this;
            xtl.Text = Text;
            return xtl;
        }

        /// <summary>
        /// Insert a text at the specified row index
        /// </summary>
        /// <param name="Text">Text to insert</param>
        /// <param name="index">Row index where the text should be inserted</param>
        /// <returns>The row that was inserted</returns>
        public Row Insert(string Text, int index)
        {
            return Insert(Text, index, true);
        }

        /// <summary>
        /// Insert a text at the specified row index
        /// </summary>
        /// <param name="Text">Text to insert</param>
        /// <param name="index">Row index where the text should be inserted</param>
        /// <param name="StoreUndo">true if and undo action should be added to the undo stack</param>
        /// <returns>The row that was inserted</returns>
        public Row Insert(string Text, int index, bool StoreUndo)
        {
            var xtl = new Row {Document = this};
            mDocument.Insert(index, xtl);
            xtl.Text = Text;
            if (StoreUndo)
            {
                var undo = new UndoBlock()
                           {
                               Text = Text,
                               
                           };

                undo.Position.Y = IndexOf(xtl);
                AddToUndoList(undo);
            }

            //this.ResetVisibleRows ();
            return xtl;
        }


        /// <summary>
        /// Remove a row at specified row index
        /// </summary>
        /// <param name="index">index of the row that should be removed</param>
        public void Remove(int index)
        {
            Remove(index, true);
        }

        public void Remove(int index, bool StoreUndo)
        {
            Remove(index, StoreUndo, true);
        }

        /// <summary>
        /// Remove a row at specified row index
        /// </summary>
        /// <param name="index">index of the row that should be removed</param>
        /// <param name="StoreUndo">true if and undo action should be added to the undo stack</param>
        public void Remove(int index, bool StoreUndo, bool RaiseChanged)
        {
            Row r = this[index];

            if (StoreUndo)
            {
                var ra = new TextRange();

                if (index != Count - 1)
                {
                    ra.FirstColumn = 0;
                    ra.FirstRow = index;
                    ra.LastRow = index + 1;
                    ra.LastColumn = 0;
                }
                else
                {
                    ra.FirstColumn = r.PrevRow.Text.Length;
                    ra.FirstRow = index - 1;
                    ra.LastRow = index;
                    ra.LastColumn = r.Text.Length;
                }
                PushUndoBlock(UndoAction.DeleteRange, GetRange(ra), ra.FirstColumn, ra.FirstRow);
            }


            mDocument.RemoveAt(index);
            if (r.InKeywordQueue)
                KeywordQueue.Remove(r);

            if (r.InQueue)
                ParseQueue.Remove(r);

            //this.ResetVisibleRows ();
            OnRowDeleted(r);
            if (RaiseChanged)
                OnChange();
        }

        /// <summary>
        /// Deletes a range of text
        /// </summary>
        /// <param name="Range">the range that should be deleted</param>
        public void DeleteRange(TextRange Range)
        {
            DeleteRange(Range, true);
        }

        private int ParseRows(Row r)
        {
            return ParseRows(r, false);
        }


        private int ParseRows(Row r, bool Keywords)
        {
            if (!Keywords)
            {
                int index = IndexOf(r);
                int count = 0;
                try
                {
                    while (r.InQueue && count < 100)
                    {
                        if (index >= 0)
                        {
                            if (index > 0)
                                if (this[index - 1].InQueue)
                                    ParseRow(this[index - 1]);

                            Parser.ParseLine(index, false);
                        }

                        int i = ParseQueue.IndexOf(r);
                        if (i >= 0)
                            ParseQueue.RemoveAt(i);
                        r.InQueue = false;
                        index++;
                        count++;
                        r = this[index];

                        if (r == null)
                            break;
                    }
                }
                catch {}

                return count;
            }
            else
            {
                int index = IndexOf(r);
                if (index == -1 || r.InKeywordQueue == false)
                {
                    KeywordQueue.Remove(r);
                    return 0;
                }
                int count = 0;
                try
                {
                    while (r.InKeywordQueue && count < 100)
                    {
                        if (index >= 0)
                        {
                            if (index > 0)
                                if (this[index - 1].InQueue)
                                    ParseRow(this[index - 1]);

                            Parser.ParseLine(index, true);
                        }
                        index++;
                        count++;
                        r = this[index];

                        if (r == null)
                            break;
                    }
                }
                catch {}

                return count;
            }
        }


        /// <summary>
        /// Forces a row to be parsed
        /// </summary>
        /// <param name="r">Row to parse</param>
        /// <param name="ParseKeywords">true if keywords and operators should be parsed</param>
        public void ParseRow(Row r, bool ParseKeywords)
        {
            int index = IndexOf(r);
            if (index >= 0)
            {
                if (index > 0)
                    if (this[index - 1].InQueue)
                        ParseRow(this[index - 1]);

                Parser.ParseLine(index, false);
                if (ParseKeywords)
                    Parser.ParseLine(index, true);
            }

            int i = ParseQueue.IndexOf(r);
            if (i >= 0)
                ParseQueue.RemoveAt(i);

            r.InQueue = false;
        }


        /// <summary>
        /// Forces a row to be parsed
        /// </summary>
        /// <param name="r">Row to parse</param>
        public void ParseRow(Row r)
        {
            ParseRow(r, false);
        }

        /// <summary>
        /// Gets the row index of the next bookmarked row
        /// </summary>
        /// <param name="StartIndex">Start index</param>
        /// <returns>Index of the next bookmarked row</returns>
        public int GetNextBookmark(int StartIndex)
        {
            for (int i = StartIndex + 1; i < Count; i++)
            {
                Row r = this[i];
                if (r.Bookmarked)
                    return i;
            }

            for (int i = 0; i < StartIndex; i++)
            {
                Row r = this[i];
                if (r.Bookmarked)
                    return i;
            }

            return StartIndex;
        }

        /// <summary>
        /// Gets the row index of the previous bookmarked row
        /// </summary>
        /// <param name="StartIndex">Start index</param>
        /// <returns>Index of the previous bookmarked row</returns>
        public int GetPreviousBookmark(int StartIndex)
        {
            for (int i = StartIndex - 1; i >= 0; i--)
            {
                Row r = this[i];
                if (r.Bookmarked)
                    return i;
            }

            for (int i = Count - 1; i >= StartIndex; i--)
            {
                Row r = this[i];
                if (r.Bookmarked)
                    return i;
            }

            return StartIndex;
        }

        /// <summary>
        /// Deletes a range of text
        /// </summary>
        /// <param name="Range">Range to delete</param>
        /// <param name="StoreUndo">true if the actions should be pushed onto the undo stack</param>
        public void DeleteRange(TextRange Range, bool StoreUndo)
        {
            TextRange r = Range;
            Modified = true;
            if (StoreUndo)
            {
                string deltext = GetRange(Range);
                PushUndoBlock(UndoAction.DeleteRange, deltext, r.FirstColumn, r.FirstRow);
            }


            if (r.FirstRow == r.LastRow)
            {
                Row xtr = this[r.FirstRow];
                int max = Math.Min(r.FirstColumn, xtr.Text.Length);
                string left = xtr.Text.Substring(0, max);
                string right = "";
                if (xtr.Text.Length >= r.LastColumn)
                    right = xtr.Text.Substring(r.LastColumn);
                xtr.Text = left + right;
            }
            else
            {
                if (r.LastRow > Count - 1)
                    r.LastRow = Count - 1;

                string row1 = "";
                Row xtr = this[r.FirstRow];
                if (r.FirstColumn > xtr.Text.Length)
                {
                    int diff = r.FirstColumn - xtr.Text.Length;
                    var ws = new string(' ', diff);
                    InsertText(ws, xtr.Text.Length, r.FirstRow, true);
                    //return;
                }

                row1 = xtr.Text.Substring(0, r.FirstColumn);

                string row2 = "";
                Row xtr2 = this[r.LastRow];
                int Max = Math.Min(xtr2.Text.Length, r.LastColumn);
                row2 = xtr2.Text.Substring(Max);

                string tot = row1 + row2;
                //bool fold=this[r.LastRow].IsCollapsed | this[r.FirstRow].IsCollapsed ;

                int start = r.FirstRow;
                int end = r.LastRow;

                for (int i = end - 1; i >= start; i--)
                {
                    Remove(i, false, false);
                }

                //todo: DeleteRange error						
                //this.Insert ( tot  ,r.FirstRow,false);


                Row row = this[start];
                bool f = row.IsCollapsed;
                row.Expanded = true;
                row.Text = tot;
                row.startSpans.Clear();
                row.endSpans.Clear();
                row.startSpan = null;
                row.endSpan = null;
                row.Parse();
            }

            ResetVisibleRows();
            OnChange();
        }

        /// <summary>
        /// Get a range of text
        /// </summary>
        /// <param name="Range">The range to get</param>
        /// <returns>string containing the text inside the given range</returns>
        public string GetRange(TextRange Range)
        {
            if (Range.FirstRow >= Count)
                Range.FirstRow = Count;

            if (Range.LastRow >= Count)
                Range.LastRow = Count;

            if (Range.FirstRow != Range.LastRow)
            {
                //note:error has been tracked here
                Row r1 = this[Range.FirstRow];
                int mx = Math.Min(r1.Text.Length, Range.FirstColumn);
                string s1 = r1.Text.Substring(mx) + Environment.NewLine;

                //if (Range.LastRow >= this.Count)
                //	Range.LastRow=this.Count -1;

                Row r2 = this[Range.LastRow];
                if (r2 == null)
                    return "";

                int Max = Math.Min(r2.Text.Length, Range.LastColumn);
                string s2 = r2.Text.Substring(0, Max);

                string s3 = "";
                var sb = new StringBuilder();
                for (int i = Range.FirstRow + 1; i <= Range.LastRow - 1; i++)
                {
                    Row r3 = this[i];

                    sb.Append(r3.Text + Environment.NewLine);
                }

                s3 = sb.ToString();
                return s1 + s3 + s2;
            }
            else
            {
                Row r = this[Range.FirstRow];
                int Max = Math.Min(r.Text.Length, Range.LastColumn);
                int Length = Max - Range.FirstColumn;
                if (Length <= 0)
                    return "";
                string s = r.Text.Substring(Range.FirstColumn, Max - Range.FirstColumn);
                return s;
            }
        }


        /// <summary>
        /// Returns the index of a given row
        /// </summary>
        /// <param name="xtr">row to find</param>
        /// <returns>Index of the given row</returns>
        public int IndexOf(Row xtr)
        {
            return mDocument.IndexOf(xtr);
        }

        /// <summary>
        /// Clear all content in the document
        /// </summary>
        public void clear()
        {
            foreach (Row r in mDocument)
            {
                OnRowDeleted(r);
            }
            mDocument.Clear();
            //		this.FormatRanges.Clear ();
            ParseQueue.Clear();
            KeywordQueue.Clear();
            UndoBuffer.Clear();
            UndoStep = 0;
            //	this.Add ("");
            //	ResetVisibleRows();
            //	this.OnChange ();
        }

        public void Clear()
        {
            Text = "";
        }

        /// <summary>
        /// Inserts a text into the document at a given column,row.
        /// </summary>
        /// <param name="text">Text to insert</param>
        /// <param name="xPos">Column</param>
        /// <param name="yPos">Row index</param>
        /// <returns>TextPoint containing the end of the inserted text</returns>
        public TextPoint InsertText(string text, int xPos, int yPos)
        {
            return InsertText(text, xPos, yPos, true);
        }

        /// <summary>
        /// Inserts a text into the document at a given column,row.
        /// </summary>
        /// <param name="text">Text to insert</param>
        /// <param name="xPos">Column</param>
        /// <param name="yPos">Row index</param>
        /// <param name="StoreUndo">true if this action should be pushed onto the undo stack</param>
        /// <returns>TextPoint containing the end of the inserted text</returns>
        public TextPoint InsertText(string text, int xPos, int yPos, bool StoreUndo)
        {
            Modified = true;
            Row xtr = this[yPos];
            string lft, rgt;
            if (xPos > xtr.Text.Length)
            {
                //virtualwhitespace fix
                int Padd = xPos - xtr.Text.Length;
                var PaddStr = new string(' ', Padd);
                text = PaddStr + text;
                xPos -= Padd;
            }
            lft = xtr.Text.Substring(0, xPos);
            rgt = xtr.Text.Substring(xPos);
            string NewText = lft + text + rgt;


            string t = NewText.Replace(Environment.NewLine, "\n");
            string[] lines = t.Split('\n');
            xtr.Text = lines[0];

            Row lastrow = xtr;

            //this.Parser.ParsePreviewLine(xtr);	
            xtr.Parse();
            if (!xtr.InQueue)
                ParseQueue.Add(xtr);
            xtr.InQueue = true;

            int i = IndexOf(xtr);
            for (int j = 1; j <= lines.GetUpperBound(0); j++)
            {
                lastrow = Insert(lines[j], j + i, false);
            }

            if (StoreUndo)
                PushUndoBlock(UndoAction.InsertRange, text, xPos, yPos);

            ResetVisibleRows();
            OnChange();


            return new TextPoint(lastrow.Text.Length - rgt.Length, IndexOf(lastrow));
        }

        private void OnModifiedChanged()
        {
            if (ModifiedChanged != null)
                ModifiedChanged(this, new EventArgs());
        }

        private void OnChange()
        {
            if (Change != null)
                Change(this, new EventArgs());
        }

        private void OnRowParsed(Row r)
        {
            if (RowParsed != null)
                RowParsed(this, new RowEventArgs(r));

            OnApplyFormatRanges(r);
        }

        //		private void OnRowAdded(Row r)
        //		{
        //			if (RowAdded != null)
        //				RowAdded(this,new RowEventArgs(r));
        //		}
        private void OnRowDeleted(Row r)
        {
            if (RowDeleted != null)
                RowDeleted(this, new RowEventArgs(r));
        }

        public void PushUndoBlock(UndoAction Action, string text, int x, int y)
        {
            var undo = new UndoBlock()
                       {
                           Action = Action,
                           Text = text
                       };

            undo.Position.Y = y;
            undo.Position.X = x;
            //AddToUndoList(undo);

            if (mCaptureMode)
            {
                mCaptureBlock.Add(undo);
            }
            else
            {
                AddToUndoList(undo);
            }
        }

        /// <summary>
        /// Gets a Range from a given text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <returns></returns>
        public TextRange GetRangeFromText(string text, int xPos, int yPos)
        {
            string t = text.Replace(Environment.NewLine, "\n");
            string[] lines = t.Split("\n".ToCharArray());
            var r = new TextRange
                    {
                        FirstColumn = xPos,
                        FirstRow = yPos,
                        LastRow = (lines.Length - 1 + yPos),
                        LastColumn = lines[lines.Length - 1].Length
                    };

            if (r.FirstRow == r.LastRow)
                r.LastColumn += r.FirstColumn;

            return r;
        }

        public void AddToUndoList(UndoBlock undo)
        {
            //store the undo action in a actiongroup
            var ActionGroup = new UndoBlockCollection {undo};

            AddToUndoList(ActionGroup);
        }

        /// <summary>
        /// Add an action to the undo stack
        /// </summary>
        /// <param name="ActionGroup">action to add</param>
        public void AddToUndoList(UndoBlockCollection ActionGroup)
        {
            UndoBuffer.ClearFrom(UndoStep);
            UndoBuffer.Add(ActionGroup);
            UndoStep++;
            OnUndoBufferChanged();
        }

        /// <summary>
        /// Perform an undo action
        /// </summary>
        /// <returns>The position where the caret should be placed</returns>
        public TextPoint Undo()
        {
            if (UndoStep == 0)
                return new TextPoint(-1, -1);


            UndoBlockCollection ActionGroup = UndoBuffer[UndoStep - 1];
            UndoBlock undo = ActionGroup[0];

            for (int i = ActionGroup.Count - 1; i >= 0; i--)
            {
                undo = ActionGroup[i];
                //TextPoint tp=new TextPoint (undo.Position.X,undo.Position.Y);
                switch (undo.Action)
                {
                    case UndoAction.DeleteRange:
                        InsertText(undo.Text, undo.Position.X, undo.Position.Y, false);
                        break;
                    case UndoAction.InsertRange:
                        {
                            TextRange r = GetRangeFromText(undo.Text, undo.Position.X, undo.Position.Y);
                            DeleteRange(r, false);
                        }
                        break;
                    default:
                        break;
                }
            }

            UndoStep--;
            ResetVisibleRows();

            //no undo steps left , the document is not dirty
            if (UndoStep == 0)
                Modified = false;

            var tp = new TextPoint(undo.Position.X, undo.Position.Y);
            OnUndoBufferChanged();
            return tp;
        }

        public void AutoIndentSegment(Span span)
        {
            if (span == null)
                span = this[0].startSpan;

            Row start = span.StartRow;
            Row end = span.EndRow;
            if (start == null)
                start = this[0];

            if (end == null)
                end = this[Count - 1];


            for (int i = start.Index; i <= end.Index; i++)
            {
                Row r = this[i];
                int depth = r.Indent;
                string text = r.Text.Substring(r.GetLeadingWhitespace().Length);
                var indent = new string('\t', depth);
                r.Text = indent + text;
            }
            ResetVisibleRows();
        }

        //Returns the span object at the given position
        /// <summary>
        /// Gets a span object form a given column , Row index
        /// (This only applies if the row is fully parsed)
        /// </summary>
        /// <param name="p">Column and Rowindex</param>
        /// <returns>span object at the given position</returns>
        public Span GetSegmentFromPos(TextPoint p)
        {
            Row xtr = this[p.Y];
            int CharNo = 0;

            if (xtr.Count == 0)
                return xtr.startSpan;

            Span prev = xtr.startSpan;
            foreach (Word w in xtr)
            {
                if (w.Text.Length + CharNo > p.X)
                {
                    if (CharNo == p.X)
                        return prev;
                    else
                        return w.Span;
                }
                else
                {
                    CharNo += w.Text.Length;
                    prev = w.Span;
                }
            }

            return xtr.endSpan;
        }

        //the specific word that contains the char in point p
        /// <summary>
        /// Gets a Word object form a given column , Row index
        /// (this only applies if the row is fully parsed)
        /// </summary>
        /// <param name="p">Column and Rowindex</param>
        /// <returns>Word object at the given position</returns>
        public Word GetWordFromPos(TextPoint p)
        {
            Row xtr = this[p.Y];
            int CharNo = 0;
            Word CorrectWord = null;
            foreach (Word w in xtr)
            {
                if (CorrectWord != null)
                {
                    if (w.Text == "")
                        return w;
                    else
                        return CorrectWord;
                }

                if (w.Text.Length + CharNo > p.X || w == xtr[xtr.Count - 1])
                {
                    //return w;
                    CorrectWord = w;
                }
                else
                {
                    CharNo += w.Text.Length;
                }
            }
            return CorrectWord;
        }

        //the specific word that contains the char in point p
        /// <summary>
        /// Gets a Word object form a given column , Row index
        /// (this only applies if the row is fully parsed)
        /// </summary>
        /// <param name="p">Column and Rowindex</param>
        /// <returns>Word object at the given position</returns>
        public Word GetFormatWordFromPos(TextPoint p)
        {
            Row xtr = this[p.Y];
            int CharNo = 0;
            Word CorrectWord = null;
            foreach (Word w in xtr.FormattedWords)
            {
                if (CorrectWord != null)
                {
                    if (w.Text == "")
                        return w;
                    else
                        return CorrectWord;
                }

                if (w.Text.Length + CharNo > p.X || w == xtr[xtr.Count - 1])
                {
                    //return w;
                    CorrectWord = w;
                }
                else
                {
                    CharNo += w.Text.Length;
                }
            }
            return CorrectWord;
        }

        /// <summary>
        /// Call this method to make the document raise the RowParsed event
        /// </summary>
        /// <param name="row"></param>
        public void InvokeRowParsed(Row row)
        {
            OnRowParsed(row);
        }


        /// <summary>
        /// Call this method to recalculate the visible rows
        /// </summary>
        public void ResetVisibleRows()
        {
            InternalResetVisibleRows();
        }

        private void InternalResetVisibleRows()
        {
//			if (System.DateTime.Now > new DateTime (2002,12,31))
//			{
//				
//				this.mDocument = new RowList ();
//				this.Add ("BETA VERSION EXPIRED");
//				VisibleRows = this.mDocument;
//				return;
//			}

            if (!mFolding)
            {
                VisibleRows = mDocument;
                NeedResetRows = false;
            }
            else
            {
                NeedResetRows = false;
                VisibleRows = new RowList(); //.Clear ();			
                int RealRow = 0;
                Row r = null;
                for (int i = 0; i < Count; i++)
                {
                    r = this[RealRow];
                    VisibleRows.Add(r);
                    bool collapsed = false;
                    if (r.CanFold)
                        if (r.expansion_StartSpan.Expanded == false)
                        {
                            if (r.expansion_StartSpan.EndWord == null) {}
                            else
                            {
                                r = r.Expansion_EndRow; // .expansion_StartSpan.EndRow;
                                collapsed = true;
                            }
                        }

                    if (!collapsed)
                        RealRow++;
                    else
                        RealRow = IndexOf(r) + 1;

                    if (RealRow >= Count)
                        break;
                }
            }
        }

        /// <summary>
        /// Converts a Column/Row index position into a char index
        /// </summary>
        /// <param name="pos">TextPoint where x is column and y is row index</param>
        /// <returns>Char index in the document text</returns>
        public int PointToIntPos(TextPoint pos)
        {
            int y = 0;
            int p = 0;
            foreach (Row r in this)
            {
                if (y == pos.Y)
                    break;
                p += r.Text.Length + Environment.NewLine.Length;
                y++;
            }

            return p + Math.Min(pos.X, this[pos.Y].Text.Length);
        }

        /// <summary>
        /// Converts a char index into a Column/Row index
        /// </summary>
        /// <param name="pos">Char index to convert</param>
        /// <returns>Point where x is column and y is row index</returns>
        public TextPoint IntPosToPoint(int pos)
        {
            int p = 0;
            int y = 0;
            foreach (Row r in this)
            {
                p += r.Text.Length + Environment.NewLine.Length;
                if (p > pos)
                {
                    p -= r.Text.Length + Environment.NewLine.Length;
                    int x = pos - p;
                    return new TextPoint(x, y);
                }
                y++;
            }
            return new TextPoint(-1, -1);
        }

        /// <summary>
        /// Toggle expansion of a given row
        /// </summary>
        /// <param name="r"></param>
        public void ToggleRow(Row r)
        {
            if (!mFolding)
                return;

            if (r.Expansion_EndRow == null || r.Expansion_StartRow == null)
                return;


//			if (r.IsCollapsed)
//			{
//				r.expansion_StartSpan.Expanded =	true;
//				ExpandRow(r);
//			}
//			else
//			{
//				r.expansion_StartSpan.Expanded =	false;
//				CollapseRow(r);
//			}

            if (r.CanFold)
                r.Expanded = !r.Expanded;
            ResetVisibleRows();

            OnChange();
        }

        /// <summary>
        /// Collapse a given row
        /// </summary>
        /// <param name="r">Row to collapse</param>
        private void CollapseRow(Row r)
        {
            //remove rows from visible list
            Row start = r.Expansion_StartRow;
            Row end = r.Expansion_EndRow;
            int count = end.VisibleIndex - start.VisibleIndex - 1;
            int startIndex = start.VisibleIndex + 1;
            for (int i = 0; i <= count; i++)
            {
                VisibleRows.RemoveAt(startIndex);
            }
        }

        /// <summary>
        /// Expand a given row
        /// </summary>
        /// <param name="r">Row to expand</param>
        private void ExpandRow(Row r)
        {
            //add rows to visible list...
            Row start = r.Expansion_StartRow;
            Row end = r.Expansion_EndRow;
            int count = end.Index - start.Index - 1;
            int startIndex = start.Index + 1;

            int visIndex = start.VisibleIndex + 1;
            int i = 0;
            while (i <= count)
            {
                Row tmpRow = this[startIndex + i];
                VisibleRows.Insert(visIndex, tmpRow);
                if (tmpRow.expansion_StartSpan != null)
                    if (tmpRow.expansion_StartSpan.Expanded == false)
                    {
                        tmpRow = tmpRow.expansion_StartSpan.EndRow;
                        i = tmpRow.Index - startIndex;
                    }
                visIndex++;
                i++;
            }

            //ResetVisibleRows ();
        }

        /// <summary>
        /// Perform an redo action
        /// </summary>
        /// <returns>The position where the caret should be placed</returns>
        public TextPoint Redo()
        {
            if (UndoStep >= UndoBuffer.Count)
                return new TextPoint(-1, -1);

            UndoBlockCollection ActionGroup = UndoBuffer[UndoStep];
            UndoBlock undo = ActionGroup[0];
            for (int i = 0; i < ActionGroup.Count; i++)
            {
                undo = ActionGroup[i];

                switch (undo.Action)
                {
                    case UndoAction.InsertRange:
                        {
                            InsertText(undo.Text, undo.Position.X, undo.Position.Y, false);
                        }
                        break;
                    case UndoAction.DeleteRange:
                        {
                            TextRange r = GetRangeFromText(undo.Text, undo.Position.X, undo.Position.Y);
                            DeleteRange(r, false);
                        }
                        break;
                    default:
                        break;
                }
            }

            TextRange ran = GetRangeFromText(undo.Text, undo.Position.X, undo.Position.Y);
            UndoStep++;
            ResetVisibleRows();
            OnUndoBufferChanged();
            return new TextPoint(ran.LastColumn, ran.LastRow);
        }

        public Word GetStartBracketWord(Word Start, Pattern End, Span FindIn)
        {
            if (Start == null || Start.Pattern == null || Start.Span == null)
                return null;

            int CurrentRow = Start.Row.Index;
            int FirstRow = FindIn.StartRow.Index;
            int x = Start.Index;
            int count = 0;
            while (CurrentRow >= FirstRow)
            {
                for (int i = x; i >= 0; i--)
                {
                    Word w = this[CurrentRow][i];
                    if (w.Span == FindIn && w.Type == WordType.Word)
                    {
                        if (w.Pattern == Start.Pattern)
                            count++;
                        if (w.Pattern == End)
                            count--;

                        if (count == 0)
                            return w;
                    }
                }

                if (!Start.Pattern.IsMultiLineBracket)
                    break;

                CurrentRow--;
                if (CurrentRow >= 0)
                    x = this[CurrentRow].Count - 1;
            }
            return null;
        }


        public Word GetEndBracketWord(Word Start, Pattern End, Span FindIn)
        {
            if (Start == null || Start.Pattern == null || Start.Span == null)
                return null;

            int CurrentRow = Start.Row.Index;

            int LastRow = Count - 1;
            if (FindIn.EndRow != null)
                LastRow = FindIn.EndRow.Index;


            int x = Start.Index;
            int count = 0;
            while (CurrentRow <= LastRow)
            {
                for (int i = x; i < this[CurrentRow].Count; i++)
                {
                    Word w = this[CurrentRow][i];
                    if (w.Span == FindIn && w.Type == WordType.Word)
                    {
                        if (w.Pattern == Start.Pattern)
                            count++;
                        if (w.Pattern == End)
                            count--;

                        if (count == 0)
                            return w;
                    }
                }

                if (!Start.Pattern.IsMultiLineBracket)
                    break;

                CurrentRow++;
                x = 0;
            }
            return null;
        }


        /// <summary>
        /// Sets a syntax file, from an embedded resource.
        /// </summary>
        /// <param name="assembly">The assembly which contains the embedded resource.</param>
        /// <param name="resourceName">The name of the resource.</param>
        public void SetSyntaxFromEmbeddedResource(Assembly assembly, String resourceName)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            if (string.IsNullOrEmpty(resourceName))
                throw new ArgumentNullException("resourceName");

            //
            // Get the xml from an embedded resource. Load the stream.
            //

            Stream stream = assembly.GetManifestResourceStream(resourceName);
            stream.Seek(0, SeekOrigin.Begin);

            //
            // Read stream.
            //

            var reader = new StreamReader(stream);
            String xml = reader.ReadToEnd();

            //
            // Clean up stream.
            //

            stream.Close();

            //
            // Initialize.
            //

            Parser.Init(SyntaxDefinition.FromSyntaxXml(xml));
            Text = Text;
        }


        protected internal void OnApplyFormatRanges(Row row)
        {
            row.FormattedWords = row.words;
        }
    }
}