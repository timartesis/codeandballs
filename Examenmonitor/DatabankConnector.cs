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
            String pad = ConfigDB.getPad();
            List<int> lijst = new List<int>();
            var conn = new SQLiteConnection(@"data source="+ConfigDB.getPad()+"");
            //var conn = new SQLiteConnection(@"data source=E:\Users\Tim\Documents\Bedrijfontwikkelshit\Examenmonitor\codeandballs\Examenmonitor\Database\db");
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