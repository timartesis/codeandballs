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
                string email = Email.Text;
                //TODO: code toevoegen die de VOORNAAM en NAAM uit de database haalt als deze in de database zit!
                //TODO: random link voor paswoord resetten!
                wachtwoordMail.ZendPaswoordResetMail("DEZE STRING MOET VOORNAAM + NAAM BEVATTEN", email, "HIERIN LINK VOOR PASWOORD RESET");
            }
        }
    }
}