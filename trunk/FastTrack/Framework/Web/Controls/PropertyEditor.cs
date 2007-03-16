using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NPersist.Framework.Mapping;
using Puzzle.NPersist.Framework.Enumerations;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Puzzle.FastTrack.Framework.Web.Factories;
using Puzzle.FastTrack.Framework.Web.Interfaces;

namespace Puzzle.FastTrack.Framework.Web.Controls
{
    public class PropertyEditor : Control
    {
        public PropertyEditor()
        {
        }

        public PropertyEditor(string propertyName, Table table)
        {
            this.propertyName = propertyName;
            this.table = table;
        }

        private bool initialized = false;

        private string propertyName = "";
        public virtual string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        private Table table;
        public virtual Table Table
        {
            get { return table; }
            set { table = value; }
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

            if (propertyName != null && propertyName != "")
            {
                FastTrackPage page = GetPage();

                if (page != null)
                {
                    object obj = page.SelectedObject;

                    if (obj != null)
                    {
                        SetupEditor(obj);
                    }
                }
            }

        }

        private void SetupEditor(object obj)
        {
            FastTrackPage page = GetPage();

            if (page != null)
            {
                PropertyInfo property = obj.GetType().GetProperty(propertyName);
                if (property != null)
                {
                    Control editor = EditorFactory.GetPropertyValueEditor(page, obj, property);

                    if (editor != null)
                    {
                        Label nameLabel = new Label();
                        nameLabel.Text = property.Name;

                        TableRow row = null;
                        Table table = this.table;

                        if (table == null)
                            table = this.Parent as Table;

                        if (table != null)
                        {
                            row = new TableRow();
                            table.Rows.Add(row);
                        }

                        if (row == null)
                        {
                            row = this.Parent as TableRow;
                        }

                        if (row != null)
                        {
                            TableCell nameCell = new TableCell();
                            TableCell editorCell = new TableCell();

                            nameCell.VerticalAlign = VerticalAlign.Top;
                            nameCell.HorizontalAlign = HorizontalAlign.Left;
                            editorCell.VerticalAlign = VerticalAlign.Top;
                            editorCell.HorizontalAlign = HorizontalAlign.Left;
                            editorCell.Wrap = false;

                            nameCell.Controls.Add(nameLabel);
                            editorCell.Controls.Add(editor);

                            row.Cells.Add(nameCell);
                            row.Cells.Add(editorCell);

                            if (page.IsNullableProperty(obj, propertyName))
                            {
                                NullValueEditor nullEditor = new NullValueEditor(propertyName);

                                Label nullLabel = new Label();
                                nullLabel.Text = "Null";

                                editorCell.Controls.Add(nullEditor);
                                editorCell.Controls.Add(nullLabel);
                            }
                        }
                        else
                        {
                            this.Controls.Add(nameLabel);
                            this.Controls.Add(editor);
                        }

                        ((IValueEditor)editor).Initialize();
                    }
                }
            }
        }

        public FastTrackPage GetPage()
        {
            if (this.Table != null)
                return this.Table.Page as FastTrackPage;
            else
                return this.Page as FastTrackPage;
        }
    }
}
