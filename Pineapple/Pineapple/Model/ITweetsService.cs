using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Pineapple.Model
{

    interface ITweetsService{
      void AddTweet(string tweet);
      string[] GetTweets(int limit);
    }


}