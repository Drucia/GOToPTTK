using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GOToPTTK.Model.Entities;
using GOToPTTK.Model.Services;
using GOToPTTK.Model.Extensions;
using GOToPTTK.Model.Responses;

namespace GOToPTTK.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IRouteListService _routeListService;
        private readonly IApiCustomRouteService _apiCustomRouteService;

        public RoutesController(ApplicationDbContext context, IRouteListService service, IApiCustomRouteService apiService)
        {
            _context = context;
            _routeListService = service;
            _apiCustomRouteService = apiService;
        }

        // GET: api/Routes
        [HttpGet]
        public IEnumerable<RouteResponse> GetEffectiveRouteList()
        {
            var effectiveRouteList = _context.GetEffectiveRouteList();
            return effectiveRouteList.Select(r => RouteResponse.BuildFromModel(r));
        }

        [HttpPost]
        [Route("custom-route")]
        public IActionResult CreateCustomRoute(CustomRouteRequest route)
        {
            if (_apiCustomRouteService.CreateCustomRoute(route, out var customRoute))
            {
                return Ok(customRoute);
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
