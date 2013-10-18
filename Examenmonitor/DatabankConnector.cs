using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;

namespace Examenmonitor
{
    public static class DatabankConnector
    {
        String pad = ConfigDB.getPad();
        var conn = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + "");

        public static void OpenConnection()
        {

            conn.Open();
        }

        public static List<int> GetData()
        {
            
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

        public static void Insert()
        {

        }
        
    }
}