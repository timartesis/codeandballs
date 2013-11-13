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

        //Haalt het email adress van iemand die pass reset heeft aangevraagd uit de Passreset tabel
        public static string getEmailTroughPassResetHash(string hash)
        {
            string result = "";
            string SQL = "SELECT * FROM tblPassreset WHERE activatiehash = '" + SanitizeHtml(hash) + "'";
            DBController controller = new DBController(SQL);
            result = controller.ExecuteReaderQueryReturnSingleString("email");

            /*
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();
                SQL = "SELECT * FROM tblPassreset WHERE activatiehash = '" + SanitizeHtml(hash) + "'";
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) {
                            result = reader.GetString(reader.GetOrdinal("email"));
                        }                        
                    }
                }
            } */
           
            return result;
        }
        
        //veranderd het paswoord van een bepaalde user
        public static void changePassword(string email, string pass) {            
            string hash = getHashSha256(SanitizeHtml(pass));
            string SQL = "UPDATE tblUsers SET wachtwoord='" + hash + "' WHERE email = '" + SanitizeHtml(email) + "'";
            DBController controller = new DBController(SQL);
            controller.ExecuteNonQuery();
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

        //krijg de huidige systeemtijd terug als string
        public static string GetHuidigeDatum()
        {
            string datum = DateTime.Today.ToShortDateString();
            datum += " " + DateTime.Now.ToString("HH:mm:ss");
            return datum;
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

        //slaagt de registratie gegevens op en stuurt da activatiehash terug die in de mail kan worden gebruikt
        public static string RegistratieMail(string email)
        {        
            string datum = GetHuidigeDatum();
            string activatieHash = genereerActivatieHash(email);
            
            String SQL = "UPDATE tblActivatie SET actief = '0' WHERE email = '" + SanitizeHtml(email) + "'";
            DBController controller = new DBController(SQL);
            controller.ExecuteNonQuery();

            SQL = "INSERT INTO tblActivatie (actief,datum,email,activatieHash) VALUES";
            SQL += "(1, '" + datum + "','" + SanitizeHtml(email) + "','" + activatieHash + "')";
            controller.SQL = SQL;            
            controller.ExecuteNonQuery();

            return activatieHash;
        }

        //slaagt de paswoord reset gegevens op en stuurt de activatiehash terug die in de mail kan worden gebruikt
        public static string PassResetMail(string email)
        {
            /*String pad = ConfigDB.getPad();
            var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");
            conn.Open();*/

            string datum = GetHuidigeDatum();

            string activatieHash = genereerActivatieHash(email);

            /*
            //alle andere instanties van deze email op non actief zetten
            var cmd2 = conn.CreateCommand();
            string SQL = "UPDATE tblPassreset SET actief = '0' WHERE email = '" + email + "'";
            cmd2.CommandText = SQL;
            cmd2.ExecuteNonQuery();

            SQL = "INSERT INTO tblPassreset (actief,datum,email,activatieHash) VALUES";
            SQL += "(1, '" + datum + "','" + email + "','" + activatieHash + "')";

            var cmd = conn.CreateCommand();
            cmd.CommandText = SQL;
            cmd.ExecuteNonQuery();



            conn.Close();*/


            string SQL = "UPDATE tblPassreset SET actief = '0' WHERE email = '" + SanitizeHtml(email) + "'";
            DBController controller = new DBController(SQL);
            controller.ExecuteNonQuery();

            /*
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();
                SQL = "UPDATE tblPassreset SET actief = '0' WHERE email = '" + SanitizeHtml(email) + "'";
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {
                    cmd.ExecuteNonQuery();
                }
            }           
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();
                SQL = "INSERT INTO tblPassreset (actief,datum,email,activatieHash) VALUES";
                SQL += "(1, '" + datum + "','" + SanitizeHtml(email) + "','" + activatieHash + "')";
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {
                    cmd.ExecuteNonQuery();
                }
            }*/

            SQL = "INSERT INTO tblPassreset (actief,datum,email,activatieHash) VALUES";
            SQL += "(1, '" + datum + "','" + SanitizeHtml(email) + "','" + activatieHash + "')";
            controller.SQL = SQL;
            controller.ExecuteNonQuery();

            return activatieHash;
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

            string SQL = "INSERT INTO tblUsers (actief, email,wachtwoord,achternaam,voornaam) VALUES";
            SQL += "(0, '" + SanitizeHtml(email) + "','" + encryptedWachtwoord + "','" + SanitizeHtml(achternaam) + "','" + SanitizeHtml(voornaam) + "')";
            DBController controller = new DBController(SQL);
            controller.ExecuteNonQuery();
            /*
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();
                SQL = "INSERT INTO tblUsers (actief, email,wachtwoord,achternaam,voornaam) VALUES";
                SQL += "(0, '" + SanitizeHtml(email) + "','" + encryptedWachtwoord + "','" + SanitizeHtml(achternaam) + "','" + SanitizeHtml(voornaam) + "')";
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {
                    cmd.ExecuteNonQuery();
                }
            }*/
        }

        //als de opgegeven email ongebruikt is geeft deze functie true terug
        public static bool ControleerBestaandeEmail(string email)
        {
            bool result;

            string SQL = "SELECT * FROM tblUsers WHERE email = '" + SanitizeHtml(email) + "'";
            DBController controller = new DBController(SQL);
            result = !controller.ExecuteReaderQueryReturnSingleResult();
            

            return result;
        }

        //als de opgegeven email geactiveerd is, geeft deze functie true terug
        public static bool ControleerActivatieEmail(string email)
        {
            bool result;

            string SQL = "SELECT * FROM tblUsers WHERE email = '" + SanitizeHtml(email) + "' AND actief = '1'";
            DBController controller = new DBController(SQL);
            result = controller.ExecuteReaderQueryReturnSingleResult();
            
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

            /*
            DBController controller = new DBController(SQL);
            string datum = controller.ExecuteReaderQueryReturnMultipleResults("datum,"email");
            DateTime geconverteerdeDatum = StringDatumNaarDateTime(datum);  //omzette naar IO
            DateTime vandaag = StringDatumNaarDateTime(GetHuidigeDatum());
            TimeSpan tijdspanne = vandaag.Subtract(geconverteerdeDatum);
            if (tijdspanne.TotalDays < 2.0)
            {
                result = true;
                mail = reader.GetString(reader.GetOrdinal("email"));
            }

            */
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
            
            if (result)
            {
                SQL = "UPDATE tblUsers SET actief='1' WHERE email = '" + mail + "'";
                DBController controller2 = new DBController(SQL);
                controller2.ExecuteNonQuery();
                /*
                using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
                {
                    c.Open();
                    SQL = "UPDATE tblUsers SET actief='1' WHERE email = '" + mail + "'";
                    using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }*/

                SQL = "UPDATE tblActivatie SET actief='0' WHERE email = '" + mail + "'";
                controller2.ExecuteNonQuery();
                /*
               using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
                {
                    c.Open();
                    SQL = "UPDATE tblActivatie SET actief='0' WHERE email = '" + mail + "'";
                    using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                 * */
            }            
            
            return result;
        }

        //controleert of de hash overeenkomt met aanvraag op passreset
        public static bool ControleerPassresetHash(string hash)
        {
            string mail = "";
            bool result = false;
            String pad = ConfigDB.getPad();
            //var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");
            string SQL = "SELECT * FROM tblPassreset WHERE activatiehash = '" + hash + "' AND actief = '1'";
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
                SQL = "UPDATE tblPassreset SET actief='0' WHERE email = '" + mail + "'";
                DBController controller2 = new DBController(SQL);
                controller2.ExecuteNonQuery();

                /*
                using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
                {
                    c.Open();
                    SQL = "UPDATE tblPassreset SET actief='0' WHERE email = '" + mail + "'";
                    using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }*/
            }

            return result;
        }


        //int 0 = succes, int 1= ongeactiveerd account, int 2= verkeerde login gegevens, int 3 = unexpected error
        public static int login(string email, string passwoord)
        {
            int actief;
            int result = 3;            
            
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

        //halen van naam en voornaam uit DB
        public static string GetVoornaamEnAchternaam(string email)
        {
            string result = "";


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

        public static string GetAllLocaties()
        {
            return "";
        }
    }
}