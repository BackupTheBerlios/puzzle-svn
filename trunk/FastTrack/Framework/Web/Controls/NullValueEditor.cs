using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Puzzle.FastTrack.Framework.Web.Interfaces;
using System.Reflection;

namespace Puzzle.FastTrack.Framework.Web.Controls
{
    public class NullValueEditor : CheckBox, IValueEditor
    {
        public NullValueEditor()
        {
        }

        public NullValueEditor(string propertyName)
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
                    if (page.GetPropertyNullStatus(propertyName))
                    {
                        this.Checked = true;
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
                    if (page.IsReadOnlyProperty(property.Name))
                        return;
                }

                page.SetPropertyNullStatus(propertyName, this.Checked);
            }
        }

    }
}
