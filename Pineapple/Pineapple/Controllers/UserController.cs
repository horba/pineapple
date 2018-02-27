using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.DBServices;
using Pineapple.Model;

using Microsoft.AspNetCore.Http;
using System.IO;
using Pineapple.Services;

namespace Pineapple.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        // POST
        [HttpPost("register")]
        public UserModel Post(UserModel data)
        {
            data.Email = data.Email.ToLower();
            data.FirstName = data.FirstName == null ? "" : data.FirstName;
            data.LastName = data.LastName == null ? "" : data.LastName;

            UserModel response = new UserModel();
            UserService user = new UserService();

            response.Nickname = user.CheckUserNick(data.Nickname);
            response.Email = user.CheckUserEmail(data.Email);
            response.FirstName = user.CheckUserFirstName(data.FirstName);
            response.LastName = user.CheckUserSecondName(data.LastName);
            response.Password = user.CheckUserPassword(data.Password);
            response.RPassword = user.CheckUserRPassword(data.Password, data.RPassword);

            if (response.Nickname == "true" && response.Email == "true" && response.FirstName == "true" && response.LastName == "true" &&
                response.Password == "true" && response.RPassword == "true")
            {
                response.Status = user.RegisterUser(data);
            }

            return response;
        }

        [HttpPost("change")]
        public UserShortModel PostChange(UserShortModel data)
        {

            UserModel current = UserAuth.GetUserBySession(Request.Cookies["session_id"]);

            UserShortModel response = new UserShortModel();
            UserService user = new UserService();

            if (data.Nickname != current.Nickname) { response.Nickname = user.CheckUserNick(data.Nickname); }
            else { response.Nickname = "true"; }

            if (data.Email != current.Email) { response.Email = user.CheckUserEmail(data.Email); }
            else { response.Email = "true"; }

            data.Nickname = data.Nickname == null ? current.Nickname : data.Nickname;
            data.Email = data.Email == null ? current.Email : data.Email;
            data.FirstName = data.FirstName == null ? current.FirstName : data.FirstName;
            data.LastName = data.LastName == null ? current.LastName : data.LastName;
            data.Email = data.Email.ToLower();

            response.FirstName = user.CheckUserFirstName(data.FirstName);
            response.LastName = user.CheckUserSecondName(data.LastName);

            if (response.Nickname == "true" && response.Email == "true" && response.FirstName == "true" && response.LastName == "true")
            {
                response.Status = user.ChangeUser(data, current);
            }

            return response;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            UserModel user = UserAuth.GetUserBySession(Request.Cookies["session_id"]);
            if (user != null)
            {
                return Json(new { status = true, user = new SimpleUserModel(user.Id, user.Nickname, user.FirstName, user.LastName)});
            }
            else {
                return Json(new { status = false });
            }
        }

        [HttpPost("photo")]
        public void Post(IFormFile file)
        {
            try
            {
                UserModel user = UserAuth.GetUserBySession(Request.Cookies["session_id"]);
                file.CopyTo(new FileStream("wwwroot\\img\\" + user.Id + ".jpg", FileMode.Create));
            }
            catch { }
        }

        [HttpGet("photo")]
        public string GetPhoto()
        {
            UserModel user = UserAuth.GetUserBySession(Request.Cookies["session_id"]);
            if (System.IO.File.Exists(Directory.GetCurrentDirectory()+"\\wwwroot\\img\\" + user.Id + ".jpg"))
            {
                return "\\img\\" + user.Id + ".jpg";
            }
            return "\\img\\avatar.png";
        }
    }
    
}
