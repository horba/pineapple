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
        List<Tweet> GetLimitTweets(int limit);
        List<Tweet> GetAllTweets();
    }


}