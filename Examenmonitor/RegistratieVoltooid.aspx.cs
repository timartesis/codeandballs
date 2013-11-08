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
            
            //Gewoon om te checken, mag later weg!
            bool controleActivatieHash = DatabankConnector.ControleerActivatieHash(activatieHash);
            hash.Text += " : " + controleActivatieHash.ToString();

            //Zet de knoppen op onzichtbaar
            buttonLogin.Visible = false;
            buttonResend.Visible = false;

            //Controleert of de meegestuurde hash overeenkomt met de hash in de Databank.
            if (controleActivatieHash)
            {
                hashControle.Text = "Activatie is succesvol beëindigd! Ga via onderstaande button naar de login pagina!";
                buttonLogin.Visible = true;
            }
            else
            {
                hashControle.Text = "Er doet zich een probleem voor! Ga via onderstaande button naar de pagina om de email opnieuw te verzenden!";
                buttonResend.Visible = true;
            }
        }

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            //Doorverwijzing naar de login pagina!
            Response.Redirect("~/Login.aspx");
        }

        protected void buttonResend_Click(object sender, EventArgs e)
        {
            //Doorverwijzing naar de login pagina!
            Response.Redirect("~/ResendMail.aspx");
        }
    }
}