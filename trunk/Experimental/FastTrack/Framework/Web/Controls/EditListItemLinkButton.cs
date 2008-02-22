using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Puzzle.FastTrack.Framework.Web.Controls
{
    public class EditListItemLinkButton : LinkButton
    {
        public EditListItemLinkButton()
        {
            this.Text = "Edit";
        }

        public EditListItemLinkButton(object obj)
        {
            this.obj = obj;
            this.Text = "Edit";
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
                page.Response.Redirect(page.GetEditUrl(obj, ""));
        }

    }
}
