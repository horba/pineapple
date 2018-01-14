using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class LoginResponseModel
    {
        public string Status { get; set; }
        public string Error { get; set; }
        public LoginResponseModel(string status, string error)
        {
            Status = status;
            Error = error;
        }
    }
}
