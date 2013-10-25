﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Text.RegularExpressions;

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

        public static List<int> GetData()
        {
            String pad = ConfigDB.getPad();
            var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");
            conn.Open();
            List<int> lijst = new List<int>();
            
            //var conn = new SQLiteConnection(@"data source=E:\Users\Tim\Documents\Bedrijfontwikkelshit\Examenmonitor\codeandballs\Examenmonitor\Database\db");
            var cmd = conn.CreateCommand();
            
            cmd.CommandText = "SELECT * FROM tblUsers";
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

            
            //cmd.CommandText = "INSERT INTO tblUsers (actief,email,wachtwoord,achternaam,voornaam,id)VALUES (actief,email,wachtwoord,achternaam,voornaam,id)";
            string SQL = "INSERT INTO tblUsers (actief, email,wachtwoord,achternaam,voornaam) VALUES";
            SQL += "(0, '"+email+"','"+wachtwoord+"','"+achternaam+"','"+voornaam+"')";

            cmd.CommandText = SQL;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        
    }
}