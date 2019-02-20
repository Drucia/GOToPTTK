using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GOToPTTK.Controllers.MvcControllers
{
    public class ManagePlaceListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("dodano-punkt", Name = "CreateSuccess")]
        public IActionResult CreateSuccess()
        {
            return View();
        }
    }
}