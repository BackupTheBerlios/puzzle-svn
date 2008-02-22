using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Puzzle.FastTrack.Framework.Web.Factories;

namespace Puzzle.FastTrack.Framework.Web.Controls
{
    public class ListColumnHeader : HyperLink
    {
        public ListColumnHeader()
        {
        }

        public ListColumnHeader(string columnPropertyName, string currentSortProperty, bool descending)
            : this(columnPropertyName, currentSortProperty, "", descending)
        {
        }

        public ListColumnHeader(string columnPropertyName, string currentSortProperty, string propertyName, bool descending)
        {
            this.columnPropertyName = columnPropertyName ;
            this.propertyName = propertyName;
            if (currentSortProperty != null)
            {
                if (columnPropertyName.ToLower() == currentSortProperty.ToLower())
                    descending = !descending;
                else
                    descending = false;
            }
            else
                descending = false;

            this.descending = descending;
        }

        private bool initialized = false;
        private bool descending = false;

        private string propertyName = "";
        public virtual string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        private string columnPropertyName = "";
        public virtual string ColumnPropertyName
        {
            get { return columnPropertyName; }
            set { columnPropertyName = value; }
        }

        private bool sortable = true;
        public virtual bool Sortable
        {
            get { return sortable; }
            set { sortable = value; }
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

            if (columnPropertyName != null && columnPropertyName != "")
            {
                FastTrackPage page = this.Page as FastTrackPage;

                if (page != null)
                {
                    this.Text = columnPropertyName;

                    if (sortable)
                        this.NavigateUrl = UrlFactory.GetListSortUrl(page.Request, columnPropertyName, descending, propertyName);
                }
            }
        }
    }
}
