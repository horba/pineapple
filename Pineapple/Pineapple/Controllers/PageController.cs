using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.Model;
using Pineapple.DBServices;

namespace Pineapple.Controllers
{
    [Route("/Page")]
    public class PageController : Controller
    {
        [HttpGet]
        public ActionResult Page()
        {
            //возвращает главную сраницу или страницу твитов, в зависимости от того, авторизирован ли пользователь
            bool auth = true;

            if (auth)
            {
                FollowService fs = new FollowService();

                Response.Cookies.Append("id", "2");

                List<UserModel> lastRegisteredUsers = new UsersController().Get(10);

                for (int i = 0; i < lastRegisteredUsers.Count; i++) {
                    lastRegisteredUsers[i].CurrentFollow = fs.CheckFollow(Convert.ToInt32(Request.Cookies["id"]), lastRegisteredUsers[i].Id);
                }

                return View("~/Views/UserPage/UserPage.cshtml", lastRegisteredUsers);
            }
            else
            {
                return View("~/Views/MainPage/MainPage.cshtml");
            }
        }
    }
}
