using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.Model;

namespace Pineapple.Controllers
{
    public class AuthController :Controller
    {
        public LoginResponseModel Login(User currentUser)
        {
            if (currentUser.UserName == "test")
            {
                if (currentUser.Password == "Passw0rd")
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
        public IActionResult Register()
        {
            return new EmptyResult();
        }
    }
}
