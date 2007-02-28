using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Puzzle.FastTrack.WebForms.Framework.Interfaces;
using System.Reflection;

namespace Puzzle.FastTrack.WebForms.Framework.Controls
{
    public class NumberEditor : TextBox, IValueEditor
    {
        public NumberEditor()
        {

        }

        public NumberEditor(string propertyName)
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
                PropertyInfo property = page.GetPropertyInfo(propertyName);
                if (property != null)
                {
                    if (property.GetSetMethod() == null)
                        this.ReadOnly = true;
                }

                if (!Page.IsPostBack)
                {
                    object value = page.GetPropertyValue(propertyName);

                    if (value != null)
                    {
                        this.Text = value.ToString();
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

                try
                {
                    object obj = page.SelectedObject;

                    if (obj != null)
                    {
                        if (property != null)
                        {
                            Type type = property.PropertyType;

                            page.SetPropertyValue(propertyName, Convert.ChangeType(this.Text, type));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
