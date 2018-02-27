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
    public class UserController : Controller
    {
        // POST
        [HttpPost("register")]
        public Dictionary<string, string> Post(UserModel data)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            data.Email = data.Email.ToLower();
            data.FirstName = data.FirstName == null ? "" : data.FirstName;
            data.LastName = data.LastName == null ? "" : data.LastName;

            UserModel response = new UserModel();
            UserService user = new UserService();

            if (user.CheckUserNick(data.Nickname) != "true")
            {
                errors.Add("Nickname", user.CheckUserNick(data.Nickname));
            }
            if (user.CheckUserEmail(data.Email) != "true")
            {
                errors.Add("Email", user.CheckUserEmail(data.Email));
            }
            if (user.CheckUserFirstName(data.FirstName) != "true")
            {
                errors.Add("FirstName", user.CheckUserFirstName(data.FirstName));
            }
            if (user.CheckUserSecondName(data.LastName) != "true")
            {
                errors.Add("LastName", user.CheckUserSecondName(data.LastName));
            }
            if (user.CheckUserPassword(data.Password) != "true")
            {
                errors.Add("Password", user.CheckUserPassword(data.Password));
            }
            if (user.CheckUserRPassword(data.Password, data.RPassword) != "true")
            {
                errors.Add("RPassword", user.CheckUserRPassword(data.Password, data.RPassword));
            }


            if (errors.Count == 0)
            {
                if (user.RegisterUser(data) != "true")
                {
                    errors.Add("DB", user.RegisterUser(data));
                }    
            }
            return errors;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            UserService modelReader = new UserService();
            List<UserModel> allUsers = modelReader.GetAllUsers();
            List<string> stringUsers = new List<string>();
            foreach (UserModel user in allUsers)
            {
                stringUsers.Add(user.ToStringWithoutPassword());
            }
            return stringUsers;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            UserService modelReader = new UserService();
            UserModel user = modelReader.GetUserById(id);
            return user.ToStringWithoutPassword();
        }
    }
}
