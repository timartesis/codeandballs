using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Cryptography;

namespace Examenmonitor
{
    public static class IOConverter
    {
        //zet een stuk tekst om in onomkeerbare sha256 string
        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        //krijg de huidige systeemtijd terug als string
        public static string GetHuidigeDatum()
        {
            string datum = DateTime.Today.ToShortDateString();
            datum += " " + DateTime.Now.ToString("HH:mm:ss");
            return datum;
        }

        //Haalt alle html tags uit een string
        public static string SanitizeHtml(string html)
        {
            string acceptable = "";
            string stringPattern = @"</?(?(?=" + acceptable + @")notag|[a-zA-Z0-9]+)(?:\s[a-zA-Z0-9\-]+=?(?:(["",']?).*?\1?)?)*\s*/?>";
            return Regex.Replace(html, stringPattern, "");
        }

        //zet de string datum om naar een DateTime
        public static DateTime StringDatumNaarDateTime(string datum)
        {
            string[] splittedDatum = datum.Split(' ');
            string[] splittedDagen = splittedDatum[0].Split('/');
            string[] splittedUren = splittedDatum[1].Split(':');

            int dag = Convert.ToInt32(splittedDagen[0]);
            int maand = Convert.ToInt32(splittedDagen[1]);
            int jaar = Convert.ToInt32(splittedDagen[2]);

            int uur = Convert.ToInt32(splittedUren[0]);
            int minuut = Convert.ToInt32(splittedUren[1]);
            int seconde = Convert.ToInt32(splittedUren[2]);

            DateTime geconverteerdeDatum = new DateTime(jaar, maand, dag, uur, minuut, seconde);
            return geconverteerdeDatum;

        }       

    }
}