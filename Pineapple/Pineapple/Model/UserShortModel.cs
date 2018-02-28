using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class UserShortModel
    {

        public UserShortModel(int id, string nickname, string firstName, string lastName, string email)
        {
            Id = id;
            Nickname = nickname;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public UserShortModel()
        {

        }

        public int Id { get; set; }
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return Id + " " + Nickname + " " + FirstName + " " + LastName + " " + Email;
        }
        

    }
}
