using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Examenmonitor
{
    public partial class RegistratieForm : System.Web.UI.Page
    {
        private RegistratieMail registratieMail;
        protected void Page_Load(object sender, EventArgs e)
        {
            registratieMail = RegistratieMail.getInstance();
        }

        

        protected void buttonRegistreer_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //Code om values uit de form te halen
                //@tim moet da hier al gehashed worden of nog niet ? 
                string voorNaam = Voornaam.Text;
                string achterNaam = AchterNaam.Text;
                string email = Email.Text;
                string wachtwoord = ConfirmPassword.Text;
                //TODO passen naar DB handler
                //TODO terug naar home gaan
                string volledigeNaam = voorNaam + " " + achterNaam;
                registratieMail.ZendRegistratieMail(volledigeNaam, email, "Login.aspx");
                //DatabankConnector.InsertGebruiker(email, wachtwoord, voorNaam, achterNaam);
                //in commentaar zodat de databank niet constant geupdate wordt
                Response.Redirect("~/RegistratieVoltooid.aspx");
            }

        }

        
    }
}