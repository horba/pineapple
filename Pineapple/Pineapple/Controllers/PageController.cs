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

        [HttpGet("{page}")]
        public ActionResult Page(string page)
        {
            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    switch (page) {
                        case "home":
                            return View("~/Views/UserPage/HomePage.cshtml");
                        case "profile":
                            return View("~/Views/UserPage/ProfilePage.cshtml");
                        case "search":
                            return View("~/Views/UserPage/SearchPage.cshtml");
                        case "settings":
                            return View("~/Views/UserPage/SettingsPage.cshtml");
                    }

                    return View("~/Views/UserPage/ErrorPage.cshtml");
                }
                else
                {
                    return View("~/Views/UserPage/ErrorPage.cshtml");
                }
            }
            else
            {
                return View("~/Views/UserPage/ErrorPage.cshtml");
            }
        }
    }
}
