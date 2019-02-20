using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOToPTTK.Model.Entities;
using GOToPTTK.Model.Extensions;
using GOToPTTK.Model.Responses;
using GOToPTTK.Model.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GOToPTTK.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;


        public PlacesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<PlaceResponse> GetPlaces(bool partOfRoute=false)
        {
            IEnumerable<Miejsce> places;
            if (partOfRoute)
            {
                var routes = _dbContext.GradedRoutesWithIncludedRelatedData();
                places = ApiCustomRouteService.FindPlacesFromRoutes(routes);
            }
            else
            {
                places = _dbContext.Miejsce.Where(p => p.Nazwa != null).ToList();
            }
            
            return places.Select(PlaceResponse.BuildFromModel);
        }
    }
}