using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Examenmonitor
{
    public partial class PassWijzigen : System.Web.UI.Page
    {
        string gebruiker;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Controleren of de sessie geactiveerd is door een login. Zoniet wordt de gebruiker teruggestuurd naar de loginpagina.
            //Indien wel ingelogd, wordt de gebruiker opgehaald aan de hand van de sessie parameter user(mailadres).
            if (Session["Logged"].Equals("No"))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                gebruiker = Session["User"].ToString();
            }

        }

        //Event voor het klikken op de button om de gebruiker zijn wachtwoord te wijzigen.
        protected void buttonWijzig_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {   
                //Haalt het oude wachtwoord en nieuwe wachtwoord uit de form 
                string oudPass = OudPasswoord.Text;
                string wachtwoord = ConfirmPassword.Text;
                //Controleert of het oude wachtwoord gelijk is aan het nieuwe opgegeven wachtwoord.
                if (oudPass.Equals(wachtwoord))
                {
                    //Foutmelding voor het ingeven van gelijke wachtwoorden(nieuw en oud).
                    incorrectLabel.Text = "Het gegeven oud wachtwoord is identiek aan het nieuwe wachtwoord!";
                    incorrectLabel.Visible = true;
                    OudPasswoord.Text = "";
                    Password.Text = "";
                    ConfirmPassword.Text = "";
                }
                else
                {
                    //Controleert of het opgegeven oude wachtwoord gelijk is aan het wachtwoord dat zich in de databank bevindt.
                    bool controlePasswoord = DatabankConnector.vergelijkPasswoorden(DatabankConnector.GetPasswoordMetMail(gebruiker), oudPass);

                    //Indien het opgegeven wachtwoord gelijk is aan het oude wachtwoord in de databank, zal het wachtwoord gewijzigd worden in het nieuwe wachtwoord.
                    if (controlePasswoord)
                    {
                        DatabankConnector.changePassword(gebruiker, wachtwoord);
                        Response.Redirect("PassWijzigenVoltooid.aspx");
                    }
                    else
                    {
                        incorrectLabel.Text = "Het gegeven oud wachtwoord is incorrect!";
                        incorrectLabel.Visible = true;
                        OudPasswoord.Text = "";
                        Password.Text = "";
                        ConfirmPassword.Text = "";
                    }
                }
                
                
            }

        }
    }
}