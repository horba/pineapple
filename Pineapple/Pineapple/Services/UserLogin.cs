using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pineapple.Model;

namespace Pineapple.Services
{
    public class UserLogin : IUserLogin
    {
        public LoginResponseModel Login(LoginModel loginModel)
        {
            if (loginModel.UserName == "test")
            {
                if (loginModel.Password == "Passw0rd")
                {
                    return new LoginResponseModel("succes", "null");
                }
                else
                {
                    return new LoginResponseModel("failure", "Wrong password");
                }
            }
            else
            {
                return new LoginResponseModel("failure", "Wrong user");
            }
        }
    }
}
