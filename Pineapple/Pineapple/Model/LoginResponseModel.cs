using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class LoginResponseModel
    {
        public string status { get; set; }
        public string error { get; set; }
        public LoginResponseModel(string status, string error)
        {
            this.status = status;
            this.error = error;
        }
    }
}
