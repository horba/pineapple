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
    public class TweetsController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> stringTweets = new List<string>();
            TweetsService modelReader = new TweetsService();
            if (Request.Cookies.ContainsKey("session_id"))
            {
                int idOfCurrentUser = UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;
                List<TweetModel> tweetsFromFeed = modelReader.GetTweetsFromFeed(idOfCurrentUser);
                if (tweetsFromFeed != null)
                {
                    foreach (TweetModel tweet in tweetsFromFeed)
                    {
                        stringTweets.Add(tweet.ToString());
                    }
                }
            }
            return stringTweets;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            TweetsService modelReader = new TweetsService();
            TweetModel tweet = modelReader.GetTweetById(id);
            return tweet.ToString();
        }

        // POST api/values
        [HttpPost]
        public void Post(string value)
        {
            TweetsService modelReader = new TweetsService();
            if (Request.Cookies.ContainsKey("session_id"))
            {
                int idOfAuthor = Services.UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;
                modelReader.AddTweet(value, idOfAuthor);
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
        public object myTweets() {
            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    int id = UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;
                    TweetsService modelReader = new TweetsService();
                    UserService us = new UserService();

                    List<TweetModel> tweets = modelReader.GetTweetsByUserId(id);

                    List<object> response = new List<object>();
                    foreach (var tweet in tweets) {
                        UserModel user = us.GetUserById(tweet.IdOfAuthor);
                        string nickname = user == null ? "Error" : user.Nickname;
                        response.Add(new { text = tweet.Text, date = tweet.Date.ToString(), nickname });
                    }

                    if (response.Count > 0)
                    {
                        return new { status = "true", tweets = response };
                    }
                    else {
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
