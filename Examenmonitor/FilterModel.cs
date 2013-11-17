using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examenmonitor
{
    public static class FilterModel
    {
        public static List<Examen> filterExamensFullCapacity(List<Examen> original) {
            List<Examen> result = new List<Examen>();

            foreach (Examen ex in original)
            {
                if (ex.Reservaties.Count < ex.Capaciteit)
                {
                    result.Add(ex);
                }
            }
            return result;
        }

        public static List<Examen> filterExamensCities(List<Examen> original, string stad)
        {
            List<Examen> result = new List<Examen>();

            foreach (Examen ex in original)
            {
                if (ex.Locatie == stad)
                {
                    result.Add(ex);
                }
            }
            return result;
        }
    }
}