using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Puzzle.FastTrack.WebForms.Framework.Interfaces;
using System.Reflection;
using Puzzle.NPersist.Samples.Northwind.Domain;

namespace Puzzle.FastTrack.WebForms.Framework.Controls
{
    public class ReferenceEditor : LinkButton, IValueEditor
    {
        public ReferenceEditor()
        {
        }

        public ReferenceEditor(string propertyName)
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

        private string selectUrl = "";
        public virtual string SelectUrl
        {
            get { return selectUrl; }
            set { selectUrl = value; }
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

                    if (value != null)
                    {
                        Employee emp = value as Employee;
                        if (emp != null)
                            this.Text = emp.FirstName + " " + emp.LastName;

                        //this.Text = value.ToString();
                    }
                }
            }
        }

        public void Save()
        {
            //FastTrackPage page = this.Page as FastTrackPage;

            //if (page != null)
            //{
            //    PropertyInfo property = page.GetPropertyInfo(propertyName);
            //    if (property != null)
            //    {
            //        if (property.GetSetMethod() == null)
            //            return;
            //    }

            //    page.SetPropertyValue(propertyName, this.Text);
            //}
        }
    }
}
