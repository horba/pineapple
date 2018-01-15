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
            //{
            //    return "user id=;" +
            //           "password=;" +
            //           "data source=;" +
            //           "trusted_connection=yes;" +
            //           "database=pineapple; " +
            //           "connection timeout=3";

            {
                return "user id=test1;" +
                "password=ythfpuflfnm;" +
                "data source=win-1o0ieh1eb53\\sqlexpress;" +
                "trusted_connection=yes;" +
                "database=pineapple; " +
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
