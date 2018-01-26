using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class UserModel
    {
        public string Nick{ get; set; }

        public UserModel(string nick) {
            Nick = nick;
        }
    }
}
