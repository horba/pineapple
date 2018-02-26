using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class SimpleUserModel
    {
        public SimpleUserModel(int id, string nickname, string firstName, string lastName)
        {
            Id = id;
            Nickname = nickname;
            FirstName = firstName;
            LastName = lastName;
        }

        public SimpleUserModel(){}

        public int Id { get; set; }
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
