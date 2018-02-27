using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.Model;
using Pineapple.Services;
namespace Pineapple.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        ISearchService SearchEngine;
        public SearchController(ISearchService mySearchService)
        {
            SearchEngine = mySearchService;
        }

        public IActionResult Search(SearchModel searchModel)
        {
            List<UserModel> findedusers = SearchEngine.FindPeoples(searchModel);
            if (findedusers.Count == 0)
            {
                return Json(new { status = true, messages = "No data"});
            }
            return Json(new { status = true, message = "", FindedPeoples = findedusers } );
        }

        [HttpGet("searchFollowers/{searchLine}")]
        public IActionResult SearchInFollowers(string searchLine) {
            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    UserModel user = UserAuth.GetUserBySession(Request.Cookies["session_id"]);
                    int id = 0;
                    if (user != null)
                    {
                        id = user.Id;
                    }
                    else
                    {
                        return Json(new { status = false, message = "Error: User not found" });
                    }

                    List<SimpleUserModel> response = SearchEngine.FindPeoplesInFollowers(searchLine, id);

                    if (response.Count > 0)
                    {
                        return Json(new { status = true, message = "", foundPeople = response });
                    }
                    else
                    {
                        return Json(new { status = true, message = "No data", foundPeople = new List<SimpleUserModel>() });
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Error: Wrong user"});
                }
            }
            else
            {
                return Json(new { status = false, message = "Error: Cookie not found" });
            }
        }

        [HttpGet("searchFollowing/{searchLine}")]
        public IActionResult SearchInFollowing(string searchLine)
        {
            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    UserModel user = UserAuth.GetUserBySession(Request.Cookies["session_id"]);
                    int id = 0;
                    if (user != null)
                    {
                        id = user.Id;
                    }
                    else
                    {
                        return Json(new { status = false, message = "Error: User not found" });
                    }

                    List<SimpleUserModel> response = SearchEngine.FindPeoplesInFollowing(searchLine, id);

                    if (response.Count > 0)
                    {
                        return Json(new { status = true, message = "", foundPeople = response });
                    }
                    else
                    {
                        return Json(new { status = true, message = "No data", foundPeople = response });
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Error: Wrong user" });
                }
            }
            else
            {
                return Json(new { status = false, message = "Error: Cookie not found" });
            }
        }
    }
}
