using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pineapple.Model;

namespace Pineapple.DBServices
{
    interface IFollowService
    {
        bool AddFollow(int currentUser, int targetUser);
        List<FollowModel> GetAllFollowers();
        List<FollowModel> GetFollowersByCurrentUser(int currentUser);
        List<FollowModel> GetFollowersByTargetUser(int targetUser);
        bool DeleteFollow(int currentUser, int targetUser);
        bool CheckFollow(int currentUser, int targetUser);
    }
}
