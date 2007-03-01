using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Puzzle.FastTrack.Framework.Web.Interfaces;

namespace Puzzle.FastTrack.Framework.Web.Controls
{
    public class ListEditor : Table, IValueEditor
    {
        public ListEditor()
        {
        }

        public ListEditor(string propertyName)
        {
            this.propertyName = propertyName;
        }

        private bool initialized = false;

        private string propertyName = "";
        public virtual string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        private string selectUrl = "";
        public virtual string SelectUrl
        {
            get { return selectUrl; }
            set { selectUrl = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Initialize();
        }

        public void Initialize()
        {
            if (initialized)
                return;
            initialized = true;

            FastTrackPage page = this.Page as FastTrackPage;

            if (page != null)
            {
                TableRow row1 = new TableRow();
                this.Rows.Add(row1);
                TableCell listCell = new TableCell();
                row1.Cells.Add(listCell);
                listCell.ColumnSpan = 3;

                TableRow row2 = new TableRow();
                this.Rows.Add(row2);

                TableCell selectCell = new TableCell();
                TableCell removeCell = new TableCell();
                TableCell deleteCell = new TableCell();
                row2.Cells.Add(selectCell);
                row2.Cells.Add(removeCell);
                row2.Cells.Add(removeCell);

                ListView list = new ListView(propertyName);
                listCell.Controls.Add(list);

                SelectObjectLinkButton selectLink = new SelectObjectLinkButton(this.propertyName);
                selectCell.Controls.Add(selectLink);
            }
        }

        public void Save()
        {
            //FastTrackPage page = this.Page as FastTrackPage;

            //if (page != null)
            //{
            //    PropertyInfo property = page.GetPropertyInfo(propertyName);
            //    if (property != null)
            //    {
            //        if (property.GetSetMethod() == null)
            //            return;
            //    }

            //    page.SetPropertyValue(propertyName, this.Text);
            //}
        }
    }
}
