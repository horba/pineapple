using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.DBServices;
using Pineapple.Model;


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
                int idOfCurrentUser = Services.UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;
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
    }
}
