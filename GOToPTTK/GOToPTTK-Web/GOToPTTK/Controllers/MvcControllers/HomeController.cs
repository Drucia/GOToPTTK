using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOToPTTK.Model.Services;
using Microsoft.AspNetCore.Mvc;

namespace GOToPTTK.Controllers.MvcControllers
{
    public class HomeController : Controller
    {
        private readonly IRouteListService _routeListService;

        public HomeController(IRouteListService routeListService)
        {
            _routeListService = routeListService;
        }

        public IActionResult Index()
        {

            return View();
        }
    }
}