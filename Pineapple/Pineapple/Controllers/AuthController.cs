using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Pineapple.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return new EmptyResult();
        }
        public IActionResult Register()
        {
            return new EmptyResult();
        }
    }
}
