using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examenmonitor
{
    public class Examen
    {
        //Properties van het Examen object
        public DateTime Datum { get; set; }
        public double Lengte { get; set; }
        public int Capaciteit { get; set; }        
        public bool Digitaal { get; set; }
        public string Locatie { get; set; }
        public int Id { get; set; }
        public List<Reservatie> Reservaties { get; set; }


        //Default constructor
        public Examen()
        {

        }
        
        //Constructor om velden te initialiseren.
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

        //Vergelijken van 2 examen objecten
        public bool Equals(Examen obj)
        {
            return obj.Id == this.Id;            
        }

        //Berekent het aantal vrije slots ten opzichte van het totaal. Wanneer het volzet is, returnt het Volzet, anders returned het iets in de vorm van 1/3
        public string VrijeSlots()
        {
            string vrijeSlots = this.Reservaties.Count + "/" + this.Capaciteit;
                if (this.Reservaties.Count == this.Capaciteit)
                    vrijeSlots = "Volzet";
                return vrijeSlots;
        }

        //Haalt de volledige datum op in het formaat Dag/Maand/Jaar.
        public string HaalDatumOp()
        {
            return Datum.ToString("dd/MM/yyyy");
        }

        //Split de datum en het uur en geeft het begin uur weer.
        public string HaalBeginUurOp()
        {
            return (Datum - Datum.Date).ToString();
        }

        //Berekent het einduur aan de hand van het beginuur en de lengte van een examen.
        public string HaalEindUurOp()
        {
            DateTime eindUur = Datum.AddHours(Lengte);
            return (eindUur - eindUur.Date).ToString();
        }

        //Gaat na op het examen digitaal is of niet.
        public string IsDigitaal()
        {
            string digitaal = "Nee";
            if (Digitaal)
                digitaal = "Ja";
            return digitaal;
        }

        //Genereert een string die een examen netter voorstelt.
        public override string ToString()
        {
            return "Datum: " + Datum.ToShortDateString() + " lengte: " + Lengte + " capaciteit: " + Capaciteit + " digitaal: " + Digitaal +" locatie: "+ Locatie + " id: " + Id.ToString();
        }
    }
}