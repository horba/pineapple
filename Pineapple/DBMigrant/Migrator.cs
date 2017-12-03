using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DBMigration
{
    class Migrator
    {
        private string fileNumber;
        private string query;
        private string error;

        public string FileNumber { get { return fileNumber; } }
        public string Error { get { return error; } }

        public Migrator(string FileNumber, string Query) {
            fileNumber = FileNumber;
            query = Query;
        }

        //применить изменение
        public bool Apply() {

            bool result = false;

            DBconnection.ConnectionOpen();

            try
            {
                SqlDataReader reader = null;
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.MigrationHistory WHERE FileNumber = "+ FileNumber, DBconnection.myConnection);
                reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    reader.Close();
                    if (query != String.Empty)
                    {
                        command = new SqlCommand(query, DBconnection.myConnection);
                        command.ExecuteNonQuery();

                        command = new SqlCommand("INSERT INTO dbo.MigrationHistory (FileNumber, Comment, DateApplied) VALUES (@filenumber, @comment, @date)", DBconnection.myConnection);
                        SqlParameter filenumber = command.Parameters.Add("@filenumber", SqlDbType.VarChar, 4);
                        filenumber.Value = fileNumber;
                        SqlParameter comment = command.Parameters.Add("@comment", SqlDbType.VarChar, 255);
                        comment.Value = "Auto application";
                        SqlParameter date = command.Parameters.Add("@date", SqlDbType.DateTime);
                        date.Value = DateTime.Now;
                        command.ExecuteNonQuery();

                        result = true;
                    }
                    else {
                        error = "Пустой запрос.";
                    }
                }
                else {
                    error = "Этот скрипт уже был применен.";
                }
            }
            catch (Exception e) {
                error = e.Message;
            }
            DBconnection.ConnectionClose();

            return result;
        }
        //отменить изменение
        public bool Revert() {

            return true;
        }
    }
}
