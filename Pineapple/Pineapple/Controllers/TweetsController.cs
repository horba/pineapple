using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.DBServices;
using Pineapple.Model;
using System.Data;


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
                if (Services.UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    int idOfCurrentUser = Services.UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;
                    TweetsService modelReader = new TweetsService();
                    UserService us = new UserService();
                    List<TweetModel> tweets = modelReader.GetTweetsFromFeed(idOfCurrentUser);
                    foreach (var tweet in tweets)
                    {
                        UserModel user = us.GetUserById(tweet.IdOfAuthor);
                        string nickname = user == null ? "Error" : user.Nickname;
                        response.Add(new TweetViewModel (tweet, nickname));
                    }                    
                }
            }
            return response;
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
                modelReader.AddTweet(new TweetModel(DateTime.UtcNow, value, idOfAuthor));
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
    }
}
