using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Collections.Specialized;

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

        public void ReloadData()
        {
            //haaal data uit DB en renew de examen lijst
        }

        public List<Examen> getExamens()
        {
            return this.examens;
        }

        private static List<Examen> DatabankExamensToList() //databank connectie en conversie van de key value pairs in een lijst
        {
            List<Examen> lijst = new List<Examen>();


            
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
    }
}