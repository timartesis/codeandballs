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
            //Response.Redirect("~/RegistratieForm.aspx");
        }

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
                        //Debug.Text = "Ongeactiveerde email";
                        break;
                    case 2:
                        //Veranderen naar meer functionele foutmeldingen
                        //Debug.Text = "Verkeerde login";
                        break;
                    default:
                        //Veranderen naar meer functionele foutmeldingen
                        //Debug.Text = "Unexpected error";
                        break;
                }
            }
        }

        
    }
}