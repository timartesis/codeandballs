using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Examenmonitor
{
    public partial class ResendMail : System.Web.UI.Page
    {
        private MailVersturingen registratieMail;
        protected void Page_Load(object sender, EventArgs e)
        {
            registratieMail = MailVersturingen.getInstance();
            activatieLabel.Visible = false;
            mailBestaatLabel.Visible = false;
        }

        //Event om de mail opnieuw te versturen wanneer de gebruiker op de resendbutton geklikt heeft.
        protected void buttonResendMail_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                
                //Controle op mail aanwezigheid + geen activatie in databank
                string email = Email.Text;

                //controleert of het gegeven email bestaat in de tabel Users
                if (!DatabankConnector.ControleerBestaandeEmail(email))
                {
                    //controleert of het gegeven email nog niet geactiveerd is
                    if (!DatabankConnector.ControleerActivatieEmail(email))
                    {
                        //Haalt de volledige naam van de gebruiker uit de database, op basis van de email.
                        string volledigeNaam = DatabankConnector.GetVoornaamEnAchternaam(email);

                        //Verstuurt de registratiemail opnieuw.
                        registratieMail.ZendRegistratieMail(volledigeNaam, email, DatabankConnector.RegistratieMail(email));
                        Response.Redirect("~/MailCheck.aspx");
                    }
                    else
                    {
                        activatieLabel.Visible = true;
                    }
                }
                else
                {
                    mailBestaatLabel.Visible = true;
                }
            }
        }
    }
}