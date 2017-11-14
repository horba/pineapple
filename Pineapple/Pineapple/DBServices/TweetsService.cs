using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using Pineapple.Model;
using System.Data;

namespace Pineapple.DBServices
{

    public class TweetsService : ITweetsService
    {

        //public void AddTweet(string tweet) {
        //    StreamWriter sr = new StreamWriter("App_Data/Tweets.txt",true);
        //    sr.WriteLine(tweet);
        //    sr.Close();
        //}




        //public List<Tweet> GetLimitTweets(int limit) {
        //    StreamReader FileReader = File.OpenText("App_Data/Tweets.txt");
        //    List<string> allLines = new List<string>();
        //    List<Tweet> allTweets = new List<Tweet>();
        //    while (true)
        //    {
        //        string str = FileReader.ReadLine();
        //        if (str == null)
        //            break;
        //        allLines.Add(str);
        //    }
        //    FileReader.Close();
        //    for (int i = 0; i < limit; i++)
        //    {
        //        string[] elements = allLines.ElementAt(allLines.Count - limit+i).Split(' ');
        //        Tweet tweet = new Tweet(elements[0], elements[1], elements[2]);
        //        allTweets.Add(tweet);
        //    }
        //    return allTweets;
        //}



        //public List<Tweet> GetAllTweets()
        //{
        //    StreamReader FileReader = File.OpenText("App_Data/Tweets.txt");
        //    List<string> allLines = new List<string>();
        //    List<Tweet> allTweets = new List<Tweet>();
        //    while (true)
        //    {
        //        string str = FileReader.ReadLine();
        //        if (str == null)
        //            break;
        //        allLines.Add(str);
        //    }
        //    FileReader.Close();
        //    for (int i = 0; i < allLines.Count; i++)
        //    {
        //        string[] elements = allLines.ElementAt(i).Split(' ');
        //        Tweet tweet = new Tweet(elements[0], elements[1], elements[2]);
        //        allTweets.Add(tweet);
        //    }
        //    return allTweets;
        //}

        public void AddTweet(string tweet)
        {
            DBconnection.ConnectionOpen();
            try
            {
                SqlCommand myCommand = new SqlCommand(
                    "INSERT INTO dbo.Tweets (text, date) VALUES (@ParamText, @ParamDate)",
                                                         DBconnection.myConnection);
                SqlParameter ParamText = myCommand.Parameters.Add("@ParamText", SqlDbType.VarChar, 50);
                ParamText.Value = tweet+ " from " + DateTime.Now.ToShortDateString();
                SqlParameter ParamDate = myCommand.Parameters.Add("@ParamDate", SqlDbType.DateTime);
                ParamDate.Value = DateTime.Now;
                myCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            DBconnection.ConnectionClose();
        }


        public List<Tweet> GetAllTweets()
        {
            List<Tweet> result = new List<Tweet>();
            DBconnection.ConnectionOpen();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from dbo.Tweets", DBconnection.myConnection);
                myReader = myCommand.ExecuteReader();
                string[] fields = { "id", "text", "date" };
                while (myReader.Read())
                {
                    Tweet cortage = new Tweet(myReader[fields[0]].ToString(), myReader[fields[1]].ToString(), myReader[fields[2]].ToString());
                    result.Add(cortage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            DBconnection.ConnectionClose();
            return result;

        }

        public List<Tweet> GetLimitTweets(int limit)
        {
            List<Tweet> result = new List<Tweet>();
            DBconnection.ConnectionOpen();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(String.Format("select top {0} * from dbo.Tweets order by id desc",limit), DBconnection.myConnection);
                myReader = myCommand.ExecuteReader();
                string[] fields = { "id", "text", "date" };
                while (myReader.Read())
                {
                    Tweet cortage = new Tweet(myReader[fields[0]].ToString(), myReader[fields[1]].ToString(), myReader[fields[2]].ToString());
                    result.Add(cortage);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            DBconnection.ConnectionClose();
            return result;
        }


    }
}