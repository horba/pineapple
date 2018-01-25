using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class LoginResponseModel
    {
        public string statuss { get; set; }
        public string errorr { get; set; }
        public LoginResponseModel(string status, string error)
        {
            this.statuss = status;
            this.errorr = error;
        }
    }
}
