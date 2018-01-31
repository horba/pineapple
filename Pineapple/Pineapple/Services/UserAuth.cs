using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pineapple.Model;
using Pineapple.DBServices;
using System.Data.SqlClient;

namespace Pineapple.Services
{
    public class UserAuth : IUserAuth
    {
        public IEnumerable<string> Login(LoginModel loginModel)
        {
            DBconnection.ConnectionOpen();
            SqlCommand myCommand = new SqlCommand("SELECT * FROM Users WHERE Nick = @ParamNick AND Password = @ParamPass",DBconnection.myConnection);
            SqlParameter parameter = myCommand.Parameters.AddWithValue("@ParamNick",loginModel.name);
            SqlParameter ParamPass = myCommand.Parameters.AddWithValue("@ParamPass", loginModel.password);
            SqlDataReader myDataReader = myCommand.ExecuteReader();
            object id = null, nick = null;
            if (myDataReader.HasRows)
            {
                while (myDataReader.Read())
                {
                    id = myDataReader.GetValue(0);
                    nick = myDataReader.GetValue(1);
                }
            }
            else
            {
                DBconnection.ConnectionClose();
                return new List<string>() { "ban", "Invalid Login or Password" };
            }
            myDataReader.Close();
            myCommand = new SqlCommand("INSERT INTO Sessions (session_id,user_id) VALUES (@ParamSession,@ParamUser)",DBconnection.myConnection);
            SqlParameter session = myCommand.Parameters.AddWithValue("@ParamSession", nick.GetHashCode());
            SqlParameter user = myCommand.Parameters.AddWithValue("@ParamUser", (int)id);
            myCommand.ExecuteNonQuery();
            DBconnection.ConnectionClose();
            return new List<string>() { "accept", "null",nick.GetHashCode().ToString() };
        }

        public static bool CheckUserSession(string SessionId)
        {
            DBconnection.ConnectionOpen();
            SqlCommand sqlCommand = new SqlCommand
            {
                CommandText = String.Format("SELECT COUNT(*) FROM Sessions WHERE session_id = '{0}'", SessionId),
                Connection = DBconnection.myConnection
            };
            int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
            if(count == 1)
            {
                DBconnection.ConnectionClose();
                return true;
            }
            else
            {
                DBconnection.ConnectionClose();
                return false;
            }
        }

        public static RegisterData GetUserBySession(string sessionId)
        {
            DBconnection.ConnectionOpen();
            SqlCommand sqlCommand = new SqlCommand
            {
                CommandText = string.Format("SELECT * FROM Sessions WHERE session_id = '{0}'", sessionId),
                Connection = DBconnection.myConnection
            };
            SqlDataReader reader = sqlCommand.ExecuteReader();
            reader.Read();
            int userid = (int)reader.GetValue(1);
            reader.Close();

            sqlCommand = new SqlCommand
            {
                CommandText = String.Format("SELECT * FROM Users WHERE Id = '{0}'",userid),
                Connection = DBconnection.myConnection
            };
            reader = sqlCommand.ExecuteReader();
            RegisterData FindedUser = new RegisterData();

            if (reader.HasRows)
            {
                reader.Read();
                FindedUser.Id = Convert.ToInt32(reader.GetValue(0));
                FindedUser.Nick = (string)reader.GetValue(1);
                FindedUser.FirstName = (string)reader.GetValue(2);
                FindedUser.SecondName = (string)reader.GetValue(3);
                FindedUser.Email = (string)reader.GetValue(4);
                FindedUser.Password = (string)reader.GetValue(5);
            }
            
            return FindedUser;
        }
    }
}
