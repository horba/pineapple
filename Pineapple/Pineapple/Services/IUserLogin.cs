﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pineapple.Model;

namespace Pineapple.Services
{
    public interface IUserLogin
    {
        LoginResponseModel Login(LoginModel loginModel);
    }
}