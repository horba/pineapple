using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;


namespace DBMigration
{
    public class DBconnection
    {
        public static SqlConnection myConnection { get; set; }
        public static string ServerConnectionString
        {
            get
            {
                return "user id=;" +
                       "password=;" +
                       "Data Source = WIN-BO5HSJLIOG9;" +
                       "Trusted_Connection=yes;" +
                       "database=Pineapple; " +
                       "connection timeout=3";
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
