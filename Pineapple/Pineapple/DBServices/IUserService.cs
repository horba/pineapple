using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pineapple.Model;

namespace Pineapple.DBServices
{
    interface IUserService
    {
        List<UserModel> GetUsers();

        string CheckUserNick(string nick);
        string CheckUserEmail(string email);
        string CheckUserFirstName(string firstName);
        string CheckUserSecondName(string secondName);
        string CheckUserPassword(string password);
        string CheckUserRPassword(string password, string rPassword);

        string RegisterUser(RegisterData data);
    }
}
