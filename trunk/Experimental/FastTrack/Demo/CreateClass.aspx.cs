using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Puzzle.SideFX.Framework;
using Puzzle.FastTrack.Framework.Factories;

public partial class CreateClass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void createButton_Click(object sender, EventArgs e)
    {
        string className = nameTextBox.Text;
        if (string.IsNullOrEmpty(className))
            return;

        string tableName = tableTextBox.Text;
        if (string.IsNullOrEmpty(tableName))
            tableName = className;

        IEngine engine = EngineFactory.CreateEngine();
        engine.Execute("create class " + className + "(Table = " + tableName + ")");
    }
}
