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
            List<string> response = UserLogin.Login(loginModel).ToList();
            if (response.Count == 3)
            {
                Response.Cookies.Append("session_id", response[2]);
                return new ObjectResult(new LoginResponseModel("accept", "null"));
            }
            else
            {
                return new ObjectResult(new LoginResponseModel("ban", "Invalid Login or Password"));
            }
        }
        [HttpGet]
        public IActionResult GetUser()
        {
            return new ObjectResult(UserAuth.GetUserBySession(Request.Cookies["session_id"]));
        }
        public IActionResult Register()
        {
            return new EmptyResult();
        }
    }
}
