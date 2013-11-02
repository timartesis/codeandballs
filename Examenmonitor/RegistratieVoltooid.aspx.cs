using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Examenmonitor
{
    public partial class RegistratieVoltooid : System.Web.UI.Page
    {

        string activatieHash = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Haalt de hash terug uit de url, om nadien te controleren of deze overeenkomt met de DB-waarde om deze nadien op actief te zetten!
            activatieHash = Request.QueryString["hash"];
            hash.Text = activatieHash;
        }

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            //Doorverwijzing naar de login pagina!
            Response.Redirect("~/Login.aspx");
        }
    }
}