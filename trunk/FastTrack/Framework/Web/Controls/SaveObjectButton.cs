using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Puzzle.FastTrack.Framework.Web.Controls
{
    public class SaveObjectButton : Button
    {
        public SaveObjectButton()
        {
            this.Text = "Save";
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            FastTrackPage page = this.Page as FastTrackPage;

            if (page != null)
                page.SaveObject();
        }
    }
}
