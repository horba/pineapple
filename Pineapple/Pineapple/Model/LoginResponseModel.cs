using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pineapple.Model
{
    public class LoginResponseModel
    {
        public bool Status { get; set; }
        public string SessionId { get; set; }
        public string Error { get; set; }
        public LoginResponseModel(bool status, string sessionId, string error)
        {
            Status = status;
            SessionId = sessionId;
            Error = error;
        }
    }
}
