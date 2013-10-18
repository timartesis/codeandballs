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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void wwValidator(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = Validatie.ControleerWachtwoord(e.ToString());
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void CustomValidatorVoorNaam_ServerValidate(object source, ServerValidateEventArgs e)
        {
            e.IsValid = Validatie.ControleerNaam(e.Value);
            //e.IsValid = false;
        }

        protected void registeerButton_Click(object sender, EventArgs e)
        {
        
        }
    }
}