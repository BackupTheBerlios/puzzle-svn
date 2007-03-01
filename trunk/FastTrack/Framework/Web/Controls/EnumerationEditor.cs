using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Puzzle.FastTrack.Framework.Web.Interfaces;
using System.Reflection;

namespace Puzzle.FastTrack.Framework.Web.Controls
{
    public class EnumerationEditor : DropDownList, IValueEditor
    {
        public EnumerationEditor()
        {
        }

        public EnumerationEditor(string propertyName)
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
                if (!Page.IsPostBack)
                {
                    object value = page.GetPropertyValue(propertyName);

                    PropertyInfo property = page.GetPropertyInfo(propertyName);
                    if (property != null)
                    {
                        Type type = property.PropertyType;

                        Array values = Enum.GetValues(type);

                        foreach (object enumValue in values)
                        {
                            ListItem item = new ListItem(Enum.GetName(type, enumValue), enumValue.ToString());
                            this.Items.Add(item);
                            if (value != null)
                            {
                                if (value.Equals(enumValue))
                                    item.Selected = true;
                            }

                        }
                    }

                }
            }
        }

        public void Save()
        {
            FastTrackPage page = this.Page as FastTrackPage;

            if (page != null)
            {
                PropertyInfo property = page.GetPropertyInfo(propertyName);
                if (property != null)
                {
                    if (property.GetSetMethod() == null)
                        return;
                }

                page.SetPropertyValue(propertyName, Enum.Parse(property.PropertyType, this.SelectedItem.Text));
            }
        }

    }
}
