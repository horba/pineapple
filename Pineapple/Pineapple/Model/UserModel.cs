﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class UserModel
    {
        public UserModel(int id, string nick, string firstName, string secondName, string email, string password)
        {
            Id = id;
            Nick = nick;
            FirstName = firstName;
            SecondName = secondName;
            Email = email;
            Password = password;
        }

        public UserModel()
        {

        }

        public int Id { get; set; }
        public string Nick { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RPassword { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return Id + " " + Nick + " " + FirstName + " "+ SecondName + " " + Email + " " + Password;
        }

        public string ToStringWithoutPassword()
        {
            return Id + " " + Nick + " " + FirstName + " " + SecondName + " " + Email;
        }
    }
}