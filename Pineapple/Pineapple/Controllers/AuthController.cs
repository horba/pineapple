using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.Model;
using Pineapple.Services;

namespace Pineapple.Controllers
{
    [Route("api/[controller]")]
    public class AuthController :Controller
    {
        IUserAuth UserLogin;
        public AuthController(IUserAuth userLogin)
        {
            UserLogin = userLogin;
        }
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            return new ObjectResult(UserLogin.Login(loginModel));
            //return new ObjectResult(new LoginResponseModel("lol", "kek"));
        }
        public IActionResult Register()
        {
            return new EmptyResult();
        }
    }
}
