using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Puzzle.FastTrack.Framework.Web.Controls
{
    public class DeleteObjectButton : Button
    {
        public DeleteObjectButton()
        {
            this.Text = "Delete";
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            FastTrackPage page = this.Page as FastTrackPage;

            if (page != null)
            {
                object obj = page.SelectedObject;
                if (obj != null)
                {
                    Type type = obj.GetType();
                    if (type != null)
                    {
                        page.DeleteObject();
                        page.Response.Redirect(page.GetListUrl(type, ""));
                    }
                }
            }
        }
    }
}
