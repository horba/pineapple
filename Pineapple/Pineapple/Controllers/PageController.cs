using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.Services;
using Pineapple.Model;

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
                    return View("~/Views/UserPage/UserPage.cshtml");
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
