using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Reflection;

namespace Puzzle.FastTrack.Framework.Web.Controls
{
    public class SelectObjectLinkButton : LinkButton
    {
        public SelectObjectLinkButton()
        {
            this.Text = "Select";
        }

        public SelectObjectLinkButton(string propertyName)
        {
            this.propertyName = propertyName;
            this.Text = "Select";
        }

        private string propertyName = "";
        public virtual string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        private string listUrl = "";
        public virtual string ListUrl
        {
            get { return ListUrl; }
            set { ListUrl = value; }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            FastTrackPage page = this.Page as FastTrackPage;

            if (page != null)
            {
                PropertyInfo property = page.GetPropertyInfo(propertyName);
                if (property != null)
                {
                    page.Response.Redirect(page.GetListUrl(property.PropertyType, this.listUrl, page.SelectedObject, propertyName));
                }
            }
        }
    }
}
