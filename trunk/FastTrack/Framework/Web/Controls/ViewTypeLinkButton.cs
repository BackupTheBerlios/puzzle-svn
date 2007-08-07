using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Puzzle.FastTrack.Framework.Web.Controls
{
    public class ViewTypeLinkButton : LinkButton
    {
        public ViewTypeLinkButton()
        {
            this.Text = "View";
        }

        public ViewTypeLinkButton(string className)
        {
            this.className = className;
            this.Text = "View";
        }

        private string className = "";
        public virtual string ClassName
        {
            get { return ClassName; }
            set { ClassName = value; }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            FastTrackPage page = this.Page as FastTrackPage;
            if (page != null)
            {
                Type type = page.GetTypeFromTypeName(className);
                page.Response.Redirect(page.GetViewUrl(type, ""));
            }
        }
    }
}
