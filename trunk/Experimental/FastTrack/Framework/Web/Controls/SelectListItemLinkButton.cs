using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Collections;

namespace Puzzle.FastTrack.Framework.Web.Controls
{
    public class SelectListItemLinkButton : LinkButton
    {
        public SelectListItemLinkButton()
        {
            this.Text = "Select";
        }

        public SelectListItemLinkButton(object obj)
        {
            this.obj = obj;
            this.Text = "Select";
        }

        private object obj = "";
        public virtual object Object
        {
            get { return obj; }
            set { obj = value; }
        }


        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            FastTrackPage page = this.Page as FastTrackPage;

            if (page != null)
            {
                PropertyInfo property = page.GetPropertyInfo(page.SelectedPropertyName);
                if (property != null)
                {
                    if (page.IsListProperty(property.Name))
                    {
                        IList list = (IList) page.GetPropertyValue(property.Name);
                        list.Add(obj);
                    }
                    else
                    {
                        page.SetPropertyValue(property.Name, obj);
                    }

                    page.SaveObject(page.SelectedObject);

                    page.Response.Redirect(page.GetEditUrl(page.SelectedObject, ""));
                }
            }
        }
    }
}
