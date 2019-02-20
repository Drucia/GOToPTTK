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
    public class GuidesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITripListService _tripListService;
        private readonly IGuideService _guideService;

        public GuidesController(ApplicationDbContext context, ITripListService tripListService, IGuideService guideService)
        {
            _context = context;
            _tripListService = tripListService;
            _guideService = guideService;
        }

        // GET: api/Guides/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuide([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var przodownik = await _context.Przodownik.FindAsync(id);

            if (przodownik == null)
            {
                return NotFound();
            }

            return Ok(przodownik);
        }

        // GET: api/Guides/5/Trips
        [HttpGet("{id}/Trips")]
        public IEnumerable<TripResponse> GetWycieczkiDoWeryfikacji([FromRoute] int id)
        {
            var permissions = _context.UprawnieniePrzodownika.ToList();
            var tripsToVerify = _context.GradedTripsWithIncludedObjs();
            var score = _tripListService.GetTripsToVerifyByGuide(tripsToVerify, permissions, id).Select(t => TripResponse.BuildFromModel(t));
            return score;
        }

        // GET: api/Guides/5/Trips/2
        [HttpGet("{id}/Trips/{tId}")]
        public IEnumerable<TripRouteResponse> GetWycieczka([FromRoute] int id, [FromRoute] int tId)
        {
            var permissions = _context.UprawnieniePrzodownika.ToList();
            var routes = _context.GradedTripRoutesWithIncludedObjects();
            var score = _tripListService.GetTripRoutesToVerifyByGuide(routes, permissions, id, tId).Select(tr => TripRouteResponse.BuildFromModel(tr));
            return score;
        }
    }
}