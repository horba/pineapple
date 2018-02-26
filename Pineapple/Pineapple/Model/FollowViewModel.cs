using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class FollowViewModel
    {
        public FollowViewModel(int id, string nickname, string firstName, string lastName, bool follow)
        {
            Id = id;
            Nickname = nickname;
            FirstName = firstName;
            LastName = lastName;
            Follow = follow;
        }

        public FollowViewModel(int id, string nickname, string firstName, string lastName, bool follow, bool followButton)
        {
            Id = id;
            Nickname = nickname;
            FirstName = firstName;
            LastName = lastName;
            Follow = follow;
            FollowButton = followButton;
        }

        public FollowViewModel(){}

        public int Id { get; set; }
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Follow { get; set; }
        public bool FollowButton { get; set; }
    }
}
