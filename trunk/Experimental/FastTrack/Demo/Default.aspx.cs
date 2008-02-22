using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Puzzle.NPersist.Framework;
using Puzzle.FastTrack.Framework.Web.Controls;
using Puzzle.NPersist.Framework.Mapping;
using Puzzle.FastTrack.Framework.Web;

public partial class _Default : FastTrackPage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ObjectEditor employeeEditor = new ObjectEditor();

        Control form = this.FindControl("form1");

        form.Controls.Add(employeeEditor);
    }

    protected void updateButton_Click(object sender, EventArgs e)
    {
        this.SaveObject();
    }
}
