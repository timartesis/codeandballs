using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace Examenmonitor
{
    public static class DatabankConnector
    {
        //Haalt alle html tags uit een string
        private static string SanitizeHtml(string html)
        {
            string acceptable = "";
            string stringPattern = @"</?(?(?=" + acceptable + @")notag|[a-zA-Z0-9]+)(?:\s[a-zA-Z0-9\-]+=?(?:(["",']?).*?\1?)?)*\s*/?>";
            return Regex.Replace(html, stringPattern,"");
        }

        //zet een stuk tekst om in onomkeerbare sha256 string
        private static string getHashSha256(string text)
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

        //Stuur een hash + ee, ongehashed passwoord mee om deze te vergelijken
        public static bool vergelijkPasswoorden(string serverHash, string ingegevenPasswoord) 
        {
            string clientHash = getHashSha256(ingegevenPasswoord);
            return serverHash.Equals(clientHash);
        }

        //genereert een activatiehash op basis van email en random getal tussen 0.0 en 1.0
        private static string genereerActivatieHash(string email)
        {
            Random generator = new Random();            
            string randomString = generator.NextDouble().ToString();
            return getHashSha256(email + randomString);
        }

        //krijg de huidige systeemtijd terug
        public static string GetHuidigeDatum()
        {
            string datum = DateTime.Today.ToShortDateString();
            datum += " " + DateTime.Now.ToString("HH:mm:ss");
            return datum;
        }

        //slaagt de registratie gegevens op en stuurt da activatiehash terug die in de mail kan worden gebruikt
        public static string RegistratieMail(string email)
        {
            String pad = ConfigDB.getPad();
            var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");
            conn.Open();

            string datum = GetHuidigeDatum();

            string activatieHash = genereerActivatieHash(email);

            string SQL = "INSERT INTO tblActivatie (actief,datum,email,activatieHash) VALUES";
            SQL += "(1, '" + datum + "','" + email + "','" + activatieHash + "')";

            var cmd = conn.CreateCommand();
            cmd.CommandText = SQL;
            cmd.ExecuteNonQuery();
            conn.Close();
            
            return activatieHash;
        }

        //neemt een lijst me keys + values uit een databank en zet deze om in een printbare string
        public static List<string> PrintKeysAndValues(NameValueCollection myCol)
        {
            List<string> lijst = new List<string>();

            foreach (String s in myCol.AllKeys)
            {
                lijst.Add(s + " " + myCol[s]);
            }
            return lijst;
        }
        
        //voorbeeld code voor connecties, NIET GEBRUIKEN IN PRODUCTIE
        public static NameValueCollection GetData() 
        {
            String pad = ConfigDB.getPad();
            var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");
            conn.Open();
            List<string> lijst = new List<string>();
            
            //var conn = new SQLiteConnection(@"data source=E:\Users\Tim\Documents\Bedrijfontwikkelshit\Examenmonitor\codeandballs\Examenmonitor\Database\db");
            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT last_insert_rowid()";
            var reader = cmd.ExecuteReader();
            NameValueCollection col = reader.GetValues();
            conn.Close();
            return col;
        }

        //voegt een gebruiker toe aan de db
        public static void InsertGebruiker( string email, string wachtwoord, string voornaam, string achternaam)
        {
            String pad = ConfigDB.getPad();
            var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");
            conn.Open();

            var cmd = conn.CreateCommand();
            string encryptedWachtwoord = getHashSha256(wachtwoord);
            
            //cmd.CommandText = "INSERT INTO tblUsers (actief,email,wachtwoord,achternaam,voornaam,id)VALUES (actief,email,wachtwoord,achternaam,voornaam,id)";
            string SQL = "INSERT INTO tblUsers (actief, email,wachtwoord,achternaam,voornaam) VALUES";
            SQL += "(0, '" + SanitizeHtml(email) + "','" + encryptedWachtwoord + "','" + SanitizeHtml(achternaam) + "','" + SanitizeHtml(voornaam) + "')";

            cmd.CommandText = SQL;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //als de opgegeven email ongebruikt is geeft deze functie true terug
        public static bool ControleerEmail(string email)
        {
            bool result = true;
            String pad = ConfigDB.getPad();
            var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");
            conn.Open();

            var cmd = conn.CreateCommand();
            

            //cmd.CommandText = "INSERT INTO tblUsers (actief,email,wachtwoord,achternaam,voornaam,id)VALUES (actief,email,wachtwoord,achternaam,voornaam,id)";
            string SQL = "SELECT * FROM tblUsers WHERE email = '"+SanitizeHtml(email)+"'";

            cmd.CommandText = SQL;
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result = false;
            }
            conn.Close();
            return result;
        }        
    }
}