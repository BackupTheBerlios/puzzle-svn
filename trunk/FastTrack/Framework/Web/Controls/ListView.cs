using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Drawing;
using Puzzle.FastTrack.Framework.Web.Factories;
using Puzzle.FastTrack.Framework.Web.Comparers;

namespace Puzzle.FastTrack.Framework.Web.Controls
{
    public class ListView : Table
    {
        public ListView()
        {
        }

        public ListView(string propertyName)
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

        private string viewUrl = "";
        public virtual string ViewUrl
        {
            get { return viewUrl; }
            set { viewUrl = value; }
        }

        private int pageSize = 10;
        public virtual int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
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

            this.BackColor = Color.Gray;
            this.CellSpacing = 1;

            FastTrackPage page = this.Page as FastTrackPage;

            if (page != null)
            {
                IList list = null;
                int currPage = 0;
                string sortProperty = "";
                bool descending = false;
                if (propertyName != null && propertyName != "")
                {
                    string pageNr = page.Request.QueryString[propertyName + "Page"];
                    if (pageNr != null)
                        int.TryParse(pageNr, out currPage);
                    list = (IList)page.GetPropertyValue(propertyName);

                    sortProperty = page.Request.QueryString[propertyName + "Sort"];
                    if (page.Request.QueryString[propertyName + "desc"] != null)
                        descending = true;
                }
                else
                {
                    currPage = page.CurrentPage;
                    list = page.SelectedObjects;
                    sortProperty = page.SortProperty;
                    descending = page.Descending;
                }

                ArrayList sorted = list as ArrayList;
                if (sorted != null && sortProperty != null)
                {
                    ObjectComparer comparer = new ObjectComparer(page, sortProperty, descending);
                    sorted.Sort(comparer);
                    list = sorted;                    
                }

                IList listPage = PageFactory.CreatePage(list, currPage, pageSize); 

                if (listPage != null && listPage.Count > 0)
                {
                    ArrayList properties = null;  

                    foreach (object value in listPage)
                    {
                        TableRow row = new TableRow();
                        this.Rows.Add(row);

                        properties = new ArrayList(value.GetType().GetProperties());
                        properties.Sort(new PropertyComparer());

                        foreach (PropertyInfo property in properties)
                        {
                            TableCell cell = new TableCell();
                            ListColumnHeader header = new ListColumnHeader(property.Name, sortProperty, propertyName, descending);
                            row.Cells.Add(cell);
                            cell.Controls.Add(header);
                            cell.BackColor = Color.White;
                        }

                        break;
                    }

                    foreach (object value in listPage)
                    {
                        TableRow row = new TableRow();
                        this.Rows.Add(row);

                        foreach (PropertyInfo property in properties)
                        {
                            TableCell cell = new TableCell();
                            Label label = new Label();
                            object propertyValue = page.GetPropertyValue(value, property.Name);

                            if (typeof(IList).IsAssignableFrom(property.PropertyType))
                            {
                                int count = 0;
                                if (propertyValue != null)
                                    count = ((IList) propertyValue).Count;
                                label.Text = count.ToString() + " items";
                            }
                            else
                            {
                                if (propertyValue != null)
                                    label.Text = propertyValue.ToString();
                            }

                            row.Cells.Add(cell);
                            cell.Controls.Add(label);
                            cell.BackColor = Color.White;
                        }
                    }

                    if (list.Count > listPage.Count)
                    {
                        TableRow lastRow = new TableRow();
                        lastRow.BackColor = Color.White;
                        this.Rows.Add(lastRow);

                        TableCell lastCell = new TableCell();
                        lastCell.ColumnSpan = properties.Count;
                        lastCell.Width = new Unit(100, UnitType.Percentage);
                        lastRow.Controls.Add(lastCell);

                        Table navTable = new Table();
                        navTable.Width = new Unit(100, UnitType.Percentage);
                        lastCell.Controls.Add(navTable);

                        TableRow navRow = new TableRow();
                        navTable.Rows.Add(navRow);

                        TableCell prevCell = new TableCell();
                        prevCell.HorizontalAlign = HorizontalAlign.Center;
                        prevCell.BackColor = Color.White;
                        prevCell.Width = new Unit(50, UnitType.Percentage);
                        navRow.Cells.Add(prevCell);

                        TableCell pageCell = new TableCell();
                        pageCell.HorizontalAlign = HorizontalAlign.Center;
                        pageCell.BackColor = Color.White;
                        pageCell.Wrap = false;
                        navRow.Cells.Add(prevCell);

                        TableCell nextCell = new TableCell();
                        nextCell.HorizontalAlign = HorizontalAlign.Center;
                        nextCell.BackColor = Color.White;
                        nextCell.Width = new Unit(50, UnitType.Percentage);
                        navRow.Cells.Add(nextCell);

                        if (currPage > 0)
                        {
                            HyperLink prevLink = new HyperLink();
                            prevLink.Text = "Previous";
                            prevLink.NavigateUrl = UrlFactory.GetListPageUrl(page.Request, currPage, false, propertyName);

                            prevCell.Controls.Add(prevLink);
                        }

                        if (list.Count > pageSize)
                        {
                            HyperLink nextLink = new HyperLink();
                            nextLink.Text = "Next";
                            nextLink.NavigateUrl = UrlFactory.GetListPageUrl(page.Request, currPage, true, propertyName);

                            nextCell.Controls.Add(nextLink);
                        }
                    }
                }
            }
        }
    }
}
