﻿using System;
using System.Data.SqlClient;
using System.Configuration;


namespace Pineapple.DBServices
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
                       "Data Source= DESKTOP-U397L7T\\SQLEXPRESS;" +
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
            Console.WriteLine("Connection open, please press any key to write");
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
