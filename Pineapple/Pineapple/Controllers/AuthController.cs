using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.Model;
using Pineapple.Services;

namespace Pineapple.Controllers
{
    public class AuthController :Controller
    {
        IUserLogin UserLogin;
        public AuthController(IUserLogin userLogin)
        {
            UserLogin = userLogin;
        }
        public LoginResponseModel Login(LoginModel loginModel)
        {
            return UserLogin.Login(loginModel);
        }
        public IActionResult Register()
        {
            return new EmptyResult();
        }
    }
}
