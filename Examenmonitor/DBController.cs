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
        public DBController(String SQL) //elke controller vereist initiele SQL querries
        {
            this.SQL = SQL;
        }

        public void ExecuteNonQuery() //voert de opgegeven query code uit zonder resultaat terug te geven
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

        public string ExecuteReaderQueryReturnSingleString(string tabelnaam) //haalt 1 stringresult uit 1 row op basis van een tabelnaam op
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

        public int ExecuteReaderQueryReturnSingleInt(string tabelnaam) //haalt 1 int op uit 1 row op basis van een tabelnaam
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

        public bool ExecuteReaderQueryReturnSingleResult() //haalt op of er rijen zijn in opgegeven query
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

        public List<KeyValuePair<string, string>> ExecuteReaderQueryReturnMultipleResultsOneRow(params string[] tabelnamen) //deze parameer moet veranderen naar een variabele die oneindig strings accepteert            
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

        public List<List<KeyValuePair<string, string>>> ExecuteReaderQueryReturnMultipleResultsMultipleRow(params string[] tabelnamen) //deze parameer moet veranderen naar een variabele die oneindig strings accepteert            
        {
            /*
             * Werkt met volgende structuur:
             * Alle rijen zitten in een lijst, in de volgende lijst vindt je een lijst met alle key value pairs voor 1 row
             * elke key value paar staat voor 1 opgehaalde waarde
             * Rij->Data->KeyValue
             */
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