using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Pineapple.Controllers
{
    [Route("/")]
    public class IndexController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
