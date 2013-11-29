﻿using System;
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
        private List<Examen> origineleLijst = new List<Examen>();
        private static List<Examen> filterLijst;
        private List<CheckBox> checkboxLijst = new List<CheckBox>();
        private string userMail;
        private List<string> Kolomnamen = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valideren of de sessie geldig is
            if (Session["Logged"].Equals("No"))
            {
                Response.Redirect("Login.aspx");
            }
            userMail = Session["User"].ToString();

            //Code voor generatie check boxes
            table.ID = "Filter";
            tableData.ID = "Data view";
            PanelFilter.Controls.Add(table);
            PanelData.Controls.Add(tableData);

            //Locaties ophalen uit de DataBase via ExamenModel
            ExamenModel.ReloadData();
            ExamenModel ex = ExamenModel.getInstance();

            //Methode oproepen om checkboxes te genereren voor de locaties van mogelijke examens.
            List<string> locaties = ex.GetAllLocaties();
            //Manueel toevoegen van extra checkboxen.
            locaties.Add("Mijn reservaties");
            locaties.Add("Vrije plaatsen");
            //Genereert checkboxen op basis van een lijst met locaties.
            this.GenerateCheckBoxes(locaties);
            //Steekt alle examens in een lijst.
            this.origineleLijst = ExamenModel.getExamens();
            
            //Als de filterlijst geen initialisatie heeft, wordt deze gelinked aan de originele lijst.
            if (filterLijst == null)
            {
                filterLijst = origineleLijst;
            }

            //Methode om alle data weer te geven.
            this.InitDataView(filterLijst);
        }

        //Methode om locatie checkboxen te genereren.
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
                cb.AutoPostBack = true;
                cb.Width = 200;
                cb.Text = item.ToString();
                cb.CheckedChanged += new EventHandler(this.CheckedChangeFilter);
                // Add the control to the TableCell
                tempCell.Controls.Add(cb);
                row.Cells.Add(tempCell);
                checkboxLijst.Add(cb);
            }
            table.Rows.Add(row);
        }

        //Methode om de tabel te maken en opvullen met gegevens.
        private void InitDataView(List<Examen> lijst)
        {
            //Leegmaken van de tabel voor hem opnieuw te renderen
            tableData.Rows.Clear();

            //Sorteerbuttons toevoegen en een header toevoegen.
            tableData.Rows.Add(this.GenerateSortButtons());
            tableData.Rows.Add(this.GenerateHeaderRow());

            foreach (var item in lijst)
            {
                //Rij aanmaken per row uit de databank.
                TableRow tempRow = new TableRow();

                //Toont de locaties
                TableCell locatieCell = new TableCell();
                locatieCell.ID = "Locatie" + item.Id;
                locatieCell.Text = item.Locatie;

                //Datum in tabel weergeven
                TableCell datumCell = new TableCell();
                datumCell.ID = "Datum" + item.Id;
                datumCell.Text = item.HaalDatumOp();

                //Beginuur in tabel weergeven
                TableCell beginUurCell = new TableCell();
                beginUurCell.ID = "BeginUur" + item.Id;
                beginUurCell.Text = item.HaalBeginUurOp();

                //Einduur in tabel weergeven
                TableCell EindUurCell = new TableCell();
                EindUurCell.ID = "EindUur" + item.Id;
                EindUurCell.Text = item.HaalEindUurOp();

                //Duur van een examen in de tabel weergeven
                TableCell DuurCell = new TableCell();
                DuurCell.ID = "Duur" + item.Id;
                DuurCell.Text = item.Lengte.ToString();

                //Weergeven of het een digitaal examen is of niet
                TableCell DigitaalCell = new TableCell();
                DigitaalCell.ID = "Digitaal" + item.Id;
                DigitaalCell.Text = item.IsDigitaal();

                //Verhouding totale plaatsen tegenover vrije plaatsen
                TableCell TotaalVrijCell = new TableCell();
                TotaalVrijCell.ID = "TotaalVrij" + item.Id;
                TotaalVrijCell.Text = item.VrijeSlots();

                //Mogelijkheid tot reserveren van het examen, dmv een checkbox
                TableCell ReserverenCell = new TableCell();
                ReserverenCell.ID = "Reserveren" + item.Id;
                CheckBox check = new CheckBox();
                check.ID = item.Id.ToString();
                check.AutoPostBack = true;
                check.CheckedChanged += new EventHandler(this.CheckedChangeData);
                ReserverenCell.Controls.Add(check);
                
                //Als de kolom met het aantal vrije plaatsen "volzet" is, wordt de checkbox niet meer klikbaar.
                if (item.VrijeSlots().Equals("Volzet"))
                {
                    check.Enabled = false;
                    foreach (Reservatie res in item.Reservaties)
                    {
                        if (res.Usermail.Equals(userMail))
                        {
                            check.Enabled = true;
                        }
                    }
                }

                //Checken of de reservaties overeenkomen met de ingelogde user, indien dit het geval is, zal elke reservatie gechecked worden.
                foreach (Reservatie res in item.Reservaties)
                {
                    if (res.Usermail.Equals(userMail))
                    {
                        check.Checked = true;
                    }
                }

                //Cellen toevoegen aan een rij = 1 examen
                tempRow.Cells.Add(locatieCell);
                tempRow.Cells.Add(datumCell);
                tempRow.Cells.Add(beginUurCell);
                tempRow.Cells.Add(EindUurCell);
                tempRow.Cells.Add(DuurCell);
                tempRow.Cells.Add(DigitaalCell);
                tempRow.Cells.Add(TotaalVrijCell);
                tempRow.Cells.Add(ReserverenCell);

                //Rij toevoegen aan de tabel
                tableData.Rows.Add(tempRow);
            }
        }

        //Genereren van de rij, met daarin de header elementen.
        private TableRow GenerateHeaderRow()
        {
            //Heading row maken
            TableRow row = new TableRow();
            row.ID = "Heading";

            //Maken van de nodige cellen
            TableCell locatieCell = new TableCell();
            TableCell datumCell = new TableCell();
            TableCell beginUurCell = new TableCell();
            TableCell EindUurCell = new TableCell();
            TableCell DuurCell = new TableCell();
            TableCell DigitaalCell = new TableCell();
            TableCell TotaalVrijCell = new TableCell();
            TableCell ReserverenCell = new TableCell();

            //Locatie cell
            locatieCell.ID = "Locatie";
            Label locatieLabel = new Label();
            locatieLabel.ID = "locatielabel";
            locatieLabel.Width = 200;
            locatieLabel.Text = "Locatie";
            locatieLabel.Font.Bold = true;
            locatieCell.Controls.Add(locatieLabel);

            //Datum cell
            datumCell.ID = "Datum";
            Label datumLabel = new Label();
            datumLabel.ID = "datumlabel";
            datumLabel.Width = 200;
            datumLabel.Text = "Datum";
            datumLabel.Font.Bold = true;
            datumCell.Controls.Add(datumLabel);

            //Begin uur cell
            beginUurCell.ID = "Begin uur";
            Label beginUurLabel = new Label();
            beginUurLabel.ID = "beginUurlabel";
            beginUurLabel.Width = 200;
            beginUurLabel.Text = "Begin uur";
            beginUurLabel.Font.Bold = true;
            beginUurCell.Controls.Add(beginUurLabel);

            //Eind uur cell
            EindUurCell.ID = "Eind uur";
            Label EindUurLabel = new Label();
            EindUurLabel.ID = "EindUurlabel";
            EindUurLabel.Width = 200;
            EindUurLabel.Text = "Eind uur";
            EindUurLabel.Font.Bold = true;
            EindUurCell.Controls.Add(EindUurLabel);

            //Duur cell
            DuurCell.ID = "Duur";
            Label duurLabel = new Label();
            duurLabel.ID = "duurlabel";
            duurLabel.Width = 200;
            duurLabel.Text = "Duur";
            duurLabel.Font.Bold = true;
            DuurCell.Controls.Add(duurLabel);

            //Digitaal cell
            DigitaalCell.ID = "Digitaal";
            Label digitaalLabel = new Label();
            digitaalLabel.ID = "digitaallabel";
            digitaalLabel.Width = 200;
            digitaalLabel.Text = "Digitaal";
            digitaalLabel.Font.Bold = true;
            DigitaalCell.Controls.Add(digitaalLabel);

            //TotaalVrije cell
            TotaalVrijCell.ID = "TotaalVrij";
            Label totaalVrijLabel = new Label();
            totaalVrijLabel.ID = "totaalVrijlabel";
            totaalVrijLabel.Width = 200;
            totaalVrijLabel.Text = "Bezet/Totaal";
            totaalVrijLabel.Font.Bold = true;
            TotaalVrijCell.Controls.Add(totaalVrijLabel);

            //Reserveren cell
            ReserverenCell.ID = "Reserveren";
            Label reserverenLabel = new Label();
            reserverenLabel.ID = "reserverenlabel";
            reserverenLabel.Width = 200;
            reserverenLabel.Text = "Reserveren";
            reserverenLabel.Font.Bold = true;
            ReserverenCell.Controls.Add(reserverenLabel);

            //Cellen toevoegen aan de row
            row.Cells.Add(locatieCell);
            row.Cells.Add(datumCell);
            row.Cells.Add(beginUurCell);
            row.Cells.Add(EindUurCell);
            row.Cells.Add(DuurCell);
            row.Cells.Add(DigitaalCell);
            row.Cells.Add(TotaalVrijCell);
            row.Cells.Add(ReserverenCell);

            return row;
        }

        //Genereert een rij, waarin de sorteer buttons geplaatst worden.
        private TableRow GenerateSortButtons()
        {
            Kolomnamen.Add("Locatie");
            Kolomnamen.Add("Datum");
            Kolomnamen.Add("BeginUur");
            Kolomnamen.Add("EindUur");
            Kolomnamen.Add("Duur");
            Kolomnamen.Add("Digitaal");
            Kolomnamen.Add("TotaalVrij");
            TableRow row = new TableRow();

            for (int i = 0; i <= 6; i++)
            {
                //Genereert een cell voor elke kolom, waarin 2 buttons komen.
                TableCell tempCell = new TableCell();
                tempCell.ID = "Sort" + i;
                //Aflopende button per header toevoegen
                Button btn1 = new Button();
                btn1.Click += new EventHandler(SorteerButton_Click);
                //Oplopende button per header toevoegen
                Button btn2 = new Button();
                btn2.Click += new EventHandler(SorteerButton_Click);
                btn1.ID = "Aflopend" + Kolomnamen[i];
                btn2.ID = "Oplopend" + Kolomnamen[i];

                //Settings goed zetten voor de buttons
                btn1.Width = 75;
                btn2.Width = 75;
                btn1.Text = "Aflopend";
                btn2.Text = "Oplopend";
                
                //Toevoegen van de buttons aan de cellen en rij
                tempCell.Controls.Add(btn1);
                tempCell.Controls.Add(btn2);
                row.Cells.Add(tempCell);
            }

            return row;
        }

        //Filteren op basis van de geselecteerde checkboxen.
        protected void Filteren()
        {
            filterLijst = origineleLijst;

            //Filteren indien volzet filter gechecked is
            if (checkboxLijst[checkboxLijst.Count - 1].Checked) 
            {
                filterLijst = FilterModel.filterExamensFullCapacity(filterLijst);
            }

            //Filteren indien eigen reservatie filter gechecked is
            if (checkboxLijst[checkboxLijst.Count - 2].Checked) 
            {
                filterLijst = FilterModel.filterExamensID(filterLijst, Session["User"].ToString());
            }

            //Aantal locaties die getoond worden
            int aantalLocaties = checkboxLijst.Count - 2; 
            List<string> locaties = new List<string>();

            //Zetten van locaties die gechecked zijn in een lijst
            for (int i = 0; i < aantalLocaties; i++) 
            {
                if (checkboxLijst[i].Checked)
                {
                    locaties.Add(checkboxLijst[i].Text);
                }
            }

            //Als er locaties gechecked zijn, zal het filteren ervan hier plaatsnemen
            if (locaties.Count > 0) 
            {
                filterLijst = FilterModel.filterExamensCities(filterLijst, locaties);
            }

            //Updaten van de data voor de gebruiker.
            InitDataView(filterLijst);
        }

        //Event voor de checkboxes van de filter
        protected void CheckedChangeFilter(object sender, EventArgs e)
        {
            Filteren();
        }

        //Event voor de checkboxes van de Dataview
        protected void CheckedChangeData(object sender, EventArgs e)
        {
            CheckBox ch = (CheckBox)sender;
            if (ch.Checked == true)
            {
                //Reservatie toevoegen aan de database.
                DatabankConnector.addReservation(filterLijst, userMail, int.Parse(ch.ID));
            }
            else
            {
                //Reservatie verwijderen uit de database.
                DatabankConnector.removeReservation(filterLijst, userMail, int.Parse(ch.ID));
            }
            //Updaten van de data voor de gebruiker.
            InitDataView(filterLijst);
        }

        //Event voor het klikken van een sorteerbutton.
        protected void SorteerButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Sorteren(b.ID);
        }

        //Sorteer methode die de sorteermethode aanspreekt van ons sorteermodel.
        protected void Sorteren(string id)
        {
            filterLijst = SorteerModel.Sorteer(filterLijst, id);
            InitDataView(filterLijst);
        }

        //Event voor uit te loggen.
        //Indien erop geklikt wordt, zal de session leeggemaakt worden en zal de gebruiker doorverwezen worden naar de login pagina.
        protected void LinkButton_Click(Object sender, EventArgs e) 
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("Login.aspx"); 
        }
    }
}