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
        private Table tableData = new Table();

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
            table.ID = "Filter";
            tableData.ID = "Data view";
            PanelFilter.Controls.Add(table);
            PanelData.Controls.Add(tableData);
            //Locaties ophalen uit de DataBase via ExamenModel
            ExamenModel.ReloadData();
            ExamenModel ex = ExamenModel.getInstance();

            //Methode oproepen om checkboxes te genereren
            List<string> locaties = ex.GetAllLocaties();
            locaties.Add("Mijn reservaties");
            locaties.Add("Geen volzet tonen");
            this.GenerateCheckBoxes(locaties);

            //Methode om alle data te showen
            this.InitDataView(ExamenModel.getExamens());
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

        private void InitDataView(List<Examen> lijst)
        {
            //Sorteer optie row maken
            //Heading row maken
            TableRow row = new TableRow();
            row.ID = "Heading";
            

            foreach (var item in lijst)
            {
                TableRow tempRow = new TableRow();
            }
        }

        private TableRow GenerateHeaderRow()
        {
            //Heading row maken
            TableRow row = new TableRow();
            row.ID = "Heading";
            //maken van de nodige cellen
            TableCell datumCell = new TableCell();
            TableCell beginUurCell = new TableCell();
            TableCell EindUurCell = new TableCell();
            TableCell DuurCell = new TableCell();
            TableCell DigitaalCell = new TableCell();
            TableCell TotaalVrijCell = new TableCell();
            TableCell ReserverenCell = new TableCell();

            //Opmaken van de cellen
            //Datum cell
            datumCell.ID = "Datum";
            Label datumLabel = new Label();
            datumLabel.ID = "datumlabel";
            datumLabel.Width = 200;
            datumLabel.Text = "Datum";
            datumCell.Controls.Add(datumLabel);

            //Begin uur cell
            beginUurCell.ID = "Datum";
            Label beginUurLabel = new Label();
            beginUurLabel.ID = "beginUurlabel";
            beginUurLabel.Width = 200;
            beginUurLabel.Text = "Begin uur";
            beginUurCell.Controls.Add(beginUurLabel);

            //Eind uur cell
            EindUurCell.ID = "Eind uur";
            Label EindUurLabel = new Label();
            EindUurLabel.ID = "EindUurlabel";
            EindUurLabel.Width = 200;
            EindUurLabel.Text = "Eind uur";
            EindUurCell.Controls.Add(EindUurLabel);

            //Cellen toevoegen aan de row
            row.Cells.Add(datumCell);
            row.Cells.Add(beginUurCell);
            row.Cells.Add(EindUurCell);
            row.Cells.Add(DuurCell);
            row.Cells.Add(DigitaalCell);
            row.Cells.Add(TotaalVrijCell);
            row.Cells.Add(ReserverenCell);

            return row;
        }

    }
}