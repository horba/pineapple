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
    public class UsersController : Controller
    {
        [HttpGet("{count}")]
        public List<UserModel> Get(int count) {

            UserService us = new UserService();

            return us.GetLastRegisteredUsers(count);
        }
    }
}
