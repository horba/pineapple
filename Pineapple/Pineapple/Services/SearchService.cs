using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pineapple.Model;
using Pineapple.DBServices;
using System.Data.SqlClient;

namespace Pineapple.Services
{
    public class SearchService : ISearchService
    {
        List<UserModel> FindedUsers = new List<UserModel>();
        public List<UserModel> FindPeoples(SearchModel searchModel)
        {
            if (searchModel.SearchText == null)
            {
                return FindedUsers;
            }
            DBconnection.ConnectionOpen();
            string[] searchText = searchModel.SearchText.Split(' ');
            foreach (var item in searchText)
            {
                SqlQuery(item);
            }
            FindedUsers = FindedUsers.Distinct().ToList();
            return FindedUsers;
        }

        private List<UserModel> FindUsersByReader(SqlDataReader dataReader)
        {
            UserModel FindedUser;
            List<UserModel> AllFindedUsers = new List<UserModel>();
            while (dataReader.Read())
            {
                FindedUser = new UserModel();
                FindedUser.Id = Convert.ToInt32(dataReader["Id"]);
                FindedUser.Nickname = (string)dataReader["Nick"];
                FindedUser.FirstName = (string)dataReader["FirstName"];
                FindedUser.LastName = (string)dataReader["SecondName"];
                AllFindedUsers.Add(FindedUser);
            }
            return AllFindedUsers;
        }

        private void SqlQuery(string SearachText)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM dbo.Users WHERE Nick LIKE @ParamNick OR FirstName LIKE @ParamNick OR SecondName LIKE @ParamNick", DBconnection.myConnection);
                sqlCommand.Parameters.AddWithValue("@ParamNick", "%" + SearchText + "%");
                SqlDataReader myDataReader = sqlCommand.ExecuteReader();
                FindedUsers.AddRange(FindUsersByReader(myDataReader));
                myDataReader.Close();
            }
            catch(Exception ex) {
                LogUsing4net.WriteError(ex.Message);
            }
        }

        public List<SimpleUserModel> FindPeoplesInFollowers(string searchLine, int userId) {

            List<SimpleUserModel> users = new List<SimpleUserModel>();

            DBconnection.ConnectionOpen();

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM dbo.Users WHERE (Nick LIKE @ParamNick OR FirstName LIKE @ParamNick OR SecondName LIKE @ParamNick) AND " +
                                                        "Id IN (SELECT CurrentID FROM dbo.Followers WHERE TargetID = @ParamId)", DBconnection.myConnection);
                sqlCommand.Parameters.AddWithValue("@ParamNick", "%" + searchLine + "%");
                sqlCommand.Parameters.AddWithValue("@ParamId", userId);
                SqlDataReader myDataReader = sqlCommand.ExecuteReader();

                while (myDataReader.Read()) {
                    users.Add(new SimpleUserModel(Convert.ToInt32(myDataReader["Id"]), Convert.ToString(myDataReader["Nick"]), Convert.ToString(myDataReader["FirstName"]),
                        Convert.ToString(myDataReader["SecondName"])));
                }

                myDataReader.Close();
            }
            catch(Exception ex) {
                LogUsing4net.WriteError(ex.Message);
            }

            DBconnection.ConnectionClose();

            return users;
        }

        public List<SimpleUserModel> FindPeoplesInFollowing(string searchLine, int userId)
        {

            List<SimpleUserModel> users = new List<SimpleUserModel>();

            DBconnection.ConnectionOpen();

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM dbo.Users WHERE (Nick LIKE @ParamNick OR FirstName LIKE @ParamNick OR SecondName LIKE @ParamNick) AND " +
                                                        "Id IN (SELECT TargetID FROM dbo.Followers WHERE CurrentID = @ParamId)", DBconnection.myConnection);
                sqlCommand.Parameters.AddWithValue("@ParamNick", "%" + searchLine + "%");
                sqlCommand.Parameters.AddWithValue("@ParamId", userId);
                SqlDataReader myDataReader = sqlCommand.ExecuteReader();

                while (myDataReader.Read())
                {
                    users.Add(new SimpleUserModel(Convert.ToInt32(myDataReader["Id"]), Convert.ToString(myDataReader["Nick"]), Convert.ToString(myDataReader["FirstName"]),
                        Convert.ToString(myDataReader["SecondName"])));
                }

                myDataReader.Close();
            }
            catch (Exception ex)
            {
                LogUsing4net.WriteError(ex.Message);
            }

            DBconnection.ConnectionClose();

            return users;
        }
    }
}
