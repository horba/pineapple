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
        public string Follow(int targetUser)
        {
            int currentUser;

            FollowService fs = new FollowService();

            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"])) {
                    currentUser = UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;

                    if (currentUser == targetUser) {
                        return "You can't follow yourself";
                    }

                    if (!fs.CheckFollow(currentUser, targetUser))
                    {
                        if (fs.AddFollow(currentUser, targetUser))
                        {
                            return "true";
                        }
                        else {
                            return "Something is wrong. Try again later.";
                        }
                    }
                    else {
                        return "Already follow";
                    }
                }
            }

            return "Not logged in";
        }

        [HttpPost, Route("unfollow")]
        public string Unfollow(int targetUser) {
            int currentUser;

            FollowService fs = new FollowService();

            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    currentUser = UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;

                    if (fs.CheckFollow(currentUser, targetUser))
                    {
                        if (fs.DeleteFollow(currentUser, targetUser))
                        {
                            return "true";
                        }
                        else
                        {
                            return "Something is wrong. Try again later.";
                        }
                    }
                    else
                    {
                        return "Already unfollow";
                    }
                }
            }

            return "Not logged in";
        }

        [HttpGet, Route("followers")]
        public IActionResult Followers() {

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

                    FollowService fs = new FollowService();
                    UserService us = new UserService();

                    List<FollowModel> followers = fs.GetFollowersByTargetUser(id);

                    List<FollowViewModel> followersLikeUser = new List<FollowViewModel>();

                    foreach (var i in followers) {
                        UserModel followLikeUser = us.GetUserById(i.CurrentUser);
                        if (user != null)
                        {
                            followersLikeUser.Add(new FollowViewModel(followLikeUser.Id, followLikeUser.Nickname, followLikeUser.FirstName, followLikeUser.LastName, fs.CheckFollow(id, user.Id)));
                        }
                    }

                    if (followers.Count > 0)
                    {
                        return Json(new { status = true, message = "", followers = followersLikeUser });
                    }
                    else
                    {
                        return Json(new { status = true, message = "No followers", followers = new List<SimpleUserModel>() });
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

        [HttpGet, Route("following")]
        public IActionResult Following()
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
                    FollowService fs = new FollowService();
                    UserService us = new UserService();

                    List<FollowModel> following = fs.GetFollowersByCurrentUser(id);

                    List<SimpleUserModel> followingLikeUser = new List<SimpleUserModel>();

                    foreach (var i in following)
                    {
                        UserModel followLikeUser = us.GetUserById(i.TargetUser);
                        if (user != null) {
                            followingLikeUser.Add(new SimpleUserModel(followLikeUser.Id, followLikeUser.Nickname, followLikeUser.FirstName, followLikeUser.LastName));
                        }
                    }

                    if (following.Count > 0)
                    {
                        return Json(new { status = true, message = "", following = followingLikeUser });
                    }
                    else
                    {
                        return Json(new { status = true, message = "No following", following = new List<SimpleUserModel>() });
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
