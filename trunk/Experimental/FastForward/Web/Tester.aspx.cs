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

namespace Puzzle.FastForward.Web
{
    public partial class Tester : System.Web.UI.Page
    {
        public string aspString = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            aspString = "<% = 23.ToString() %>";
        }
    }
}
