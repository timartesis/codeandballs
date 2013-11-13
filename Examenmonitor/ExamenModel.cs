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

        private static List<Examen> DatabankExamensToList(ArrayList data) //bestaat uit 2 arraylists, 1 voor de rows en 1 voor de data
        {
            List<Examen> lijst = new List<Examen>();
            int teller;

            foreach (ArrayList row in data)
            {
                teller = 0;
                foreach (string value in row)
                {
                    //case schrijven met values
                    //hashmaps uitzoeken
                    //keyvaluepairs

                    teller++;
                }
            }

            return lijst;

        }
    }
}