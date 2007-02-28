using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Puzzle.NPersist.Framework.Mapping;
using System.Reflection;
using Puzzle.FastTrack.WebForms.Framework.Interfaces;

namespace Puzzle.FastTrack.WebForms.Framework.Controls
{
    public class StringEditor : TextBox, IValueEditor 
    {
        public StringEditor()
        {
        }

        public StringEditor(string propertyName)
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

                page.SetPropertyValue(propertyName, this.Text);
            }
        }

    }
}
