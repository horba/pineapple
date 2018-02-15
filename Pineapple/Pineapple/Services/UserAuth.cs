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
        public LoginResponseModel Login(LoginModel loginModel)
        {
            DBconnection.ConnectionOpen();

            int userId = 0;
            string username = "";
            string sessionId = "";

            try
            {
                SqlCommand myCommand = new SqlCommand("SELECT * FROM Users WHERE Nick = @Nickname", DBconnection.myConnection);
                myCommand.Parameters.AddWithValue("@Nickname", loginModel.Nickname);
                SqlDataReader myDataReader = myCommand.ExecuteReader();
                
                if (myDataReader.Read())
                {
                    if (UserService.CreateMD5(loginModel.Password) != (string)myDataReader["Password"])
                    {
                        DBconnection.ConnectionClose();
                        return new LoginResponseModel (false, "", "Invalide password");
                    }
                    else {
                        userId = (int)myDataReader["Id"];
                        username = (string)myDataReader["Nick"];
                    }
                }
                else
                {
                    DBconnection.ConnectionClose();
                    return new LoginResponseModel(false, "", "Invalide nickname");
                }
                myDataReader.Close();

                sessionId = UserService.CreateMD5(username + DateTime.Now.ToString());

                myCommand = new SqlCommand("INSERT INTO Sessions (user_id, session_id, date) VALUES (@ParamUser, @ParamSession, @ParamDate)",DBconnection.myConnection);
                myCommand.Parameters.AddWithValue("@ParamUser", userId);
                myCommand.Parameters.AddWithValue("@ParamSession", sessionId);
                myCommand.Parameters.AddWithValue("@ParamDate", DateTime.UtcNow);
                myCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                DBconnection.ConnectionClose();
                return new LoginResponseModel(false, "", e.Message);
            }
            DBconnection.ConnectionClose();

            return new LoginResponseModel(true, sessionId, "");
        }

        public static bool CheckUserSession(string sessionId)
        {
            bool status = false;

            DBconnection.ConnectionOpen();
            try
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    CommandText = String.Format("SELECT COUNT(*) FROM Sessions WHERE session_id = '{0}'", sessionId),
                    Connection = DBconnection.myConnection
                };
                int count = Convert.ToInt32(sqlCommand.ExecuteScalar());

                status = count == 1 ? true : false;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            DBconnection.ConnectionClose();

            return status;
        }

        public static UserModel GetUserBySession(string sessionId)
        {
            UserModel FindedUser = new UserModel();

            DBconnection.ConnectionOpen();
            try
            {
                SqlCommand sqlCommand = new SqlCommand
                {
                    CommandText = string.Format("SELECT * FROM Sessions WHERE session_id = '{0}'", sessionId),
                    Connection = DBconnection.myConnection
                };
                SqlDataReader reader = sqlCommand.ExecuteReader();

                int userid = 0;
                if (reader.Read())
                {
                    userid = (int)reader["user_id"];
                    reader.Close();
                }
                else
                {
                    DBconnection.ConnectionClose();
                    FindedUser.Status = "Not found";
                    return FindedUser;
                }

                sqlCommand = new SqlCommand
                {
                    CommandText = String.Format("SELECT * FROM Users WHERE Id = '{0}'", userid),
                    Connection = DBconnection.myConnection
                };
                reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    FindedUser.Id = (int)reader.GetValue(0);
                    FindedUser.Nickname = (string)reader.GetValue(1);
                    FindedUser.FirstName = (string)reader.GetValue(2);
                    FindedUser.LastName = (string)reader.GetValue(3);
                    FindedUser.Email = (string)reader.GetValue(4);
                    FindedUser.Password = (string)reader.GetValue(5);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                FindedUser.Status = "Error";
            }
            DBconnection.ConnectionClose();

            return FindedUser;
        }
    }
}
