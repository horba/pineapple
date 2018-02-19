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

        public void AddTweet(string tweet)
        {
            DBconnection.ConnectionOpen();
            try
            {
                SqlCommand myCommand = new SqlCommand(
                    "INSERT INTO dbo.Tweets (text, date) VALUES (@ParamText, @ParamDate)",
                                                         DBconnection.myConnection);
                SqlParameter ParamText = myCommand.Parameters.Add("@ParamText", SqlDbType.VarChar, 50);
                ParamText.Value = tweet;
                SqlParameter ParamDate = myCommand.Parameters.Add("@ParamDate", SqlDbType.DateTime);
                ParamDate.Value = DateTime.UtcNow;
                myCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            DBconnection.ConnectionClose();
        }

        public void AddTweet(string tweet, int idOfAuthor)
        {
            DBconnection.ConnectionOpen();
            try
            {
                SqlCommand myCommand = new SqlCommand(
                    "INSERT INTO dbo.Tweets (text, date, idOfAuthor) VALUES (@ParamText, @ParamDate, @ParamIdOfAuthor)",
                                                         DBconnection.myConnection);
                myCommand.Parameters.AddWithValue("@ParamText", tweet);
                myCommand.Parameters.AddWithValue("@ParamDate", DateTime.Now);
                myCommand.Parameters.AddWithValue("@ParamIdOfAuthor", idOfAuthor);
                myCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            DBconnection.ConnectionClose();
        }


        public List<TweetModel> GetAllTweets()
        {
            List<TweetModel> result = new List<TweetModel>();
            DBconnection.ConnectionOpen();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from dbo.Tweets", DBconnection.myConnection);
                myReader = myCommand.ExecuteReader();
                string[] fields = { "id",  "date" ,"text", "idOfAuthor"};
                while (myReader.Read())
                {
                    TweetModel cortage = new TweetModel(Convert.ToInt32(myReader[fields[0]]), Convert.ToDateTime(myReader[fields[1]]), myReader[fields[2]].ToString(), Convert.ToInt32(myReader[fields[3]]));
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

        public List<TweetModel> GetTweetsFromFeed(int idOfCurrentUser)
        {
            const int limitOfFeedByDays = 2;
            List<TweetModel> result = new List<TweetModel>();
            FollowService followService = new FollowService();
            List<FollowModel> SendersOfTweets = followService.GetFollowersByCurrentUser(idOfCurrentUser);
            if (SendersOfTweets.Count == 0)
            {
                return null;
            }
            string SendersID = "";
            for (int i = 0; i < SendersOfTweets.Count - 1; i++)
            {
                SendersID += SendersOfTweets[i].TargetUser + ", ";
            }
            SendersID += SendersOfTweets[SendersOfTweets.Count - 1].TargetUser;
            DBconnection.ConnectionOpen();
            try
            {
                SqlDataReader myReader = null;
                DateTime timeLimit = DateTime.UtcNow;
                timeLimit.AddDays(-limitOfFeedByDays);
                SqlCommand myCommand = new SqlCommand("SELECT * FROM dbo.Tweets WHERE idOfAuthor IN(" + SendersID + ") AND date > @ParamDate ORDER BY date DESC", DBconnection.myConnection);
                SqlParameter ParamDate = myCommand.Parameters.AddWithValue("@ParamDate", timeLimit);
                myReader = myCommand.ExecuteReader();
                string[] fields = { "id", "date", "text", "idOfAuthor" };
                while (myReader.Read())
                {
                    TweetModel cortage = new TweetModel(Convert.ToInt32(myReader[fields[0]]), Convert.ToDateTime(myReader[fields[1]]), myReader[fields[2]].ToString(), Convert.ToInt32(myReader[fields[3]]));
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

        public List<TweetModel> GetLimitTweets(int limit)
        {
            List<TweetModel> result = new List<TweetModel>();
            DBconnection.ConnectionOpen();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(String.Format("select top {0} * from dbo.Tweets order by id desc",limit), DBconnection.myConnection);
                myReader = myCommand.ExecuteReader();
                string[] fields = { "id", "date" , "text", "idOfAuthor" };
                while (myReader.Read())
                {
                    TweetModel cortage = new TweetModel(Convert.ToInt32(myReader[fields[0]]), Convert.ToDateTime(myReader[fields[1]]), myReader[fields[2]].ToString(), Convert.ToInt32(myReader[fields[3]]));
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

        public TweetModel GetTweetById(int id)
        {
            TweetModel result = null;
            DBconnection.ConnectionOpen();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(String.Format("select * from dbo.Tweets where id = {0}",id), DBconnection.myConnection);
                myReader = myCommand.ExecuteReader();
                string[] fields = { "id", "date", "text", "idOfAuthor" };
                myReader.Read();
                result = new TweetModel(Convert.ToInt32(myReader[fields[0]]), Convert.ToDateTime(myReader[fields[1]]), myReader[fields[2]].ToString(), Convert.ToInt32(myReader[fields[3]]));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            DBconnection.ConnectionClose();
            return result;
        }

        public void ChangeTweet(int id, string tweet)
        {
            DBconnection.ConnectionOpen();
            try
            {
                SqlCommand myCommand = new SqlCommand("UPDATE dbo.Tweets SET text = @ParamText, date = @ParamDate where id =  @ParamID",
                                                         DBconnection.myConnection);
                SqlParameter ParamText = myCommand.Parameters.Add("@ParamText", SqlDbType.VarChar, 50);
                ParamText.Value = tweet + " from " + DateTime.Now.ToShortDateString();
                SqlParameter ParamDate = myCommand.Parameters.Add("@ParamDate", SqlDbType.DateTime);
                ParamDate.Value = DateTime.Now;
                SqlParameter ParamID = myCommand.Parameters.Add("@ParamID", SqlDbType.Int);
                ParamID.Value = id;
                myCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            DBconnection.ConnectionClose();
        }

        public void DeleteTweet(int id)
        {
            DBconnection.ConnectionOpen();
            try
            {
                SqlCommand myCommand = new SqlCommand("DELETE FROM dbo.Tweets  WHERE id =  @ParamID",
                                                         DBconnection.myConnection);
                SqlParameter ParamID = myCommand.Parameters.Add("@ParamID", SqlDbType.Int);
                ParamID.Value = id;
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            DBconnection.ConnectionClose();
        }





    }
}