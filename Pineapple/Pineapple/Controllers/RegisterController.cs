using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.DBServices;
using Pineapple.Model;

namespace Pineapple.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        // POST
        [HttpPost]
        public RegisterData Post(RegisterData data)
        {
            data.Email = data.Email.ToLower();
            data.FirstName = data.FirstName == null ? "" : data.FirstName;
            data.SecondName = data.SecondName == null ? "" : data.SecondName;

            RegisterData response = new RegisterData();
            UserService user = new UserService();

            response.Nick = user.CheckUserNick(data.Nick);
            response.Email = user.CheckUserEmail(data.Email);
            response.FirstName = user.CheckUserFirstName(data.FirstName);
            response.SecondName = user.CheckUserSecondName(data.SecondName);
            response.Password = user.CheckUserPassword(data.Password);
            response.RPassword = user.CheckUserRPassword(data.Password, data.RPassword);

            if (response.Nick == "true" && response.Email == "true" && response.FirstName == "true" && response.SecondName == "true" &&
                response.Password == "true" && response.RPassword == "true")
            {
                response.Status = user.RegisterUser(data);
            }

            return response;
        }
    }
}
