using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
