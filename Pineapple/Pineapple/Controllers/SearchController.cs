using System;
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

        public IActionResult Searach(SearchModel searchModel)
        {
            List<UserModel> findedusers = SearchEngine.FindPeoples(searchModel);
            if (findedusers.Count == 0)
            {
                return Json(new { status = "empty"});
            }
            return Json(new {FindedPeoples = findedusers, status = "true"} );
        }
    }
}
