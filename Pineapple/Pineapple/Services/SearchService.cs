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
        public SearchResponseModel FindPeoples(SearchModel searchModel)
        {
            DBconnection.ConnectionOpen();
            string[] searchText = searchModel.SearchText.Split(' ');
            foreach (var item in searchText)
            {
                HelpMethod(item);
            }
            FindedUsers = FindedUsers.Distinct().ToList();
            SearchResponseModel SRM = new SearchResponseModel();
            SRM.Users = FindedUsers;
            return SRM;
        }

        private List<UserModel> FindUsersByReader(SqlDataReader dataReader)
        {
            UserModel FindedUser;
            List<UserModel> AllFindedUsers = new List<UserModel>();
            while (dataReader.Read())
            {
                FindedUser = new UserModel();
                FindedUser.Id = (int)dataReader.GetValue(0);
                FindedUser.Nickname = (string)dataReader.GetValue(1);
                FindedUser.FirstName = (string)dataReader.GetValue(2);
                FindedUser.LastName = (string)dataReader.GetValue(3);
                FindedUser.Email = (string)dataReader.GetValue(4);
                FindedUser.Password = (string)dataReader.GetValue(5);
                AllFindedUsers.Add(FindedUser);
            }
            return AllFindedUsers;
        }

        private void HelpMethod(string SearachText)
        {
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Users WHERE Nick = @ParamNick", DBconnection.myConnection);
            sqlCommand.Parameters.AddWithValue("@ParamNick", SearachText);
            SqlDataReader myDataReader = sqlCommand.ExecuteReader();
            FindedUsers = FindUsersByReader(myDataReader);
            myDataReader.Close();

            sqlCommand.CommandText = "SELECT * FROM Users WHERE FirstName = @ParamNick";
            myDataReader = sqlCommand.ExecuteReader();
            FindedUsers.AddRange(FindUsersByReader(myDataReader));
            myDataReader.Close();


            sqlCommand.CommandText = "SELECT * FROM Users WHERE SecondName = @ParamNick";
            myDataReader = sqlCommand.ExecuteReader();
            FindedUsers.AddRange(FindUsersByReader(myDataReader));
            myDataReader.Close();
        }      
    }
}
