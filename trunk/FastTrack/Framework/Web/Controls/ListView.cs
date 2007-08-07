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

            this.BackColor = Color.LightGray;
            this.ForeColor = Color.Wheat;

            this.CellSpacing = 1;

            FastTrackPage page = this.Page as FastTrackPage;

            if (page != null)
            {
                if ((page.SelectedObjects != null && page.SelectedObjects.Count > 0) || (!string.IsNullOrEmpty(propertyName)))
                    CreateListTable(page);
                else
                    CreateClassListTable(page);
            }
        }

        #region Create list of objects

        private void CreateListTable(FastTrackPage page)
        {
            IList list = null;
            int currPage = 0;
            string sortProperty = "";
            bool descending = false;
            Type type = null;
            if (propertyName != null && propertyName != "")
            {
                string pageNr = page.Request.QueryString[propertyName + "Page"];
                if (pageNr != null)
                    int.TryParse(pageNr, out currPage);
                list = (IList)page.GetPropertyValue(propertyName);

                sortProperty = page.Request.QueryString[propertyName + "Sort"];
                if (page.Request.QueryString[propertyName + "desc"] != null)
                    descending = true;

                type = page.GetListPropertyItemType(propertyName);
            }
            else
            {
                currPage = page.CurrentPage;
                list = page.SelectedObjects;
                sortProperty = page.SortProperty;
                descending = page.Descending;

                type = page.SelectedType;
            }

            ArrayList sorted = list as ArrayList;
            if (sorted != null && sortProperty != null)
            {
                ObjectComparer comparer = new ObjectComparer(page.DomainController, sortProperty, descending);
                sorted.Sort(comparer);
                list = sorted;
            }

            CreateHeaderAndRows(page, list, currPage, sortProperty, descending, type);
        }

        private void CreateHeaderAndRows(FastTrackPage page, IList list, int currPage, string sortProperty, bool descending, Type type)
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

                AddControlRow(page, properties, type);
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
                    cell.BackColor = this.BackColor;
                }
            }

            if (propertyName != null && propertyName != "")
            {
                TableCell cell = new TableCell();
                row.Cells.Add(cell);
                cell.BackColor = this.BackColor;
            }

            TableCell viewCell = new TableCell();
            row.Cells.Add(viewCell);
            viewCell.BackColor = this.BackColor;

            TableCell editCell = new TableCell();
            row.Cells.Add(editCell);
            editCell.BackColor = this.BackColor;

            TableCell deleteCell = new TableCell();
            row.Cells.Add(deleteCell);
            deleteCell.BackColor = this.BackColor;

            foreach (PropertyInfo property in properties)
            {
                TableCell cell = new TableCell();
                ListColumnHeader header = new ListColumnHeader(property.Name, sortProperty, propertyName, descending);
                row.Cells.Add(cell);
                cell.Controls.Add(header);
                cell.BackColor = this.BackColor;
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
                    cell.BackColor = this.ForeColor ;
                }
            }

            if (propertyName != null && propertyName != "")
            {
                TableCell cell = new TableCell();
                RemoveListItemLinkButton link = new RemoveListItemLinkButton(value, propertyName);
                link.Text = "Remove";
                row.Cells.Add(cell);
                cell.Controls.Add(link);
                cell.BackColor = this.ForeColor;
            }

            TableCell viewCell = new TableCell();
            ViewListItemLinkButton viewLink = new ViewListItemLinkButton(value);
            viewLink.Text = "View";
            row.Cells.Add(viewCell);
            viewCell.Controls.Add(viewLink);
            viewCell.BackColor = this.ForeColor;

            TableCell editCell = new TableCell();
            EditListItemLinkButton editLink = new EditListItemLinkButton(value);
            editLink.Text = "Edit";
            row.Cells.Add(editCell);
            editCell.Controls.Add(editLink);
            editCell.BackColor = this.ForeColor;

            TableCell deleteCell = new TableCell();
            DeleteListItemLinkButton deleteLink = new DeleteListItemLinkButton();
            deleteLink.Text = "Delete";
            row.Cells.Add(deleteCell);
            deleteCell.Controls.Add(deleteLink);
            deleteCell.BackColor = this.ForeColor;

            foreach (PropertyInfo property in properties)
            {
                TableCell cell = new TableCell();
                Label label = new Label();
                label.ForeColor = Color.Black;

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
                cell.BackColor = this.ForeColor;
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

        private void AddControlRow(FastTrackPage page, ArrayList properties, Type type)
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

            string typeName = page.GetTypeNameFromType(type);
            CreateObjectLinkButton createObjectLinkButton = new CreateObjectLinkButton(typeName);
            lastCell.Controls.Add(createObjectLinkButton);
        }

        #endregion

        #region Create list of classes

        private void CreateClassListTable(FastTrackPage page)
        {
            IList list = null;
            list = page.GetTypeNames();

            CreateClassHeaderAndRows(page, list);
        }

        private void CreateClassHeaderAndRows(FastTrackPage page, IList list)
        {
            CreateClassHeader(page);

            foreach (string typeName in list)
            {
                TableRow row = new TableRow();
                this.Rows.Add(row);

                CreateClassRow(page, typeName, row);
            }

            //AddNavigationTable(page, list, currPage, properties);
        }

        private void CreateClassHeader(FastTrackPage page)
        {
            TableRow row = new TableRow();
            this.Rows.Add(row);

            TableCell viewCell = new TableCell();
            row.Cells.Add(viewCell);
            viewCell.BackColor = this.BackColor;

            TableCell editCell = new TableCell();
            row.Cells.Add(editCell);
            editCell.BackColor = this.BackColor;

            TableCell deleteCell = new TableCell();
            row.Cells.Add(deleteCell);
            deleteCell.BackColor = this.BackColor;

            TableCell cell = new TableCell();
            Label label = new Label();
            label.Text = "Name";
            label.ForeColor = Color.Black;

            row.Cells.Add(cell);
            cell.Controls.Add(label);
            cell.BackColor = this.BackColor;
        }

        private void CreateClassRow(FastTrackPage page, string typeName, TableRow row)
        {
            TableCell viewCell = new TableCell();
            ViewTypeLinkButton viewLink = new ViewTypeLinkButton(typeName);
            viewLink.Text = "View";
            row.Cells.Add(viewCell);
            viewCell.Controls.Add(viewLink);
            viewCell.BackColor = this.ForeColor;

            TableCell editCell = new TableCell();
            EditTypeLinkButton editLink = new EditTypeLinkButton(typeName);
            editLink.Text = "Edit";
            row.Cells.Add(editCell);
            editCell.Controls.Add(editLink);
            editCell.BackColor = this.ForeColor;

            TableCell deleteCell = new TableCell();
            //DeleteTypeLinkButton deleteLink = new DeleteTypeLinkButton(typeName);
            LinkButton deleteLink = new LinkButton();
            deleteLink.Text = "Delete";
            row.Cells.Add(deleteCell);
            deleteCell.Controls.Add(deleteLink);
            deleteCell.BackColor = this.ForeColor;

            TableCell cell = new TableCell();
            Label label = new Label();
            label.ForeColor = Color.Black;

            label.Text = typeName;

            row.Cells.Add(cell);
            cell.Controls.Add(label);
            cell.BackColor = this.ForeColor;
        }

        //private void AddNavigationTable(FastTrackPage page, IList list, int currPage, ArrayList properties)
        //{
        //    int colSpan = properties.Count;
        //    if (page.SelectedPropertyName != null && page.SelectedPropertyName != "")
        //        colSpan++;

        //    if (propertyName != null && propertyName != "")
        //        colSpan++;

        //    colSpan += 3;

        //    TableRow lastRow = new TableRow();
        //    lastRow.BackColor = Color.White;
        //    this.Rows.Add(lastRow);

        //    TableCell lastCell = new TableCell();
        //    lastCell.ColumnSpan = colSpan;
        //    lastCell.Width = new Unit(100, UnitType.Percentage);
        //    lastRow.Controls.Add(lastCell);

        //    Table navTable = new Table();
        //    navTable.Width = new Unit(100, UnitType.Percentage);
        //    lastCell.Controls.Add(navTable);

        //    TableRow navRow = new TableRow();
        //    navTable.Rows.Add(navRow);

        //    TableCell prevCell = new TableCell();
        //    prevCell.HorizontalAlign = HorizontalAlign.Center;
        //    prevCell.BackColor = Color.White;
        //    prevCell.Width = new Unit(50, UnitType.Percentage);
        //    navRow.Cells.Add(prevCell);

        //    TableCell pageCell = new TableCell();
        //    pageCell.HorizontalAlign = HorizontalAlign.Center;
        //    pageCell.BackColor = Color.White;
        //    pageCell.Wrap = false;
        //    navRow.Cells.Add(prevCell);

        //    TableCell nextCell = new TableCell();
        //    nextCell.HorizontalAlign = HorizontalAlign.Center;
        //    nextCell.BackColor = Color.White;
        //    nextCell.Width = new Unit(50, UnitType.Percentage);
        //    navRow.Cells.Add(nextCell);

        //    if (currPage > 0)
        //    {
        //        HyperLink prevLink = new HyperLink();
        //        prevLink.Text = "Previous";
        //        prevLink.NavigateUrl = UrlFactory.GetListPageUrl(page.Request, currPage, false, propertyName);

        //        prevCell.Controls.Add(prevLink);
        //    }

        //    if (list.Count > pageSize)
        //    {
        //        HyperLink nextLink = new HyperLink();
        //        nextLink.Text = "Next";
        //        nextLink.NavigateUrl = UrlFactory.GetListPageUrl(page.Request, currPage, true, propertyName);

        //        nextCell.Controls.Add(nextLink);
        //    }
        //}

        #endregion
    }
}
