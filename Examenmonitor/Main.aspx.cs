using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Examenmonitor
{
    public partial class Main : System.Web.UI.Page
    {
        private Table table = new Table();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valideren of de sessie geldig is
            if (Session["Logged"].Equals("No"))
            {
                Response.Redirect("Login.aspx");
            }
            //Debug label TODO verwijderen
            //debugLabel.Text = Session["User"].ToString();

            //Code voor generatie check boxes
            table.ID = "table1";
            Panel1.Controls.Add(table);
            //Locaties ophalen uit de DataBase via ExamenModel
            ExamenModel.ReloadData();
            ExamenModel ex = ExamenModel.getInstance();
            //Methode oproepen om te genereren
            List<string> locaties = ex.GetAllLocaties();
            locaties.Add("Mijn reservaties");
            locaties.Add("Geen volzet tonen");
            this.GenerateCheckBoxes(locaties);
        }

        
        private void GenerateCheckBoxes(List<string> lijst)
        {
            //Teller om te zien wanneer we bij de 3 zijn
            int teller = 0;
            //Nieuwe tabel row maken
            TableRow row = new TableRow();
            foreach (var item in lijst)
            {
                teller++;
                if (teller == 4)
                {
                    table.Rows.Add(row);
                    row = new TableRow();
                    row.ID = "row" + (teller);
                    teller = 0;
                }
                TableCell tempCell = new TableCell();
                tempCell.ID = item.ToString();
                CheckBox cb = new CheckBox();
                cb.ID = "Checkbox " + item.ToString();
                cb.AutoPostBack = false;
                cb.Width = 200;
                cb.Text = item.ToString();
                // Add the control to the TableCell
                tempCell.Controls.Add(cb);
                row.Cells.Add(tempCell);
            }
            table.Rows.Add(row);
        }
    }
}