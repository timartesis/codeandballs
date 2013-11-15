using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;

namespace Examenmonitor
{
    /* het examen model is een singleton die instaat voor het opvragen van alle examen gerelateerde data */
    /* singleton opgebouwd om lock fouten te voorkomen (possie stijl) */
    public class ExamenModel
    {
        private static ExamenModel model = new ExamenModel(); 
        private List<Examen> examens;

        private ExamenModel()
        {
            this.examens = new List<Examen>();
        }

        public static ExamenModel getInstance() { //hiermee verkrijg je de instantie van het model
            return model;
        }

        public static void ReloadDataAsync()
        {
            ExamenModel.Init(ExamenModel.Work);  
        }

        public static void ReloadData()
        {
            ExamenModel.Work();
        }

        public List<Examen> getExamens()
        {
            return this.examens;
        }

        private List<Examen> DatabankExamensToList() //databank connectie en conversie van de key value pairs in een lijst
        {
            List<Examen> lijst = new List<Examen>();
            Examen examen;
            string SQL = "SELECT * FROM tblSlots";
            DBController controller = new DBController(SQL);            
            List<List<KeyValuePair<string, string>>> resultset = controller.ExecuteReaderQueryReturnMultipleResultsMultipleRow("id", "datum", "lengte", "capaciteit", "digitaal", "locatie");

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

        public List<string> GetAllLocaties() //haal uit de lijst alle locaties op en geef deze terug in een string lijst, dubbele locaties worden gefilterd
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
        }
    }
}