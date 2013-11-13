using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Collections.Specialized;

namespace Examenmonitor
{
    public class ExamenModel
    {
        private List<Examen> examens;

        public ExamenModel()
        {
            this.examens = new List<Examen>();
        }

        public void ReloadData()
        {
            //haaal data uit DB en renew de examen lijst
        }

        public List<Examen> getExamens()
        {
            return this.examens;
        }

        private static List<Examen> DatabankExamensToList() 
        {
            List<Examen> lijst = new List<Examen>();


            
            return lijst;
        }

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
    }
}