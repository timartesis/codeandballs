using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Examenmonitor
{
    public partial class PassresetVoltooid : System.Web.UI.Page
    {

        string passresetHash = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            MailVersturingen pasresetmail = MailVersturingen.getInstance();
            //Haalt de hash terug uit de url, om nadien te controleren of deze overeenkomt met de DB-waarde om deze nadien op actief te zetten!
            passresetHash = Request.QueryString["hash"];
            hash.Text = passresetHash;

            bool controlePassresetHash = DatabankConnector.ControleerPassresetHash(passresetHash);

            //Gewoon om te checken, mag later weg!
            hash.Text += " : " + controlePassresetHash.ToString();

            //Zet de knoppen op onzichtbaar
            buttonLogin.Visible = false;
            buttonResend.Visible = false;

            //Controleert of de meegestuurde hash overeenkomt met de hash in de Databank.
            if (controlePassresetHash)
            {
                string randomPass = Membership.GeneratePassword(8, 2);
                string email = DatabankConnector.getEmailTroughPassResetHash(passresetHash);
                string volledigeNaam = DatabankConnector.GetVoornaamEnAchternaam(email);
                pasresetmail.ZendPaswoordResetMail(volledigeNaam, email, randomPass);

                hashControle.Text = "Passwoord is verstuurd, gelieve uw mail te checken.";
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
            Response.Redirect("~/WachtwoordVergeten.aspx");
        }
    }
}