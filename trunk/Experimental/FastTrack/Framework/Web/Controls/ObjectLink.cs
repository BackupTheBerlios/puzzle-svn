using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Puzzle.FastTrack.Framework.Web.Controls
{
    public class ObjectLink : HyperLink
    {
        public ObjectLink()
        {
        }

        public ObjectLink(string propertyName)
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
                        this.Text = page.GetObjectName(value);
                        this.NavigateUrl = page.GetEditUrl(value, this.viewUrl);
                    }
                }
            }
        }
    }
}
