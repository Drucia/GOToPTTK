using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOToPTTK.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GOToPTTK.Controllers.MvcControllers
{
    [Route("wykaz")]
    public class RouteListController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public RouteListController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var routeLists = _dbContext.WykazTras.ToList();
            return View(routeLists);
        }
    }
}