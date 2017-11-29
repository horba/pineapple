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
                ParamText.Value = tweet + " from " + DateTime.Now.ToShortDateString();
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
                string[] fields = { "id",  "date" ,"text"};
                while (myReader.Read())
                {
                    Tweet cortage = new Tweet(Convert.ToInt32(myReader[fields[0]]), Convert.ToDateTime(myReader[fields[1]]), myReader[fields[2]].ToString());
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
                string[] fields = { "id", "date" , "text"};
                while (myReader.Read())
                {
                    Tweet cortage = new Tweet(Convert.ToInt32(myReader[fields[0]]), Convert.ToDateTime(myReader[fields[1]]), myReader[fields[2]].ToString());
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

        public Tweet GetTweetById(int id)
        {
            Tweet result = null;
            DBconnection.ConnectionOpen();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(String.Format("select * from dbo.Tweets where id = {0}",id), DBconnection.myConnection);
                myReader = myCommand.ExecuteReader();
                string[] fields = { "id", "date", "text" };
                myReader.Read();
                result = new Tweet(Convert.ToInt32(myReader[fields[0]]), Convert.ToDateTime(myReader[fields[1]]), myReader[fields[2]].ToString());
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