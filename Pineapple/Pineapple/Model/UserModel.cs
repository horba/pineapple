using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class UserModel : IEquatable<UserModel>
    {
        public UserModel(int id, string nickname, string firstName, string lastName, string email, string password)
        {
            Id = id;
            Nickname = nickname;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public UserModel()
        {

        }

        public int Id { get; set; }
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RPassword { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return Id + " " + Nickname + " " + FirstName + " "+ LastName + " " + Email + " " + Password;
        }

        public string ToStringWithoutPassword()
        {
            return Id + " " + Nickname + " " + FirstName + " " + LastName + " " + Email;
        }

        public bool Equals(UserModel other)
        {
            return Nickname.Equals(other.Nickname);
        }
    }
}
