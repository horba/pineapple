using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.Model;
using Pineapple.DBServices;
using Pineapple.Services;

namespace Pineapple.Controllers
{
    [Route("/Page")]
    public class PageController : Controller
    {
        [HttpGet]
        public ActionResult Page()
        {
            if (Request.Cookies.ContainsKey("session_id"))
            {
                RegisterData user = UserAuth.GetUserBySession(Request.Cookies["session_id"]);
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    FollowService fs = new FollowService();

                    List<UserModel> lastRegisteredUsers = new UsersController().Get(10);

                    for (int i = 0; i < lastRegisteredUsers.Count; i++)
                    {
                        lastRegisteredUsers[i].CurrentFollow = fs.CheckFollow(Convert.ToInt32(Request.Cookies["id"]), lastRegisteredUsers[i].Id);
                    }

                    return View("~/Views/UserPage/UserPage.cshtml", lastRegisteredUsers);
                }
                else
                {
                    return View("~/Views/MainPage/MainPage.cshtml");
                }
            }
            else
            {
                return View("~/Views/MainPage/MainPage.cshtml");
            }
        }
    }
}
