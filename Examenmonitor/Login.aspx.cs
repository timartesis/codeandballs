using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Examenmonitor
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void buttonRegistreerHier_Click(object sender, EventArgs e)
        {
        }

        //inloggen met de gegevens
        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string email = Email.Text.ToString();
                string password = Password.Text.ToString();

                int code = DatabankConnector.login(email, password);

                switch (code)
                {
                    case 0:
                       
                        Session["Logged"] = "yes";
                        Session["User"] = Email.Text;
                        Response.Redirect("Main.aspx"); 
                        break;
                    case 1:
                        //Veranderen naar meer functionele foutmeldingen
                        CredentialsLabel.Text = "Uw mail adress is nog niet geactiveerd, Geen mail gehad? Klik dan nu op 'Activatie mail verzenden'";
                        resendMailButton.Visible = true;
                        break;
                    case 2:
                        //Veranderen naar meer functionele foutmeldingen
                        CredentialsLabel.Text = "De login gegevens zijn foutief!";
                        break;
                    default:
                        //Veranderen naar meer functionele foutmeldingen
                        CredentialsLabel.Text = "Je hebt het internet kapot gemaakt. Er is een Federaal team onderweg naar uw locatie. Gelieve te wachten en vreedzaam mee te werken met het team tijdens de arrestatie.";
                        break;
                }
            }
        }

        
    }
}