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

            LoginResponseModel response = UserLogin.Login(loginModel);
            if (response.Status == true)
            {
                Response.Cookies.Append("session_id", response.SessionId);
                return new ObjectResult(new { status = true, error = "" });
            }
            else
            {
                return new ObjectResult(new { status = false, error = response.Error });
            }
        }

        [HttpPost("logout")]
        public void Logout()
        {
            UserAuth UserLogin = new UserAuth();
            Response.Cookies.Append("session_id", "");
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            return new ObjectResult(UserAuth.GetUserBySession(Request.Cookies["session_id"]));
        }
    }
}
