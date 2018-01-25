using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pineapple.Model;

namespace Pineapple.DBServices
{
    interface IFollowService
    {
        void AddFollow(int currentUser, int targetUser);
        List<FollowModel> GetAllFollowers();
        List<FollowModel> GetFollowersByCurrentUser(int currentUser);
        List<FollowModel> GetFollowersByTargertUser(int targetUser);
        void DeleteFollow(int currentUser, int targetUser);
    }
}
