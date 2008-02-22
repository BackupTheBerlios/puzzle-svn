using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NPersist.Framework;
using System.Web.UI;
using Puzzle.NPersist.Framework.Mapping;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Drawing;
using System.Collections;
using Puzzle.FastTrack.Framework.Comparers;

namespace Puzzle.FastTrack.Framework.Web.Controls
{
    public class ObjectEditor : Control
    {
        public ObjectEditor()
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Initialize();
        }

        private bool initialized = false;

        public void Initialize()
        {
            if (initialized)
                return;
            initialized = true;

            FastTrackPage page = this.Page as FastTrackPage;
            if (page != null)
            {
                object obj = page.SelectedObject;

                if (obj != null)
                {
                    //Edit existing object
                    Table table = new Table();
                    this.Controls.Add(table);

                    ArrayList properties = new ArrayList(obj.GetType().GetProperties());
                    properties.Sort(new PropertyComparer());

                    foreach (PropertyInfo property in properties)
                    {
                        PropertyEditor propertyEditor = new PropertyEditor(property.Name, table);
                        propertyEditor.Initialize();
                    }

                    TableRow row = new TableRow();
                    table.Rows.Add(row);

                    TableCell buttonCell = new TableCell();
                    buttonCell.ColumnSpan = 2;
                    row.Cells.Add(buttonCell);

                    SaveObjectButton saveButton = new SaveObjectButton();
                    DeleteObjectButton deleteButton = new DeleteObjectButton();

                    buttonCell.Controls.Add(saveButton);
                    buttonCell.Controls.Add(deleteButton);
                }
                else
                {
                    if (page.SelectedType != null)
                    {
                        //Create new object
                        Type type = page.SelectedType;

                        Table table = new Table();
                        this.Controls.Add(table);

                        ArrayList properties = new ArrayList(type.GetProperties());
                        properties.Sort(new PropertyComparer());

                        foreach (PropertyInfo property in properties)
                        {
                            PropertyEditor propertyEditor = new PropertyEditor(property.Name, table);
                            propertyEditor.Initialize();
                        }

                        TableRow row = new TableRow();
                        table.Rows.Add(row);

                        TableCell buttonCell = new TableCell();
                        buttonCell.ColumnSpan = 2;
                        row.Cells.Add(buttonCell);

                        CreateObjectButton createButton = new CreateObjectButton();
                        
                        buttonCell.Controls.Add(createButton);

                    }
                }
            }
        }	
    }
}
