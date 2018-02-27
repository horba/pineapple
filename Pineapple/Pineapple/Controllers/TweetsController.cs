using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.DBServices;
using Pineapple.Model;
using System.Data;
using Pineapple.Services;

namespace Pineapple.Controllers
{
    [Route("api/[controller]")]
    public class TweetsController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<TweetViewModel> Get()
        {   
            List<TweetViewModel> response = new List<TweetViewModel>();
            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    int CurrentUserId = UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;
                    TweetsService modelReader = new TweetsService();
                    UserService us = new UserService();
                    List<TweetModel> tweets = modelReader.GetTweetsFromFeed(CurrentUserId);
                    foreach (var tweet in tweets)
                    {
                        UserModel user = us.GetUserById(tweet.AuthorId);
                        string nickname = user == null ? "Error" : user.Nickname;
                        response.Add(new TweetViewModel (tweet, nickname));
                    }                    
                }
            }
            return response;
        }

        [HttpGet ("TweeetsById/{Id}")]
        public IEnumerable<TweetViewModel> Get(int id)
        {
            List<TweetViewModel> response = new List<TweetViewModel>();
            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    TweetsService modelReader = new TweetsService();
                    UserService us = new UserService();
                    List<TweetModel> tweets = modelReader.GetTweetsByUserId(id);
                    foreach (var tweet in tweets)
                    {
                        UserModel user = us.GetUserById(tweet.AuthorId);
                        string nickname = user == null ? "Error" : user.Nickname;
                        tweet.Date = tweet.Date.ToLocalTime();
                        response.Add(new TweetViewModel(tweet, nickname));
                    }
                }
            }
            return response;
        }

        // POST api/values
        [HttpPost]
        public void Post(string value)
        {
            TweetsService modelReader = new TweetsService();
            if (Request.Cookies.ContainsKey("session_id"))
            {
                int AuthorId = Services.UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;
                modelReader.AddTweet(new TweetModel(DateTime.UtcNow, value, AuthorId));
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            TweetsService modelReader = new TweetsService();
            modelReader.ChangeTweet(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            TweetsService modelReader = new TweetsService();
            modelReader.DeleteTweet(id);
        }

        [HttpGet, Route("myTweets")]
        public IActionResult myTweets() {
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
                    else {
                        return Json(new { status = false, message = "Error: User not found" });
                    }

                    TweetsService modelReader = new TweetsService();
                    UserService us = new UserService();

                    List<TweetModel> tweets = modelReader.GetTweetsByUserId(id);

                    List<TweetViewModel> response = new List<TweetViewModel>();
                    foreach (var tweet in tweets) {
                        UserModel AuthorTweet = us.GetUserById(tweet.AuthorId);
                        string nickname = user == null ? "Error" : AuthorTweet.Nickname;
                        response.Add(new TweetViewModel(tweet, nickname));
                    }

                    return Json(new { status = true, message = "", tweets = response });
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
