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

        protected void naamValidator(object sender, ServerValidateEventArgs e)
        {
            bool check = true;
            for (int i = 0; i > e.ToString().Length; i++)
            {
                if (!(char.IsLetter(e.ToString()[i])))
                {
                    check = false;
                }
            }
            e.IsValid = check;

        }

        protected void wwValidator(object sender, ServerValidateEventArgs e)
        {

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}