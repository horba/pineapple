using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Nick{ get; set; }
        public bool CurrentFollow { get; set; }

        public UserModel(int id, string nick, bool currentFollow) {
            Id = id;
            Nick = nick;
            CurrentFollow = currentFollow;
        }
    }
}
