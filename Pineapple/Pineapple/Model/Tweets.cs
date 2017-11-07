using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Pineapple.Model
{

    public class Tweets : ITweetsService
    {

        public void AddTweet(string tweet) {
            StreamWriter sr = new StreamWriter("App_Data/Tweets.txt",true);
            sr.WriteLine(tweet);
            sr.Close();
        }

        public string[] GetTweets(int limit) {
            StreamReader FileReader = File.OpenText("App_Data/Tweets.txt");
            string[] allTweets = new string[limit*3];
            List<string> allLines = new List<string>();
            while (true)
            {
                string str = FileReader.ReadLine();
                if (str == null)
                    break;
                allLines.Add(str);
            }
            FileReader.Close();
            for (int i = 0; i < limit; i++)
            {
                string[] elements = allLines.ElementAt(allLines.Count - limit+i).Split(' ');
                    allTweets[i*3] = elements[0];
                    allTweets[3*i+1] = elements[1];
                    allTweets[3*i+2] = elements[2];
            }
            return allTweets;
        }



    }
}