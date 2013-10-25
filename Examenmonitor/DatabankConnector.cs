using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

namespace Examenmonitor
{
    public static class DatabankConnector
    {
        private static string SanitizeHtml(string html)
        {
            string acceptable = "";
            string stringPattern = @"</?(?(?=" + acceptable + @")notag|[a-zA-Z0-9]+)(?:\s[a-zA-Z0-9\-]+=?(?:(["",']?).*?\1?)?)*\s*/?>";
            return Regex.Replace(html, stringPattern,"");
        }

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

        public static bool vergelijkPasswoorden(string serverHash, string ingegevenPasswoord) 
        {
            string clientHash = getHashSha256(ingegevenPasswoord);
            return serverHash.Equals(clientHash);
        }

        private static string genereerActivatieHash(string email)
        {
            Random generator = new Random();            
            string randomString = generator.NextDouble().ToString();
            return getHashSha256(email + randomString);
        }

        public static void RegistratieMail(string email)
        {
            String pad = ConfigDB.getPad();
            var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");
            conn.Open();

            string datum = DateTime.Today.ToShortDateString();
            datum += " " + DateTime.Now.ToString("HH:mm:ss");

            string activatieHash = genereerActivatieHash(email);

            string SQL = "INSERT INTO tblActivatie (actief,datum,email,activatieHash) VALUES";
            SQL += "(1, '" + datum + "','" + email + "','" + activatieHash + "')";

            var cmd = conn.CreateCommand();
            cmd.CommandText = SQL;
            cmd.ExecuteNonQuery();
            conn.Close();
            
        }
        
        //voorbeeld code voor connecties, NIET GEBRUIKEN IN PRODUCTIE
        public static List<int> GetData() 
        {
            String pad = ConfigDB.getPad();
            var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");
            conn.Open();
            List<int> lijst = new List<int>();
            
            //var conn = new SQLiteConnection(@"data source=E:\Users\Tim\Documents\Bedrijfontwikkelshit\Examenmonitor\codeandballs\Examenmonitor\Database\db");
            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT last_insert_rowid()";
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
            int id = reader.GetInt32(reader.GetOrdinal("id"));
            lijst.Add(id);
            }
            conn.Close();
            return lijst;
        }

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
        
    }
}