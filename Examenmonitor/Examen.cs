﻿using System;
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
        public DateTime Einddatum { get; set; }
        public int Capaciteit { get; set; }
        public bool Gereserveerd { get; set; }
        public bool Digitaal { get; set; }
        public string Locatie { get; set; }
        public int Id { get; set; }
        public List<Reservatie> Reservaties { get; set; } 

        //TODO
        public Examen(DateTime datum, DateTime einddatum, int capaciteit, bool gereserveerd, bool digitaal, string locatie, int id, List<Reservatie> reservaties)
        {
            this.Datum = datum;
            this.Einddatum = einddatum;
            this.Capaciteit = capaciteit;
            this.Gereserveerd = gereserveerd;
            this.Digitaal = digitaal;
            this.Locatie = locatie;
            this.Id = id;
            this.Reservaties = reservaties;
        }

        public bool Equals(Examen obj)
        {
            return obj.Id == this.Id;            
        }
    }
}