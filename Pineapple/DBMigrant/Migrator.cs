using System;
using System.Data.SqlClient;
using System.Data;

namespace DBMigration
{
    static class Migrator
    {
        //применить изменение
        static public bool Apply(Int64 ClassNumber, string query) {

            bool result = false;

            DBconnection.ConnectionOpen();

            try
            {
                SqlDataReader reader = null;
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.MigrationHistory WHERE ClassNumber = " + ClassNumber, DBconnection.myConnection);
                reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    reader.Close();
                    if (query != String.Empty)
                    {
                        command = new SqlCommand(query, DBconnection.myConnection);
                        command.ExecuteNonQuery();

                        command = new SqlCommand("INSERT INTO dbo.MigrationHistory (ClassNumber, DateApplied) VALUES (@classNumber, @date)", DBconnection.myConnection);
                        SqlParameter date = command.Parameters.Add("@date", SqlDbType.DateTime);
                        date.Value = DateTime.Now;
                        SqlParameter classNumber = command.Parameters.Add("@classNumber", SqlDbType.BigInt);
                        classNumber.Value = ClassNumber;
                        command.ExecuteNonQuery();

                        result = true;

                        Console.WriteLine(ClassNumber + ": Successfully applied.");
                    }
                    else {
                        Console.WriteLine(ClassNumber + " error: Empty query.");
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(ClassNumber + " error: " + e.Message);
            }
            DBconnection.ConnectionClose();

            return result;
        }
        //отменить изменение
        static public bool Revert(Int64 ClassNumber, string query) {

            bool result = false;

            DBconnection.ConnectionOpen();

            try
            {
                SqlDataReader reader = null;
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.MigrationHistory WHERE ClassNumber = " + ClassNumber, DBconnection.myConnection);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    if (query != String.Empty)
                    {
                        command = new SqlCommand(query, DBconnection.myConnection);
                        command.ExecuteNonQuery();

                        command = new SqlCommand("DELETE FROM dbo.MigrationHistory WHERE ClassNumber =" + ClassNumber, DBconnection.myConnection);
                        command.ExecuteNonQuery();

                        result = true;

                        Console.WriteLine(ClassNumber + ": Successfully revert.");
                    }
                    else
                    {
                        Console.WriteLine(ClassNumber + " error: Empty query.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(ClassNumber + " error: " + e.Message);
            }

            DBconnection.ConnectionClose();

            return result;
        }
    }
}
