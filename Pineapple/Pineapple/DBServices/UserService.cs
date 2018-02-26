using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Pineapple.Model;
using System.Security.Cryptography;

namespace Pineapple.DBServices
{
    public class UserService : IUserService
    {
        public List<UserModel> GetAllUsers()
        {
            List<UserModel> result = new List<UserModel>();
            DBconnection.ConnectionOpen();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from dbo.Users", DBconnection.myConnection);
                myReader = myCommand.ExecuteReader();
                string[] fields = { "Id", "Nick", "FirstName", "SecondName", "Email", "Password" };
                while (myReader.Read())
                {
                    UserModel cortage = new UserModel(Convert.ToInt32(myReader[fields[0]]), myReader[fields[1]].ToString(), myReader[fields[2]].ToString(), myReader[fields[3]].ToString(), myReader[fields[4]].ToString(), myReader[fields[5]].ToString());
                    result.Add(cortage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            DBconnection.ConnectionClose();
            return result;
        }

        public UserModel GetUserById(int id)
        {
            UserModel result = null;
            DBconnection.ConnectionOpen();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(String.Format("select * from dbo.Users where id = {0}", id), DBconnection.myConnection);
                myReader = myCommand.ExecuteReader();
                string[] fields = { "Id", "Nick", "FirstName", "SecondName", "Email", "Password" };
                myReader.Read();
                result = new UserModel(Convert.ToInt32(myReader[fields[0]]), myReader[fields[1]].ToString(), myReader[fields[2]].ToString(), myReader[fields[3]].ToString(), myReader[fields[4]].ToString(), myReader[fields[5]].ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            DBconnection.ConnectionClose();
            return result;
        }

        public List<UserModel> GetLastRegisteredUsers(int count)
        {

            List<UserModel> users = new List<UserModel>();

            DBconnection.ConnectionOpen();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("SELECT TOP "+count+" Nick, Id FROM dbo.Users ORDER BY RegistrationDate DESC", DBconnection.myConnection);
                myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    users.Add(new UserModel() { Id = Convert.ToInt32(myReader["Id"]), Nickname = myReader["Nick"].ToString(), Message = "false" });
                }
            }
            catch (Exception e)
            {
                users.Add(new UserModel() { Id = 0, Nickname = e.Message, Message = "false"});
            }

            DBconnection.ConnectionClose();

            return users;
        }

        public string CheckUserNick(string nickname)
        {
            string status = "true";

            if (nickname.Length > 50)
            {
                status = "Long nickname";
                return status;
            }

            if (!Regex.IsMatch(nickname, "^[A-Za-z0-9_]{1,}$"))
            {
                status = "Use latin, numbers and underscore";
                return status;
            }

            DBconnection.ConnectionOpen();
            try
            {
                SqlCommand myCommand = new SqlCommand("SELECT count(*) FROM dbo.Users WHERE Nick = @Nickname", DBconnection.myConnection);
                SqlParameter ParamNick = myCommand.Parameters.AddWithValue("@Nickname", nickname);

                int count = Convert.ToInt32(myCommand.ExecuteScalar());

                if (count != 0)
                {
                    status = "This nickname already exist";
                }
            }
            catch (Exception e)
            {
                status = e.ToString();
            }
            DBconnection.ConnectionClose();

            return status;
        }

        public string CheckUserEmail(string email)
        {
            string status = "true";

            if (email.Length > 50)
            {
                status = "Long email";
                return status;
            }

            if (!Regex.IsMatch(email, @"^([a-z0-9_-]+.){1,}[a-z0-9_-]+@([a-z0-9][a-z0-9-]{1,}[a-z0-9].)+[a-z]{2,4}$"))
            {
                status = "Email is not valid";
                return status;
            }

            DBconnection.ConnectionOpen();
            try
            {
                SqlCommand myCommand = new SqlCommand("SELECT count(*) FROM dbo.Users WHERE Email = @ParamEmail", DBconnection.myConnection);
                SqlParameter ParamEmail = myCommand.Parameters.AddWithValue("@ParamEmail", email);

                int count = Convert.ToInt32(myCommand.ExecuteScalar());

                if (count != 0)
                {
                    status = "This email is already in use";
                }
            }
            catch (Exception e)
            {
                status = e.ToString();
            }
            DBconnection.ConnectionClose();

            return status;
        }

        public string CheckUserFirstName(string firstName)
        {
            string status = "true";

            if (firstName == "")
            {
                return status;
            }

            if (firstName.Length > 50)
            {
                status = "Long first name";
                return status;
            }

            if (!Regex.IsMatch(firstName, @"^[A-Za-z]{1,}$"))
            {
                status = "Use latin characters";
            }

            return status;
        }

        public string CheckUserSecondName(string secondName)
        {
            string status = "true";

            if (secondName == "")
            {
                return status;
            }

            if (secondName.Length > 50)
            {
                status = "Long second name";
                return status;
            }

            if (!Regex.IsMatch(secondName, @"^[A-Za-z]{1,}$"))
            {
                status = "Use latin characters";
            }

            return status;
        }

        public string CheckUserPassword(string password)
        {
            string status = "true";

            if (password.Length < 8)
            {
                status = "Short password";
            }

            return status;
        }

        public string CheckUserRPassword(string password, string rPassword)
        {
            string status = "true";

            if (password != rPassword)
            {
                status = "Passwords do not match";
            }

            return status;
        }
        
        public string RegisterUser(UserModel data)
        {
            string status = "true";

            DBconnection.ConnectionOpen();
            try
            {
                SqlCommand myCommand = new SqlCommand("INSERT INTO dbo.Users (Nick, FirstName, SecondName, Email, Password, RegistrationDate) VALUES (@Nickname, @FirstName, @LastName, @Email, @Password, @Date)", DBconnection.myConnection);
                myCommand.Parameters.AddWithValue("@Nickname", data.Nickname);
                myCommand.Parameters.AddWithValue("@FirstName", data.FirstName);
                myCommand.Parameters.AddWithValue("@LastName", data.LastName);
                myCommand.Parameters.AddWithValue("@Email", data.Email);
                myCommand.Parameters.AddWithValue("@Password", CreateMD5(data.Password));
                myCommand.Parameters.AddWithValue("@Date", DateTime.UtcNow);

                myCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                status = e.ToString();
            }
            DBconnection.ConnectionClose();

            return status;
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                string str = "";
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    str += hashBytes[i].ToString("X2");
                }
                return str;
            }
        }
    }
}