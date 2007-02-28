using System;
using System.Collections.Generic;
using System.Text;
using Puzzle.NPersist.Framework;
using System.Web.UI;
using Puzzle.NPersist.Framework.Mapping;
using System.Web.UI.WebControls;
using System.Reflection;

namespace Puzzle.FastTrack.WebForms.Framework.Controls
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
                    Table table = new Table();
                    this.Controls.Add(table);

                    foreach (PropertyInfo property in obj.GetType().GetProperties())
                    {
                        PropertyEditor propertyEditor = new PropertyEditor(property.Name, table);
                        propertyEditor.Initialize();
                    }
                }
            }
        }	
    }
}
