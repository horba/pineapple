﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pineapple.Model;

namespace Pineapple.Services
{
    public interface ISearchService
    {
        List<UserModel> FindPeoples(SearchModel searchModel);
    }
}
