using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Puzzle.FastTrack.Framework.Web.Controls
{
    public class CreateObjectButton : Button
    {
        public CreateObjectButton()
        {
            this.Text = "Create";
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            FastTrackPage page = this.Page as FastTrackPage;

            if (page != null)
            {
                Type type = page.SelectedType;
                if (type != null)
                {
                    page.CreateObject(type);

                    object obj = page.SelectedObject;
                    if (obj != null)
                    {
                        page.Response.Redirect(page.GetEditUrl(obj, ""));
                    }
                }
            }
        }

    }
}
