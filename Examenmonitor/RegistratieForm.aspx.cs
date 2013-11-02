using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Examenmonitor
{
    public partial class RegistratieForm : System.Web.UI.Page
    {
        private MailVersturingen registratieMail;
        protected void Page_Load(object sender, EventArgs e)
        {
            registratieMail = MailVersturingen.getInstance();
        }

        

        protected void buttonRegistreer_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //Code om values uit de form te halen
                string voorNaam = Voornaam.Text;
                string achterNaam = AchterNaam.Text;
                string email = Email.Text;
                string wachtwoord = ConfirmPassword.Text;
                string volledigeNaam = voorNaam + " " + achterNaam;
                
                
                if (DatabankConnector.ControleerEmail(email))
                {
                    registratieMail.ZendRegistratieMail(volledigeNaam, email, DatabankConnector.RegistratieMail(email));
                    //DatabankConnector.InsertGebruiker(email, wachtwoord, voorNaam, achterNaam);
                    //in commentaar zodat de databank niet constant geupdate wordt
                    Response.Redirect("~/MailCheck.aspx");
                }
                else
                {
                    //Indien er een email adres al geregistreerd is, zal deze een foutmelding geven!
                    dubbeleEmail.Text = "Email is al geregistreerd!";
                    dubbeleEmail.ForeColor = Color.Red;
                }
            }

        }

        
    }
}