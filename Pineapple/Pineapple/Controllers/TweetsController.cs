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
            TweetsService modelReader = new TweetsService();
            List<Tweet> allTweets = modelReader.GetAllTweets();
            List<string> stringTweets = new List<string>();
            foreach (Tweet tweet in allTweets)
            {
                stringTweets.Add(tweet.ToString());
            }
            return stringTweets;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            TweetsService modelReader = new TweetsService();
            Tweet tweet = modelReader.GetTweetById(id);
            return tweet.ToString();
        }

        // POST api/values
        [HttpPost]
        public void Post(string value)
        {
            TweetsService modelReader = new TweetsService();
            modelReader.AddTweet(value);
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
