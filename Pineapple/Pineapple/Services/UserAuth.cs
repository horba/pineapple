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
            SqlParameter ParamPass = myCommand.Parameters.AddWithValue("@ParamPass", UserService.CreateMD5(loginModel.password));
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
            myCommand = new SqlCommand("INSERT INTO Sessions (user_id) VALUES (@ParamUser)",DBconnection.myConnection);
            SqlParameter user = myCommand.Parameters.AddWithValue("@ParamUser", (int)id);
            myCommand.ExecuteNonQuery();

            myCommand.CommandText = "SELECT * FROM Sessions WHERE user_id = @ParamUser";
            myDataReader = myCommand.ExecuteReader();
            int CurrentSesion = 0;
            if (myDataReader.HasRows)
            {
                while (myDataReader.Read())
                {
                    CurrentSesion = (int)myDataReader.GetValue(0);
                }
            }

            DBconnection.ConnectionClose();
            return new List<string>() { "accept", "null",CurrentSesion.ToString() };
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

        public static UserModel GetUserBySession(string sessionId)
        {
            DBconnection.ConnectionOpen();
            UserModel FindedUser = new UserModel();
            SqlCommand sqlCommand = new SqlCommand
            {
                CommandText = string.Format("SELECT * FROM Sessions WHERE session_id = '{0}'", sessionId),
                Connection = DBconnection.myConnection
            };
            SqlDataReader reader = sqlCommand.ExecuteReader();
            int userid = 0;
            if (reader.HasRows)
            {
                reader.Read();
                userid = (int)reader.GetValue(1);
                reader.Close();
            }
            else
            {
                FindedUser.Status = "NOT FIND";
                return FindedUser;
            }

            sqlCommand = new SqlCommand
            {
                CommandText = String.Format("SELECT * FROM Users WHERE Id = '{0}'",userid),
                Connection = DBconnection.myConnection
            };
            reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
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
