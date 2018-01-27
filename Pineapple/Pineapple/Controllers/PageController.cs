using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

            if (auth) {
                return View("~/Views/UserPage/UserPage.cshtml");
            }
            else {
                return View("~/Views/MainPage/MainPage.cshtml");
            }
        }
    }
}
