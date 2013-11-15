using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examenmonitor
{
    public class Examen
    {
        //Examen naam?????
        //to string methode voor mooie formatering
        public DateTime Datum { get; set; }
        public double Lengte { get; set; }
        public int Capaciteit { get; set; }        
        public bool Digitaal { get; set; }
        public string Locatie { get; set; }
        public int Id { get; set; }
        public List<Reservatie> Reservaties { get; set; }

        public Examen()
        {

        }
        
        public Examen(DateTime datum, double lengte, int capaciteit, bool digitaal, string locatie, int id)
        {
            this.Datum = datum;
            this.Lengte = lengte;
            this.Capaciteit = capaciteit;
            
            this.Digitaal = digitaal;
            this.Locatie = locatie;
            this.Id = id;
            this.Reservaties = new List<Reservatie>();
        }

        public bool Equals(Examen obj)
        {
            return obj.Id == this.Id;            
        }

        public override string ToString()
        {
            return "Datum: " + Datum.ToShortDateString() + " lengte: " + Lengte + " capaciteit: " + Capaciteit + " digitaal: " + Digitaal +" locatie: "+ Locatie + " id: " + Id.ToString();
        }
    }
}