﻿using System;
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
        public object Followers() {

            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    int id = UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;
                    FollowService fs = new FollowService();
                    UserService us = new UserService();

                    List<FollowModel> followers = fs.GetFollowersByTargetUser(id);

                    List<SimpleUserModel> followersLikeUser = new List<SimpleUserModel>();

                    foreach (var i in followers) {
                        UserModel user = us.GetUserById(i.CurrentUser);
                        followersLikeUser.Add(new SimpleUserModel(user.Id, user.Nickname, user.FirstName, user.LastName));
                    }

                    if (followers.Count > 0)
                    {
                        return new { status = "true", followers = followersLikeUser };
                    }
                    else
                    {
                        return new { status = "empty" };
                    }
                }
                else
                {
                    return new { status = "error" };
                }
            }
            else
            {
                return Json(new { status = "error" });
            }
        }

        [HttpGet, Route("following")]
        public object Following()
        {
            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    int id = UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;
                    FollowService fs = new FollowService();
                    UserService us = new UserService();

                    List<FollowModel> following = fs.GetFollowersByCurrentUser(id);

                    List<SimpleUserModel> followingLikeUser = new List<SimpleUserModel>();

                    foreach (var i in following)
                    {
                        UserModel user = us.GetUserById(i.TargetUser);
                        followingLikeUser.Add(new SimpleUserModel(user.Id, user.Nickname, user.FirstName, user.LastName));
                    }

                    if (following.Count > 0)
                    {
                        return new { status = "true", following = followingLikeUser };
                    }
                    else
                    {
                        return new { status = "empty" };
                    }
                }
                else
                {
                    return new { status = "error" };
                }
            }
            else
            {
                return Json(new { status = "error" });
            }
        }
    }
}
