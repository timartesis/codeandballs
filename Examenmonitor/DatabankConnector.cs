﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
namespace Examenmonitor
{
    public static class DatabankConnector
    {
        public static void addReservation(string email, int slotid)
        {
            string datum = IOConverter.GetHuidigeDatum();
            string SQL = "INSERT INTO tblReservations (email,slotid,creatiedatum) VALUES";            
            SQL += "('"+IOConverter.SanitizeHtml(email)+"','"+slotid+"','"+datum+"')";
            DBController controller = new DBController(SQL);
            controller.ExecuteNonQuery();
        }

        public static void removeReservation(string email, int slotid)
        {
            string datum = IOConverter.GetHuidigeDatum();
            string SQL = "DELETE FROM tblReservations WHERE email = '"+ IOConverter.SanitizeHtml(email) + "' AND slotid = '" + slotid + "'";            
            DBController controller = new DBController(SQL);
            controller.ExecuteNonQuery();
        }

        //Haalt het email adress van iemand die pass reset heeft aangevraagd uit de Passreset tabel
        public static string getEmailTroughPassResetHash(string hash)
        {
            string result = "";
            string SQL = "SELECT * FROM tblPassreset WHERE activatiehash = '" + IOConverter.SanitizeHtml(hash) + "'";
            DBController controller = new DBController(SQL);
            result = controller.ExecuteReaderQueryReturnSingleString("email");
           
            return result;
        }
        
        //veranderd het paswoord van een bepaalde user
        public static void changePassword(string email, string pass) {            
            string hash = IOConverter.getHashSha256(IOConverter.SanitizeHtml(pass));
            string SQL = "UPDATE tblUsers SET wachtwoord='" + hash + "' WHERE email = '" + IOConverter.SanitizeHtml(email) + "'";
            DBController controller = new DBController(SQL);
            controller.ExecuteNonQuery();
        }

        //Stuur een hash + een ongehashed passwoord mee om deze te vergelijken
        public static bool vergelijkPasswoorden(string serverHash, string ingegevenPasswoord) 
        {
            string clientHash = IOConverter.getHashSha256(ingegevenPasswoord);
            return serverHash.Equals(clientHash);
        }

        //genereert een activatiehash op basis van email en random getal tussen 0.0 en 1.0
        private static string genereerActivatieHash(string email)
        {
            Random generator = new Random();            
            string randomString = generator.NextDouble().ToString();
            return IOConverter.getHashSha256(email + randomString);
        }

        //slaagt de registratie gegevens op en stuurt da activatiehash terug die in de mail kan worden gebruikt
        public static string RegistratieMail(string email)
        {        
            string datum = IOConverter.GetHuidigeDatum();
            string activatieHash = genereerActivatieHash(email);
            
            String SQL = "UPDATE tblActivatie SET actief = '0' WHERE email = '" + IOConverter.SanitizeHtml(email) + "'"; //alle andere mails deactiveren
            DBController controller = new DBController(SQL);
            controller.ExecuteNonQuery();

            SQL = "INSERT INTO tblActivatie (actief,datum,email,activatieHash) VALUES";
            SQL += "(1, '" + datum + "','" + IOConverter.SanitizeHtml(email) + "','" + activatieHash + "')";
            controller.SQL = SQL;            
            controller.ExecuteNonQuery();

            return activatieHash;
        }

        //slaagt de paswoord reset gegevens op en stuurt de activatiehash terug die in de mail kan worden gebruikt
        public static string PassResetMail(string email)
        {
            string datum = IOConverter.GetHuidigeDatum();
            string activatieHash = genereerActivatieHash(email);

            string SQL = "UPDATE tblPassreset SET actief = '0' WHERE email = '" + IOConverter.SanitizeHtml(email) + "'"; //alle andere mails deactiveren
            DBController controller = new DBController(SQL);
            controller.ExecuteNonQuery();

            SQL = "INSERT INTO tblPassreset (actief,datum,email,activatieHash) VALUES";
            SQL += "(1, '" + datum + "','" + IOConverter.SanitizeHtml(email) + "','" + activatieHash + "')";
            controller.SQL = SQL;
            controller.ExecuteNonQuery();

            return activatieHash;
        }      

        //voegt een gebruiker toe aan de db
        public static void InsertGebruiker( string email, string wachtwoord, string voornaam, string achternaam)
        {
            string encryptedWachtwoord = IOConverter.getHashSha256(wachtwoord);
            string SQL = "INSERT INTO tblUsers (actief, email,wachtwoord,achternaam,voornaam) VALUES";
            SQL += "(0, '" + IOConverter.SanitizeHtml(email) + "','" + encryptedWachtwoord + "','" + IOConverter.SanitizeHtml(achternaam) + "','" + IOConverter.SanitizeHtml(voornaam) + "')";
            DBController controller = new DBController(SQL);
            controller.ExecuteNonQuery();            
        }

        //als de opgegeven email ongebruikt is geeft deze functie true terug
        public static bool ControleerBestaandeEmail(string email)
        {
            bool result;

            string SQL = "SELECT * FROM tblUsers WHERE email = '" + IOConverter.SanitizeHtml(email) + "'";
            DBController controller = new DBController(SQL);
            result = !controller.ExecuteReaderQueryReturnSingleResult();
            

            return result;
        }

