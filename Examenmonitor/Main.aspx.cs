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
        private int AANTALKOLOMEN = 3;
        public static int ctr = 0;//Counter
        public static int ctr2 = 0;//Counter voor locaties
        static Table table = new Table();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valideren of de sessie geldig is
            if (Session["Logged"].Equals("No"))
            {
                Response.Redirect("Login.aspx");
            }
            //Debug label TODO verwijderen
            debugLabel.Text = Session["User"].ToString();

            //Code voor generatie check boxes
            table.ID = "table1";
            Panel1.Controls.Add(table);
            //Locaties ophalen uit de DataBase via ExamenModel
            ExamenModel ex = ExamenModel.getInstance();
            //Methdoe oproepen om te genereren
            this.GenerateCheckBoxes(ex.GetAllLocaties());
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
                tempCell.ID = lijst[ctr2];
                CheckBox cb = new CheckBox();
                cb.ID = "Checkbox " + item.ToString();
                cb.AutoPostBack = false;
                cb.Width = 10;
                cb.Text = item.ToString();
                // Add the control to the TableCell
                tempCell.Controls.Add(cb);
                row.Cells.Add(tempCell);
            }

            /*for (int i = 0; i < lijst.Count(); i+=3)
            {
                ctr++;
                TableRow row = new TableRow();
                row.ID = "row" + (ctr);
                for (int j = 0; j < 3; j++)
                {
                    ctr2++;
                    TableCell tempCell = new TableCell();
                    tempCell.ID = lijst[ctr2];
                    row.Cells.Add(tempCell);
                }
            }
            ctr++;
            // Now iterate through the table and add your controls
            TableRow row = new TableRow();
            row.ID = "row" + ctr;
            TableCell cell1 = new TableCell();
            // Add the control to the TableCell
            row.Cells.Add(cell1);
            for (int j = 1; j <= colsCount; j++)
            {
                TableCell cell2 = new TableCell();
                TextBox tb = new TextBox();
                if (j == 1)
                {
                    tb.ID = "txtJan" + ctr;
                }
                else if (j == 2)
                {
                    tb.ID = "txtFeb" + ctr;
                }
                else if (j == 3)
                {
                    tb.ID = "txtMar" + ctr;
                }
                else if (j == 4)
                {
                    tb.ID = "txtApr" + ctr;
                }
                else if (j == 5)
                {
                    tb.ID = "txtMay" + ctr;
                }
                else if (j == 6)
                {
                    tb.ID = "txtJun" + ctr;
                }
                else if (j == 7)
                {
                    tb.ID = "txtJul" + ctr;
                }
                else if (j == 8)
                {
                    tb.ID = "txtAug" + ctr;
                }
                else if (j == 9)
                {
                    tb.ID = "txtSep" + ctr;
                }
                else if (j == 10)
                {
                    tb.ID = "txtOct" + ctr;
                }
                else if (j == 11)
                {
                    tb.ID = "txtNov" + ctr;
                }
                else if (j == 12)
                {
                    tb.ID = "txtDec" + ctr;
                }
                tb.Width = 37;
                // Add the control to the TableCell
                cell2.Controls.Add(tb);
                // Add the TableCell to the TableRow
                row.Cells.Add(cell2);
            }
            TableCell cell3 = new TableCell();
            CheckBox cb = new CheckBox();
            cb.Controls.Clear();
            cb.ID = "chkSameAll" + ctr;
            cb.AutoPostBack = false;
            cb.Width = 10;
            // Add the control to the TableCell
            cell3.Controls.Add(cb);
            // Add the TableCell to the TableRow
            row.Cells.Add(cell3);
            // Add the TableRow to the Table
            table.Rows.Add(row);*/
        } 
    }
}