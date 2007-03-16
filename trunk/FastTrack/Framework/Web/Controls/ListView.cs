using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Drawing;
using Puzzle.FastTrack.Framework.Web.Factories;
using Puzzle.FastTrack.Framework.Comparers;

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
                CreateListTable(page);
            }
        }

        private void CreateListTable(FastTrackPage page)
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
                ObjectComparer comparer = new ObjectComparer(page.DomainController, sortProperty, descending);
                sorted.Sort(comparer);
                list = sorted;
            }

            CreateHeaderAndRows(page, list, currPage, sortProperty, descending);


        }

        private void CreateHeaderAndRows(FastTrackPage page, IList list, int currPage, string sortProperty, bool descending)
        {
            IList listPage = PageFactory.CreatePage(list, currPage, pageSize);

            if (listPage != null && listPage.Count > 0)
            {
                ArrayList properties = null;

                foreach (object value in listPage)
                {
                    properties = CreateHeader(page, sortProperty, descending, properties, value);

                    break;
                }

                foreach (object value in listPage)
                {
                    TableRow row = new TableRow();
                    this.Rows.Add(row);

                    CreateRow(page, value, row, properties);
                }

                if (list.Count > listPage.Count)
                {
                    AddNavigationTable(page, list, currPage, properties);
                }
            }
        }

        private ArrayList CreateHeader(FastTrackPage page, string sortProperty, bool descending, ArrayList properties, object value)
        {
            TableRow row = new TableRow();
            this.Rows.Add(row);

            properties = new ArrayList(value.GetType().GetProperties());
            properties.Sort(new PropertyComparer());

            if (page.SelectedPropertyName != null && page.SelectedPropertyName != "")
            {
                if (page.SelectedObject != null)
                {
                    TableCell cell = new TableCell();
                    row.Cells.Add(cell);
                    cell.BackColor = Color.White;
                }
            }

            if (propertyName != null && propertyName != "")
            {
                TableCell cell = new TableCell();
                row.Cells.Add(cell);
                cell.BackColor = Color.White;
            }

            TableCell viewCell = new TableCell();
            row.Cells.Add(viewCell);
            viewCell.BackColor = Color.White;

            TableCell editCell = new TableCell();
            row.Cells.Add(editCell);
            editCell.BackColor = Color.White;

            TableCell deleteCell = new TableCell();
            row.Cells.Add(deleteCell);
            deleteCell.BackColor = Color.White;

            foreach (PropertyInfo property in properties)
            {
                TableCell cell = new TableCell();
                ListColumnHeader header = new ListColumnHeader(property.Name, sortProperty, propertyName, descending);
                row.Cells.Add(cell);
                cell.Controls.Add(header);
                cell.BackColor = Color.White;
            }

            return properties;
        }

        private void CreateRow(FastTrackPage page, object value, TableRow row, ArrayList properties)
        {
            if (page.SelectedPropertyName != null && page.SelectedPropertyName != "")
            {
                if (page.SelectedObject != null)
                {
                    TableCell cell = new TableCell();
                    SelectListItemLinkButton link = new SelectListItemLinkButton(value);
                    link.Text = "Select";
                    row.Cells.Add(cell);
                    cell.Controls.Add(link);
                    cell.BackColor = Color.White;
                }
            }

            if (propertyName != null && propertyName != "")
            {
                TableCell cell = new TableCell();
                RemoveListItemLinkButton link = new RemoveListItemLinkButton(value, propertyName);
                link.Text = "Remove";
                row.Cells.Add(cell);
                cell.Controls.Add(link);
                cell.BackColor = Color.White;
            }

            TableCell viewCell = new TableCell();
            ViewListItemLinkButton viewLink = new ViewListItemLinkButton(value);
            viewLink.Text = "View";
            row.Cells.Add(viewCell);
            viewCell.Controls.Add(viewLink);
            viewCell.BackColor = Color.White;

            TableCell editCell = new TableCell();
            EditListItemLinkButton editLink = new EditListItemLinkButton(value);
            editLink.Text = "Edit";
            row.Cells.Add(editCell);
            editCell.Controls.Add(editLink);
            editCell.BackColor = Color.White;

            TableCell deleteCell = new TableCell();
            DeleteListItemLinkButton deleteLink = new DeleteListItemLinkButton();
            deleteLink.Text = "Delete";
            row.Cells.Add(deleteCell);
            deleteCell.Controls.Add(deleteLink);
            deleteCell.BackColor = Color.White;

            foreach (PropertyInfo property in properties)
            {
                TableCell cell = new TableCell();
                Label label = new Label();
                object propertyValue = page.GetPropertyValue(value, property.Name);

                if (page.IsListProperty(value, property.Name))
                {
                    int count = 0;
                    if (propertyValue != null)
                        count = ((IList)propertyValue).Count;
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

        private void AddNavigationTable(FastTrackPage page, IList list, int currPage, ArrayList properties)
        {
            int colSpan = properties.Count;
            if (page.SelectedPropertyName != null && page.SelectedPropertyName != "")
                colSpan++;

            if (propertyName != null && propertyName != "")
                colSpan++;

            colSpan += 3;

            TableRow lastRow = new TableRow();
            lastRow.BackColor = Color.White;
            this.Rows.Add(lastRow);

            TableCell lastCell = new TableCell();
            lastCell.ColumnSpan = colSpan;
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
