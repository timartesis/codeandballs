using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;

namespace Examenmonitor
{
    public static class DatabankConnector
    {
        public static List<int> test()
        {
            String pad = System.Environment.CurrentDirectory + "/Database/db.db";
            List<int> lijst = new List<int>();
            var conn = new SQLiteConnection(@"Data Source=" + pad);
            var cmd = conn.CreateCommand();
            conn.Open();
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
        
    }
}