using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;
using Pineapple.Model;
using System.Data;

namespace Pineapple.DBServices
{
    public class FollowService : IFollowService
    {

        public bool AddFollow(int currentUser, int targetUser)
        {
            bool status = true;

            DBconnection.ConnectionOpen();
            try
            {
                SqlCommand myCommand = new SqlCommand(
                    "INSERT INTO dbo.Followers (CurrentID, TargetID) VALUES (@ParamCurrent, @ParamTarget)",
                                                         DBconnection.myConnection);
                myCommand.Parameters.AddWithValue("@ParamCurrent", currentUser);
                myCommand.Parameters.AddWithValue("@ParamTarget", targetUser);
                myCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                status = false;
            }
            DBconnection.ConnectionClose();

            return status;
        }
        
        public List<FollowModel> GetAllFollowers()
        {
            List<FollowModel> result = new List<FollowModel>();
            DBconnection.ConnectionOpen();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from dbo.Followers", DBconnection.myConnection);
                myReader = myCommand.ExecuteReader();
                string[] fields = { "CurrentID", "TargetID" };
                while (myReader.Read())
                {
                    FollowModel cortage = new FollowModel(Convert.ToInt32(myReader[fields[0]]), Convert.ToInt32(myReader[fields[1]]));
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

        public List<FollowModel> GetFollowersByCurrentUser(int currentUser)
        {
            List<FollowModel> result = new List<FollowModel>();
            DBconnection.ConnectionOpen();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(String.Format("select * from dbo.Followers where CurrentID = {0}", currentUser), DBconnection.myConnection);
                myReader = myCommand.ExecuteReader();
                string[] fields = { "CurrentID", "TargetID" };
                while (myReader.Read())
                {
                    FollowModel cortage = new FollowModel(Convert.ToInt32(myReader[fields[0]]), Convert.ToInt32(myReader[fields[1]]));
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

        public List<FollowModel> GetFollowersByTargetUser(int targetUser)
        {
            List<FollowModel> result = new List<FollowModel>();
            DBconnection.ConnectionOpen();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(String.Format("select * from dbo.Followers where TargetID = {0}", targetUser), DBconnection.myConnection);
                myReader = myCommand.ExecuteReader();
                string[] fields = { "CurrentID", "TargetID" };
                while (myReader.Read())
                {
                    FollowModel cortage = new FollowModel(Convert.ToInt32(myReader[fields[0]]), Convert.ToInt32(myReader[fields[1]]));
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

        public void DeleteFollow(int currentUser, int targetUser)
        {
            DBconnection.ConnectionOpen();
            try
            {
                SqlCommand myCommand = new SqlCommand(String.Format("DELETE FROM dbo.Followers  WHERE CurrentID = {0} AND TargetID = {1}", currentUser, targetUser)
                    , DBconnection.myConnection);
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            DBconnection.ConnectionClose();
        }

        public bool CheckFollow(int currentUser, int targetUser)
        {
            bool result = true;
            DBconnection.ConnectionOpen();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(String.Format("SELECT count(*) FROM dbo.Followers WHERE CurrentID = {0} AND TargetID = {1}", currentUser, targetUser), DBconnection.myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    result = Convert.ToBoolean(myReader[0]);
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
