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
        }

        protected void buttonWachtwoordResetten_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //code om values uit de form te halen
                string email = Email.Text;

                if (!DatabankConnector.ControleerBestaandeEmail(email) && DatabankConnector.ControleerActivatieEmail(email))
                {
                    string volledigeNaam = DatabankConnector.GetVoornaamEnAchternaam(email);

                    wachtwoordMail.ZendPaswoordResetMail(volledigeNaam, email, DatabankConnector.PassResetMail(email));
                    Response.Redirect("~/MailCheck.aspx");
                }
                else
                {
                    throw new Exception();
                }
            }
        }
    }
}