﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;

namespace Examenmonitor
{
    /* het examen model is een singleton die instaat voor het opvragen van alle examen gerelateerde data */
    /* singleton opgebouwd om lock fouten te voorkomen */
    public class ExamenModel
    {
        //velden van de klasse
        private static ExamenModel model = new ExamenModel();
        private List<Examen> examens;

        //Singleton constructor
        private ExamenModel()
        {
            this.examens = new List<Examen>();
        }

        //Instantie van het model verkrijgen
        public static ExamenModel getInstance() 
        {
            return model;
        }

        //Asynchroon de data reloaden
        public static void ReloadDataAsync()
        {
            ExamenModel.Init(ExamenModel.Work);  
        }

        //Reloaden van de data.
        public static void ReloadData()
        {
            ExamenModel.Work();
        }

        //Geeft een lijst weer van examens.
        public static List<Examen> getExamens()
        {
            return ExamenModel.getInstance().examens;
        }

        //Databank connectie en conversie van de key value pairs in een lijst.
        private List<Examen> DatabankExamensToList() 
        {
            //alle examens halen uit de slots tabel
            List<Examen> lijst = new List<Examen>();
            Examen examen;
            string SQL = "SELECT * FROM tblSlots";
            DBController controller = new DBController(SQL);            
            List<List<KeyValuePair<string, string>> > resultset = controller.ExecuteReaderQueryReturnMultipleResultsMultipleRow("id", "datum", "lengte", "capaciteit", "digitaal", "locatie");

            //deze gegevens uit de lijst halen en in de lijst met examens steken
            foreach (List<KeyValuePair<string,string>> row in resultset)
            {
                examen = new Examen();
                foreach (KeyValuePair<string,string> waarde in row)
                {
                    switch (waarde.Key)
                    {
                        case "id":
                            examen.Id = int.Parse(waarde.Value);
                            break;
                        case "datum":
                            examen.Datum = IOConverter.StringDatumNaarDateTime(waarde.Value);
                            break;                        
                        case "lengte":
                            examen.Lengte = double.Parse(waarde.Value);
                            break;
                        case "capaciteit":
                            examen.Capaciteit = int.Parse(waarde.Value);
                            break;                        
                        case "digitaal":
                            examen.Digitaal = int.Parse(waarde.Value) == 1;
                            break;
                        case "locatie":
                            examen.Locatie = waarde.Value;
                            break;
                    }                    
                }
                lijst.Add(examen);
            }

            return lijst;
        }

        //Haalt de locaties op, steekt ze in een lijst en filtert alle dubbele locaties eruit.
        public List<string> GetAllLocaties()
        {
            List<string> lijst = new List<string>();
            bool compare = false;

            foreach (var examen in this.examens)
            {
                string locatie = examen.Locatie;
                foreach (string reedsOpgeslagenObject in lijst)
                {
                    if (reedsOpgeslagenObject.Equals(locatie))
                    {
                        compare = true;
                    }
                }
                if (!compare)
                {
                    lijst.Add(locatie);
                }
                compare = false;
            }
            
            return lijst;
        }

        public delegate void Worker();
        private static Thread worker;

        public static void Init(Worker work)
        {
            worker = new Thread(new ThreadStart(work));
            worker.Start();
        }

        public static void Work()
        {
            ExamenModel model = ExamenModel.getInstance();
            model.examens = model.DatabankExamensToList();
            foreach (Examen ex in model.examens)
            {
                ex.Reservaties = model.GetReservatiesInExamen(ex.Id);
            }
        }

        //Haalt alle reservaties voor een bepaald examen.
        private List<Reservatie> GetReservatiesInExamen(int examenid)
        {
            //ophalen van alle reservaties uit de tabel
            List<Reservatie> lijst = new List<Reservatie>();
            Reservatie reservatie;
            string SQL = "SELECT * FROM tblReservations WHERE slotid = '" + examenid + "'";
            DBController controller = new DBController(SQL);
            List<List<KeyValuePair<string, string>>> resultset = controller.ExecuteReaderQueryReturnMultipleResultsMultipleRow("id", "slotid", "email", "creatiedatum");

            //de opgehaalde gegevens in een lijst steken
            foreach (List<KeyValuePair<string, string>> row in resultset)
            {
                reservatie = new Reservatie();
                foreach (KeyValuePair<string, string> waarde in row)
                {
                    switch (waarde.Key)
                    {
                        case "id":
                            reservatie.Id = int.Parse(waarde.Value);
                            break;
                        case "creatiedatum":
                            reservatie.CreatieDatum = IOConverter.StringDatumNaarDateTime(waarde.Value);
                            break;
                        case "email":
                            reservatie.Usermail = waarde.Value;
                            break;
                        case "slotid":
                            reservatie.ExamenId = int.Parse(waarde.Value);
                            break;
                    }
                }
                lijst.Add(reservatie);
            }
            return lijst;
        }
    }
}