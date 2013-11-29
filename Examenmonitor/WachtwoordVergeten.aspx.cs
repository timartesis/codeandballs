using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Examenmonitor
{
    public partial class WachtwoordVergeten : System.Web.UI.Page
    {
        private MailVersturingen wachtwoordMail;

        protected void Page_Load(object sender, EventArgs e)
        {
            wachtwoordMail = MailVersturingen.getInstance();
            activatieLabel.Visible = false;
            mailBestaatLabel.Visible = false;
        }
        
        //Event voor het klikken van de reset button.
        protected void buttonWachtwoordResetten_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //code om values uit de form te halen
                string email = Email.Text;
                
                //controleert of het gegeven email bestaat in de tabel Users
                if (!DatabankConnector.ControleerBestaandeEmail(email))
                {
                    //controleert of het gegeven email al geactiveerd is
                    if (DatabankConnector.ControleerActivatieEmail(email))
                    {   
                        //Haalt de volledige naam van de gebruiker op aan de hand van de opgegeven email.
                        string volledigeNaam = DatabankConnector.GetVoornaamEnAchternaam(email);

                        //Gaat een mail terugsturen waarin een random gegenereerd wachtwoord gegeven zal worden.
                        wachtwoordMail.ZendPaswoordResetMail(volledigeNaam, email, DatabankConnector.PassResetMail(email));
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