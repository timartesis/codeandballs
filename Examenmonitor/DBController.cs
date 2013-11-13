using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;

namespace Examenmonitor
{
    public class DBController
    {
        public String SQL { get; set; }
        public DBController(String SQL)
        {
            this.SQL = SQL;
        }

        public void ExecuteNonQuery()
        {
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();                
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string ExecuteReaderQueryReturnSingleString(string tabelnaam)
        {
            string result = "";
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();                
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {
                    using(var reader = cmd.ExecuteReader()) {
                        while (reader.Read())
                        {
                            result = reader.GetString(reader.GetOrdinal(tabelnaam));
                        }
                    }
                }
            }
            return result;
        }

        public int ExecuteReaderQueryReturnSingleInt(string tabelnaam)
        {
            int result = -1;
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader.GetInt32(reader.GetOrdinal(tabelnaam));
                        }
                    }
                }
            }
            return result;
        }

        public bool ExecuteReaderQueryReturnSingleResult()
        {
            bool result;
            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        result = reader.HasRows;                        
                    }
                }
            }
            return result;
        }

        public List<KeyValuePair<string, string>> ExecuteReaderQueryReturnMultipleResultsOneRow(string[] tabelnamen) //deze parameer moet veranderen naar een variabele die oneindig strings accepteert            
        {
            List<KeyValuePair<string, string>> lijst = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> data;
            string value = "";

            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {                            
                            foreach (string tabelnaam in tabelnamen)
                            {
                                value = reader.GetString(reader.GetOrdinal(tabelnaam));
                                data = new KeyValuePair<string, string>(tabelnaam, value);
                                lijst.Add(data);
                            }
                        }
                    }
                }
            }
            return lijst;
        }

        public List<List<KeyValuePair<string, string>>> ExecuteReaderQueryReturnMultipleResultsMultipleRow(string[] tabelnamen) //deze parameer moet veranderen naar een variabele die oneindig strings accepteert            
        {
            List<List<KeyValuePair<string, string>>> rijen = new List<List<KeyValuePair<string, string>>>();
            List<KeyValuePair<string, string>> lijst;
            KeyValuePair<string, string> data;
            string value = "";

            using (SQLiteConnection c = new SQLiteConnection(@"data source=" + ConfigDB.getPad() + ""))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(SQL, c))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lijst = new List<KeyValuePair<string, string>>();
                            foreach (string tabelnaam in tabelnamen)
                            {                                
                                value = reader.GetString(reader.GetOrdinal(tabelnaam));
                                data = new KeyValuePair<string, string>(tabelnaam, value);
                                lijst.Add(data);
                            }
                            rijen.Add(lijst);
                        }
                    }
                }
            }
            return rijen;
        }
    }
}