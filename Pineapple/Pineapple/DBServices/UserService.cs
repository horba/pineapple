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
        public string CheckUserNick(string nick) {

            string status = "true";

            if (nick.Length > 50)
            {
                status = "Long nickname";
                return status;
            }

            if (!Regex.IsMatch(nick, "^[A-Za-z0-9_]{1,}$")) {
                status = "Use latin, numbers and underscore";
                return status;
            }

            DBconnection.ConnectionOpen();
            try
            {
                SqlCommand myCommand = new SqlCommand("SELECT count(*) FROM dbo.Users WHERE Nick = @ParamNick", DBconnection.myConnection);
                SqlParameter ParamNick = myCommand.Parameters.AddWithValue("@ParamNick", nick);

                int count = Convert.ToInt32(myCommand.ExecuteScalar());

                if (count != 0) {
                    status = "This nickname already exist";
                }
            }
            catch(Exception e) {
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

        public string CheckUserFirstName(string firstName) {
            string status = "true";

            if (firstName == "") {
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

        public string CheckUserPassword(string password) {
            string status = "true";

            if (password.Length < 8) {
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

        public string RegisterUser(RegisterData data) {

            string status = "true";

            DBconnection.ConnectionOpen();
            try
            {
                SqlCommand myCommand = new SqlCommand("INSERT INTO dbo.Users (Nick, FirstName, SecondName, Email, Password) VALUES (@Nick, @FirstName, @SecondName, @Email, @Password)", DBconnection.myConnection);
                SqlParameter Nick = myCommand.Parameters.AddWithValue("@Nick", data.Nick);
                SqlParameter FirstName = myCommand.Parameters.AddWithValue("@FirstName", data.FirstName);
                SqlParameter SecondName = myCommand.Parameters.AddWithValue("@SecondName", data.SecondName);
                SqlParameter Email = myCommand.Parameters.AddWithValue("@Email", data.Email);
                SqlParameter Password = myCommand.Parameters.AddWithValue("@Password", CreateMD5(data.Password));

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