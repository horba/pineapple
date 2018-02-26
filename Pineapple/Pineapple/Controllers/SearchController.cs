using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.Model;
using Pineapple.Services;
using Pineapple.DBServices;
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

        public JsonResult Searach(SearchModel searchModel)
        {
            return Json(SearchEngine.FindPeoples(searchModel));
        }

        [HttpGet("searchFollowers/{searchLine}")]
        public IActionResult SearchInFollowers(string searchLine) {
            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    int id = UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;
                    FollowService fs = new FollowService();

                    List<SimpleUserModel> response = SearchEngine.FindPeoplesInFollowers(searchLine, id);
                    List<FollowViewModel> users = new List<FollowViewModel>();

                    foreach (var i in response) {
                        users.Add(new FollowViewModel(i.Id, i.Nickname, i.FirstName, i.LastName, fs.CheckFollow(id, i.Id)));
                    }

                    if (response.Count > 0)
                    {
                        return Json(new { status = "true", foundPeople = response });
                    }
                    else
                    {
                        return Json(new { status = "empty" });
                    }
                }
                else
                {
                    return Json(new { status = "error" });
                }
            }
            else
            {
                return Json(new { status = "error" });
            }
        }

        [HttpGet("searchFollowing/{searchLine}")]
        public IActionResult SearchInFollowing(string searchLine)
        {
            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    int id = UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;

                    List<SimpleUserModel> response = SearchEngine.FindPeoplesInFollowing(searchLine, id);

                    if (response.Count > 0)
                    {
                        return Json(new { status = "true", foundPeople = response });
                    }
                    else
                    {
                        return Json(new { status = "empty" });
                    }
                }
                else
                {
                    return Json(new { status = "error" });
                }
            }
            else
            {
                return Json(new { status = "error" });
            }
        }
    }
}
