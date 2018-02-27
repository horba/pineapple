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
        public IActionResult Follow(int targetUser)
        {
            FollowService fs = new FollowService();

            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"])) {

                    UserModel currentUser = UserAuth.GetUserBySession(Request.Cookies["session_id"]);
                    int currentUserId = 0;
                    if (currentUser != null)
                    {
                        currentUserId = currentUser.Id;
                    }
                    else
                    {
                        return Json(new { status = false, message = "Something is wrong. Try again later." });
                    }

                    if (currentUserId == targetUser) {
                        return Json(new { status = false,  message = "You can't follow yourself"});
                    }

                    if (!fs.CheckFollow(currentUserId, targetUser))
                    {
                        if (fs.AddFollow(currentUserId, targetUser))
                        {
                            return Json(new { status = true, message = "" });
                        }
                        else {
                            return Json(new { status = false, message = "Something is wrong. Try again later." });
                        }
                    }
                    else {
                        return Json(new { status = false, message = "Already follow" });
                    }
                }
            }

            return Json(new { status = false, message = "Not logged in" });
        }

        [HttpPost, Route("unfollow")]
        public IActionResult Unfollow(int targetUser) {

            FollowService fs = new FollowService();

            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    UserModel currentUser = UserAuth.GetUserBySession(Request.Cookies["session_id"]);
                    int currentUserId = 0;
                    if (currentUser != null)
                    {
                        currentUserId = currentUser.Id;
                    }
                    else {
                        return Json(new { status = false, message = "Something is wrong. Try again later." });
                    }

                    if (fs.CheckFollow(currentUserId, targetUser))
                    {
                        if (fs.DeleteFollow(currentUserId, targetUser))
                        {
                            return Json(new { status = true, message = "" });
                        }
                        else
                        {
                            return Json(new { status = false, message = "Something is wrong. Try again later." });
                        }
                    }
                    else
                    {
                        return Json(new { status = false, message = "Already follow" });
                    }
                }
            }

            return Json(new { status = false, message = "Not logged in" });
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

                    return Json(new { status = true, message = "", followers = followersLikeUser });
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

                    return Json(new { status = true, message = "", following = followingLikeUser });
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
