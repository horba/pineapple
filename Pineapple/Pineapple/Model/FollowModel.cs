using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class FollowModel
    {
        public int CurrentUser { get; set; }
        public int TargetUser { get; set; }
        public FollowModel(int currentUser,int targetUser)
        {
            CurrentUser = currentUser;
            TargetUser = targetUser;
        }


        public override string ToString()
        {
            return String.Format("{0} {1}", this.CurrentUser, this.TargetUser);
        }
    }
}
