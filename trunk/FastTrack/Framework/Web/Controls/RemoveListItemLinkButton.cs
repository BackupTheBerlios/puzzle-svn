using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Collections;

namespace Puzzle.FastTrack.Framework.Web.Controls
{
    public class RemoveListItemLinkButton : LinkButton
    {
        public RemoveListItemLinkButton()
        {
            this.Text = "Remove";
        }

        public RemoveListItemLinkButton(object obj, string propertyName)
        {
            this.obj = obj;
            this.propertyName = propertyName;
            this.Text = "Remove";
        }

        private object obj = "";
        public virtual object Object
        {
            get { return obj; }
            set { obj = value; }
        }

        private string propertyName = "";
        public virtual string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            FastTrackPage page = this.Page as FastTrackPage;

            if (page != null)
            {
                IList list = (IList)page.GetPropertyValue(propertyName);
                list.Remove(obj);

                page.SaveObject(page.SelectedObject);

                page.Response.Redirect(page.Request.Url.OriginalString);
            }
        }
    }
}
