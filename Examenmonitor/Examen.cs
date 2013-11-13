using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examenmonitor
{
    public class Examen
    {
        //Examen naam?????
        public DateTime Datum { get; set; }
        public DateTime Einddatum { get; set; }
        public int Capaciteit { get; set; }
        public bool Gereserveerd { get; set; }
        public bool Digitaal { get; set; }
        public string Locatie { get; set; }
        public int Id { get; set; }
        public List<Slot> Slots { get; set; } 

        //TODO
        public Examen(DateTime datum, DateTime einddatum, int capaciteit, bool gereserveerd, bool digitaal, string locatie, int id, List<Slot> slots)
        {
            this.Datum = datum;
            this.Einddatum = einddatum;
            this.Capaciteit = capaciteit;
            this.Gereserveerd = gereserveerd;
            this.Digitaal = digitaal;
            this.Locatie = locatie;
            this.Id = id;
            this.Slots = slots;
        }

        public override bool Equals(Examen obj)
        {
            return obj.Id == this.Id;            
        }
    }
}