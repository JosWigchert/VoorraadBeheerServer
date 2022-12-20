using MySqlConnector;

namespace VoorraadBeheer.Models
{
    public class mySQLSqlHelper
    {
        //this field gets initialized at Startup.cs
        public static string ConnectionString;
        public static MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                return connection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
