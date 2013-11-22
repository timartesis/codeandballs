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

        public static List<Examen> filterExamensCities(List<Examen> original, List<string> steden)
        {
            List<Examen> result = new List<Examen>();

            foreach (Examen ex in original)
            {
                foreach (string stad in steden)
                {
                    if (ex.Locatie == stad)
                    {
                        result.Add(ex);
                    }
                }
            }
            return result;
        }

        public static List<Examen> filterExamensID(List<Examen> original, string userMail)
        {
            List<Examen> result = new List<Examen>();

            foreach (Examen ex in original)
            {
                List<Reservatie> reservaties = ex.Reservaties;

                foreach (Reservatie res in reservaties)
                {

                    if (res.Usermail.Equals(userMail))
                    {
                        result.Add(ex);
                    }
                }
            }
            return result;
        }

    }
}