        //als de opgegeven gebruiker geactiveerd is, geeft deze functie true terug
        public static bool ControleerActivatieEmail(string email)
        {
            bool result;

            string SQL = "SELECT * FROM tblUsers WHERE email = '" + IOConverter.SanitizeHtml(email) + "' AND actief = '1'";
            DBController controller = new DBController(SQL);
            result = controller.ExecuteReaderQueryReturnSingleResult();
            
            return result;
        }


        //controleert of de hash overeen komt met nen mail, 2 dagen odu check en stuurt terug op alle wijzingen zijn gelukt
        //TO DO
        public static bool ControleerActivatieHash(string hash)
        {
            string mail = "";
            bool result = false;
            string SQL = "SELECT * FROM tblActivatie WHERE activatiehash = '" + hash + "' AND actief = '1'";
            DBController controller = new DBController(SQL);
            var resultList = controller.ExecuteReaderQueryReturnMultipleResultsMultipleRow("datum", "email");

            foreach (var lijst in resultList)
            {
                string datum = lijst[0].Value;
                DateTime geconverteerdeDatum = IOConverter.StringDatumNaarDateTime(datum);
                DateTime vandaag = IOConverter.StringDatumNaarDateTime(IOConverter.GetHuidigeDatum());
                TimeSpan tijdspanne = vandaag.Subtract(geconverteerdeDatum);
                if (tijdspanne.TotalDays < 2.0)
                {
                    result = true;
                    mail = lijst[1].Value;
                }
            }
            
            if (result)
            {
                SQL = "UPDATE tblUsers SET actief='1' WHERE email = '" + mail + "'";
                DBController controller2 = new DBController(SQL);
                controller2.ExecuteNonQuery();

                SQL = "UPDATE tblActivatie SET actief='0' WHERE email = '" + mail + "'";
                DBController controller3 = new DBController(SQL);
                controller3.ExecuteNonQuery();
            }            
            
            return result;
        }

        //controleert of de hash overeenkomt met aanvraag op passreset
        //TO DO
        public static bool ControleerPassresetHash(string hash)
        {
            string mail = "";
            bool result = false;
            string SQL = "SELECT * FROM tblPassreset WHERE activatiehash = '" + hash + "' AND actief = '1'";
            DBController controller = new DBController(SQL);
            var resultList = controller.ExecuteReaderQueryReturnMultipleResultsMultipleRow("datum", "email");

            foreach (var lijst in resultList)
            {
                string datum = lijst[0].Value;
                DateTime geconverteerdeDatum = IOConverter.StringDatumNaarDateTime(datum);
                DateTime vandaag = IOConverter.StringDatumNaarDateTime(IOConverter.GetHuidigeDatum());
                TimeSpan tijdspanne = vandaag.Subtract(geconverteerdeDatum);
                if (tijdspanne.TotalDays < 2.0)
                {
                    result = true;
                    mail = lijst[1].Value;
                }
            }

            if (result)
            {
                SQL = "UPDATE tblPassreset SET actief='0' WHERE email = '" + mail + "'";
                DBController controller2 = new DBController(SQL);
                controller2.ExecuteNonQuery();
            }

            return result;
        }


        //int 0 = succes, int 1= ongeactiveerd account, int 2= verkeerde login gegevens, int 3 = unexpected error
        //TO DO
        public static int login(string email, string passwoord)
        {
            int actief;
            int result = 3;            
            string SQL = "SELECT * FROM tblUsers WHERE email = '" + IOConverter.SanitizeHtml(email) + "'";
            DBController controller = new DBController(SQL);
            var resultLijst = controller.ExecuteReaderQueryReturnMultipleResultsMultipleRow("actief","wachtwoord");

            if (resultLijst.Count != 0) //controleer of de email in de db zit
            {
                foreach (var rij in resultLijst)
                {
                    actief =  int.Parse(rij[0].Value);
                    if (actief == 1) //controleer of het account actief is
                    {
                        string hash = rij[1].Value;
                        if (vergelijkPasswoorden(hash, passwoord)) //vergelijken van passwoorden
                        {
                            result = 0;
                            return result;
                        }
                        else //verkeerd passwoord
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

            return result;
        }

        //halen van naam en voornaam uit DB aan de hand van email
        //todo
        public static string GetVoornaamEnAchternaam(string email)
        {
            string result = "";
            string SQL = "SELECT * FROM tblUsers WHERE email = '" + IOConverter.SanitizeHtml(email) + "'";
            DBController controller = new DBController(SQL);
            var resultList = controller.ExecuteReaderQueryReturnMultipleResultsOneRow("voornaam", "achternaam");
            string voornaam = resultList[0].Value;
            string achternaam = resultList[1].Value;
            result = voornaam + " " + achternaam;
            return result;
        }

        //halen van passwoordhash uit de databank aan de hand van de mail
        public static string GetPasswoordMetMail(string email)
        {
            string result = "";
            string SQL = "SELECT wachtwoord FROM tblUsers WHERE email = '" + IOConverter.SanitizeHtml(email) + "'";
            DBController controller = new DBController(SQL);
            result = controller.ExecuteReaderQueryReturnSingleString("wachtwoord");
            return result;
        }

        public static int GetIdMetMail(string email)
        {
            string result = "";
            string SQL = "SELECT id FROM tblUsers WHERE email = '" + IOConverter.SanitizeHtml(email) + "'";
            DBController controller = new DBController(SQL);
            result = controller.ExecuteReaderQueryReturnSingleString("id");
            return int.Parse(result);
        }
    }
}