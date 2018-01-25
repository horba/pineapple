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

            SqlCommand myCommand = new SqlCommand("SELECT COUNT(*) FROM dbo.Users WHERE Nick = @ParamNick AND Password = @ParamPass",DBconnection.myConnection);
            SqlParameter parameter = myCommand.Parameters.AddWithValue("@ParamNick",loginModel.name);
            SqlParameter ParamPass = myCommand.Parameters.AddWithValue("@ParamPass", loginModel.password);
            //SqlDataReader DataReader = myCommand.ExecuteReader();
            int count = Convert.ToInt32(myCommand.ExecuteScalar());
            if (count == 1)
            {
                return new LoginResponseModel("accept","null");
            }
            else 
            {
                return new LoginResponseModel("ban","Invalid Login or Password");
            }
        }
    }
}
