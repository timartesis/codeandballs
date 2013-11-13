using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examenmonitor
{
    public class Examen
    {
        public DateTime Datum { get; set; }
        public DateTime Einddatum { get; set; }
        public int Capaciteit { get; set; }
        public bool Gereserveerd { get; set; }
        public bool Digitaal { get; set; }
        public string Locatie { get; set; }

        //TODO
        public Examen(DateTime datum, DateTime einddatum, int capaciteit, bool gereserveerd, bool digitaal, string locatie)
        {
            this.Datum = datum;
            this.Einddatum = einddatum;
            this.Capaciteit = capaciteit;
            this.Gereserveerd = gereserveerd;
            this.Digitaal = digitaal;
            this.Locatie = locatie;
        }
    }
}