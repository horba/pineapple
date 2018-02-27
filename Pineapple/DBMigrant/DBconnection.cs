using System;
using System.Data.SqlClient;
using System.Configuration;


namespace DBMigration
{
    public class DBconnection
    {
        public static SqlConnection myConnection { get; set; }
        public static string ServerConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            }
        }
        public static void ConnectionOpen()
        {
            myConnection = new SqlConnection(ServerConnectionString);
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("Connection open.");
        }

        public static void ConnectionClose()
        {
            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
