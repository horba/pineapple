using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.Model;

namespace Pineapple.DBServices
{
    interface ITweetsService{
        void AddTweet(string tweet);
        List<TweetModel> GetLimitTweets(int limit);
        List<TweetModel> GetAllTweets();
        List<TweetModel> GetTweetsFromFeed(int idOfCurrentUser);
        TweetModel GetTweetById(int id);
        void ChangeTweet(int id, string tweet);
        void DeleteTweet(int id);
    }


}