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

        //elke controller vereist initiele SQL querries
        public DBController(String SQL) 
        {
            this.SQL = SQL;
        }

        //voert de opgegeven query code uit zonder resultaat terug te geven
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

        //haalt 1 stringresult uit 1 row op basis van een tabelnaam op, ints worden gevoncerteerd naar strings
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
                            try
                            {
                                result = reader.GetString(reader.GetOrdinal(tabelnaam));
                            }
                            catch (Exception e)
                            {
                                result = reader.GetInt32(reader.GetOrdinal(tabelnaam)).ToString();
                            }
                        }
                    }
                }
            }
            return result;
        }

        //returned true als er een rij gevonden is in de opgegeven query
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

        //returned een lijst waar de resultaten van de opgegeven tabelnamen inzitten in de vorm van (tabelnaam, waarde)
        public List<KeyValuePair<string, string>> ExecuteReaderQueryReturnMultipleResultsOneRow(params string[] tabelnamen)             
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
                                try
                                {
                                    value = reader.GetString(reader.GetOrdinal(tabelnaam));
                                }
                                catch (Exception e)
                                {
                                    value = reader.GetInt32(reader.GetOrdinal(tabelnaam)).ToString();
                                }
                                data = new KeyValuePair<string, string>(tabelnaam, value);
                                lijst.Add(data);
                            }
                        }
                    }
                }
            }
            return lijst;
        }

        //returned een lijst waar de resultaten van de opgegeven tabelnamen inzitten in de vorm van lijsten met daarin de (tabelnaam, waarde)
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
                                try
                                {
                                    value = reader.GetString(reader.GetOrdinal(tabelnaam));
                                }
                                catch (Exception e)
                                {
                                    value = reader.GetInt32(reader.GetOrdinal(tabelnaam)).ToString();
                                }
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