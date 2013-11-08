using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Examenmonitor
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Logged"].Equals("No"))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Response.Redirect("Main.aspx");
            }
        }
    }
}