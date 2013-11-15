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
            if (Session["Logged"].Equals("No"))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                gebruiker = Session["User"].ToString();
            }

        }

        protected void buttonWijzig_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string oudPass = OudPasswoord.Text;
                string wachtwoord = ConfirmPassword.Text;
                if (oudPass.Equals(wachtwoord))
                {
                    incorrectLabel.Text = "Het gegeven oud wachtwoord is identiek aan het nieuwe wachtwoord!";
                    incorrectLabel.Visible = true;
                    OudPasswoord.Text = "";
                    Password.Text = "";
                    ConfirmPassword.Text = "";
                }
                else
                {
                    bool controlePasswoord = DatabankConnector.vergelijkPasswoorden(DatabankConnector.GetPasswoordMetMail(gebruiker), oudPass);

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