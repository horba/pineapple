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
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    FollowService fs = new FollowService();
                    int currentUser = UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;
                    List<UserModel> lastRegisteredUsers = new UsersController().Get(10);
                    var users = lastRegisteredUsers.Where(i => i.Id == currentUser).ToList();
                    foreach (var user in users)
                    {
                        lastRegisteredUsers.Remove(user);
                    }
                    for (int i = 0; i < lastRegisteredUsers.Count; i++)
                    {                     
                        lastRegisteredUsers[i].Message = fs.CheckFollow(Convert.ToInt32(UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id), lastRegisteredUsers[i].Id).ToString();
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
