using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.DBServices;
using Pineapple.Model;
using Pineapple.Services;

namespace Pineapple.Controllers
{
    [Route("api/[controller]")]
    public class FollowController : Controller
    {
        [HttpPost]
        public string Post(int targetUser)
        {
            int currentUser;

            FollowService fs = new FollowService();

            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"])) {
                    currentUser = UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;

                    if (!fs.CheckFollow(currentUser, targetUser))
                    {
                        fs.AddFollow(currentUser, targetUser);
                        return "true";
                    }
                    else {
                        return "Already follow";
                    }
                }
            }

            return "Not logged in";
        }
    }
}
