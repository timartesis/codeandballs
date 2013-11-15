using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examenmonitor
{
    public class Reservatie
    {
        public int Id { get; set; }
        public int ExamenId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatieDatum { get; set; }

        public Reservatie()
        {
        }

        public Reservatie(int id, int examenid, int userid, DateTime creatiedatum)
        {
            this.Id = id;
            this.ExamenId = examenid;
            this.UserId = userid;
            this.CreatieDatum = creatiedatum;
        }

        public bool Equals(Reservatie obj)
        {
            return obj.Id == this.Id;
        }

        public override string ToString()
        {
            return "Datum: " + CreatieDatum.ToShortDateString() + " id: " + Id + " examenid: " + ExamenId + " userid: " + UserId;
        }
    }
}