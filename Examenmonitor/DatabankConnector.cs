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
    /* voorbeeld code voor connectie pooling
     using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
                {
                    c.Open();
                    SQL = "UPDATE tblUsers SET actief='1' WHERE email = '" + mail + "'";
                    using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                    {
                        cmd.ExecuteNonQuery();
                    }
                } */
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

        //slaagt de registratie gegevens op en stuurt da activatiehash terug die in de mail kan worden gebruikt
        public static string RegistratieMail(string email)
        {
           // String pad = ConfigDB.getPad();
           /* var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");
            conn.Open();*/
            
            string datum = GetHuidigeDatum();

            string activatieHash = genereerActivatieHash(email);
            /*
            //alle andere instanties van deze email op non actief zetten
            var cmd2 = conn.CreateCommand();
            string SQL = "UPDATE tblActivatie SET actief = '0' WHERE email = '" + email + "'";
            cmd2.CommandText = SQL;
            cmd2.ExecuteNonQuery();
            
            SQL = "INSERT INTO tblActivatie (actief,datum,email,activatieHash) VALUES";
            SQL += "(1, '" + datum + "','" + email + "','" + activatieHash + "')";

            var cmd = conn.CreateCommand();
            cmd.CommandText = SQL;
            cmd.ExecuteNonQuery();

            

            conn.Close();*/

            string SQL = "";
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();
                SQL = "UPDATE tblActivatie SET actief = '0' WHERE email = '" + email + "'";
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();
                SQL = "INSERT INTO tblActivatie (actief,datum,email,activatieHash) VALUES";
                SQL += "(1, '" + datum + "','" + email + "','" + activatieHash + "')";
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            return activatieHash;
        }

        //neemt een lijst me keys + values uit een databank en zet deze om in een printbare string

        //handig voor debug code te printen op een scherm uit een db
        public static List<string> PrintKeysAndValues(NameValueCollection myCol)
        {
            List<string> lijst = new List<string>();

            foreach (String s in myCol.AllKeys)
            {
                lijst.Add(s + " " + myCol[s]);
            }
            return lijst;
        }
        
        //voorbeeld code voor masa data op te halen
        public static NameValueCollection GetData() 
        {
            /*String pad = ConfigDB.getPad();
            var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");
            conn.Open();
            List<string> lijst = new List<string>();
            
            //var conn = new SQLiteConnection(@"data source=E:\Users\Tim\Documents\Bedrijfontwikkelshit\Examenmonitor\codeandballs\Examenmonitor\Database\db");
            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT last_insert_rowid()";
            var reader = cmd.ExecuteReader();
            NameValueCollection col = reader.GetValues();
            conn.Close();*/
            NameValueCollection col = null;
            string SQL = "";
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();
                SQL = "SELECT last_insert_rowid()";                
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        col = reader.GetValues();
                    }
                }
            }
            return col;
        }

        //voegt een gebruiker toe aan de db
        public static void InsertGebruiker( string email, string wachtwoord, string voornaam, string achternaam)
        {            
            string encryptedWachtwoord = getHashSha256(wachtwoord);
            /*
            //cmd.CommandText = "INSERT INTO tblUsers (actief,email,wachtwoord,achternaam,voornaam,id)VALUES (actief,email,wachtwoord,achternaam,voornaam,id)";
            string SQL = "INSERT INTO tblUsers (actief, email,wachtwoord,achternaam,voornaam) VALUES";
            SQL += "(0, '" + SanitizeHtml(email) + "','" + encryptedWachtwoord + "','" + SanitizeHtml(achternaam) + "','" + SanitizeHtml(voornaam) + "')";

            cmd.CommandText = SQL;
            cmd.ExecuteNonQuery();
            conn.Close();*/

            string SQL = "";
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();
                SQL = "INSERT INTO tblUsers (actief, email,wachtwoord,achternaam,voornaam) VALUES";
                SQL += "(0, '" + SanitizeHtml(email) + "','" + encryptedWachtwoord + "','" + SanitizeHtml(achternaam) + "','" + SanitizeHtml(voornaam) + "')";
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //als de opgegeven email ongebruikt is geeft deze functie true terug
        public static bool ControleerBestaandeEmail(string email)
        {
            bool result = true;
           /* String pad = ConfigDB.getPad();
            var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");
            conn.Open();

            var cmd = conn.CreateCommand();*/
            

            //cmd.CommandText = "INSERT INTO tblUsers (actief,email,wachtwoord,achternaam,voornaam,id)VALUES (actief,email,wachtwoord,achternaam,voornaam,id)";
            //string SQL = "SELECT * FROM tblUsers WHERE email = '"+SanitizeHtml(email)+"'";

           /* cmd.CommandText = SQL;
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result = false;
            }
            conn.Close();*/
            

            string SQL = "";
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();
                SQL = "SELECT * FROM tblUsers WHERE email = '" + SanitizeHtml(email) + "'";
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = false;
                        }
                    }
                }
            }
            return result;
        }

        //als de opgegeven email geactiveerd is, geeft deze functie true terug
        public static bool ControleerActivatieEmail(string email)
        {
            bool result = false;
           /* String pad = ConfigDB.getPad();
            var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");
            conn.Open();

            var cmd = conn.CreateCommand();

            string SQL = "SELECT * FROM tblUsers WHERE email = '" + SanitizeHtml(email) + "' AND actief = '1'";

            cmd.CommandText = SQL;
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result = true;
            }
            conn.Close();
            return result;*/

            string SQL = "";
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();
                SQL = "SELECT * FROM tblUsers WHERE email = '" + SanitizeHtml(email) + "' AND actief = '1'";
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }


        //controleert of de hash overeen komt met nen mail, 2 dagen odu check en stuurt terug op alle wijzingen zijn gelukt
        public static bool ControleerActivatieHash(string hash)
        {
            string mail = "";
            bool result = false;
            String pad = ConfigDB.getPad();
            //var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");
            string SQL = "SELECT * FROM tblActivatie WHERE activatiehash = '" + hash + "' AND actief = '1'";
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string datum = reader.GetString(reader.GetOrdinal("datum"));
                            DateTime geconverteerdeDatum = StringDatumNaarDateTime(datum);
                            DateTime vandaag = StringDatumNaarDateTime(GetHuidigeDatum());
                            TimeSpan tijdspanne = vandaag.Subtract(geconverteerdeDatum);
                            if (tijdspanne.TotalDays < 2.0)
                            {
                                result = true;
                                mail = reader.GetString(reader.GetOrdinal("email"));
                            }
                        }
                    }
                }
            }
            //conn.Open();

            //var cmd = conn.CreateCommand();


            //cmd.CommandText = "INSERT INTO tblUsers (actief,email,wachtwoord,achternaam,voornaam,id)VALUES (actief,email,wachtwoord,achternaam,voornaam,id)";
            
            /*
            cmd.CommandText = SQL;
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string datum = reader.GetString(reader.GetOrdinal("datum"));
                DateTime geconverteerdeDatum = StringDatumNaarDateTime(datum);
                DateTime vandaag = StringDatumNaarDateTime(GetHuidigeDatum());
                TimeSpan tijdspanne = vandaag.Subtract(geconverteerdeDatum);
                if (tijdspanne.TotalDays < 2.0)
                {
                        result = true;
                        mail = reader.GetString(reader.GetOrdinal("email"));
                }                
            }*/

            if (result)
            {
                using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
                {
                    c.Open();
                    SQL = "UPDATE tblUsers SET actief='1' WHERE email = '" + mail + "'";
                    using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
               using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
                {
                    c.Open();
                    SQL = "UPDATE tblActivatie SET actief='0' WHERE email = '" + mail + "'";
                    using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }            
            
            return result;
        }


        //int 0 = succes, int 1= ongeactiveerd account, int 2= verkeerde login gegevens, int 3 = unexpected error
        public static int login(string email, string passwoord)
        {
            int actief;
            int result = 3;
            /*String pad = ConfigDB.getPad();
            var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");
            conn.Open();
            var cmd = conn.CreateCommand();
            string SQL = "SELECT * FROM tblUsers WHERE email = '" + SanitizeHtml(email) + "'";
            cmd.CommandText = SQL;
            var reader = cmd.ExecuteReader();
            if (reader.HasRows) //controleer of de email in de db zit
            {
                while (reader.Read())
                {
                     actief = reader.GetInt32(reader.GetOrdinal("actief"));
                     if (actief == 1) //controleer of het account actief is
                     {
                         string hash = reader.GetString(reader.GetOrdinal("wachtwoord"));
                         if (vergelijkPasswoorden(hash, passwoord)) //vergelijk de passwoorden
                         {
                             result = 0;
                             return result;
                         }
                         else
                         {
                             result = 2;
                             return result;
                         }
                     }
                     else //account is inactief
                     {
                         result = 1;
                         return result;
                     }
                }
            }
            else //zo niet verkeerde login gegevens
            {
                result = 2;
            }*/
            
            string SQL = "";
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();
                SQL = "SELECT * FROM tblUsers WHERE email = '" + SanitizeHtml(email) + "'";
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                            if (reader.HasRows) //controleer of de email in de db zit
                            {
                                while (reader.Read())
                                {
                                    actief = reader.GetInt32(reader.GetOrdinal("actief"));
                                    if (actief == 1) //controleer of het account actief is
                                    {
                                        string hash = reader.GetString(reader.GetOrdinal("wachtwoord"));
                                        if (vergelijkPasswoorden(hash, passwoord)) //vergelijk de passwoorden
                                        {
                                            result = 0;
                                            return result;
                                        }
                                        else
                                        {
                                            result = 2;
                                            return result;
                                        }
                                    }
                                    else //account is inactief
                                    {
                                        result = 1;
                                        return result;
                                    }
                                }
                            }
                            else //zo niet verkeerde login gegevens
                            {
                                result = 2;
                            }                        
                    }
                }
            }
            return result;
        }

        //halen van naam en voornaam uit DB voor het opnieuw versturen van activatiemail
        public static string GetVoornaamEnAchternaam(string email)
        {
            string result = "";/*
            String pad = ConfigDB.getPad();
            var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");
            conn.Open();

            var cmd = conn.CreateCommand();

            string SQL = "SELECT * FROM tblUsers WHERE email = '" + SanitizeHtml(email) + "'";
            cmd.CommandText = SQL;
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result = reader.GetString(reader.GetOrdinal("voornaam")) + " " + reader.GetString(reader.GetOrdinal("achternaam"));
            }*/


            string SQL = "";
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                SQL = "SELECT * FROM tblUsers WHERE email = '" + SanitizeHtml(email) + "'";
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader.GetString(reader.GetOrdinal("voornaam")) + " " + reader.GetString(reader.GetOrdinal("achternaam"));
                        }
                    }
                }
            }
            return result;
        }
    }
}