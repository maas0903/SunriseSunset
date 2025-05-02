using MySql.Data.MySqlClient;
using System.Data;

namespace SunriseSunsetApp.Data
{
    public class Database
    {
        private readonly string MySQLConnectionString;
        public Database()
        {
            //MySQLConnectionString = "Server=127.0.0.1; Database=employees; Uid=usrEmployees; Pwd=password;";
            //MySQLConnectionString = "Server=192.168.63.25; Database=versies; Uid=poetry_user; Pwd=vispasty;";
            MySQLConnectionString = "Server=pastei09.local; Database=versies; Uid=poetry_user; Pwd=vispasty;";
        }

        public DataTable MySQLConnection_Datatable()
        {
            DataTable dtDaten = new DataTable();
            // Best practice is to scope the MySqlConnection to a "using" block
            using (MySqlConnection conn = new MySqlConnection(MySQLConnectionString))
            {
                // Connect to the database
                conn.Open();
                // The MySqlCommand class represents a SQL statement to execute against a MySQL database
                // Read rows - Limit for testing purpose to 15 records
                MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM versies LIMIT 15", conn);
                // execute the reader To query the database. Results are usually returned in a MySqlDataReader object, created by ExecuteReader.
                using (MySqlDataReader rdr = selectCommand.ExecuteReader())
                {
                    dtDaten.Load(rdr);
                }
                conn.Close();
            }
            return dtDaten;
        }

    }
}
