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
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            UserAuth UserLogin = new UserAuth();

            List<string> response = UserLogin.Login(loginModel).ToList();
            if (response[0] == "true")
            {
                Response.Cookies.Append("session_id", response[1]);
                return new ObjectResult(new LoginResponseModel("true", ""));
            }
            else
            {
                return new ObjectResult(new LoginResponseModel("false", response[1]));
            }
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            return new ObjectResult(UserAuth.GetUserBySession(Request.Cookies["session_id"]));
        }
    }
}
