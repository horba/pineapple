﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pineapple.Model;
using Pineapple.Services;
namespace Pineapple.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        ISearchService SearchEngine;
        public SearchController(ISearchService mySearchService)
        {
            SearchEngine = mySearchService;
        }
        [HttpPost]
        public void Searach(SearchModel searchModel)
        {
            SearchEngine.FindPeoples(searchModel);
        }

        [HttpGet("searchFollowers/{searchLine}")]
        public object SearchInFollowers(string searchLine) {
            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    int id = UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;

                    List<SimpleUserModel> response = SearchEngine.FindPeoplesInFollowers(searchLine, id);

                    if (response.Count > 0)
                    {
                        return new { status = "true", foundPeople = response };
                    }
                    else
                    {
                        return new { status = "empty" };
                    }
                }
                else
                {
                    return new { status = "error" };
                }
            }
            else
            {
                return new { status = "error" };
            }
        }

        [HttpGet("searchFollowing/{searchLine}")]
        public object SearchInFollowing(string searchLine)
        {
            if (Request.Cookies.ContainsKey("session_id"))
            {
                if (UserAuth.CheckUserSession(Request.Cookies["session_id"]))
                {
                    int id = UserAuth.GetUserBySession(Request.Cookies["session_id"]).Id;

                    List<SimpleUserModel> response = SearchEngine.FindPeoplesInFollowing(searchLine, id);

                    if (response.Count > 0)
                    {
                        return new { status = "true", foundPeople = response };
                    }
                    else
                    {
                        return new { status = "empty" };
                    }
                }
                else
                {
                    return new { status = "error" };
                }
            }
            else
            {
                return new { status = "error" };
            }
        }
    }
}
