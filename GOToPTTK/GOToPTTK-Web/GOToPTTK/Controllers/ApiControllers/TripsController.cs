using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GOToPTTK.Model.Entities;
using GOToPTTK.Model.Services;
using GOToPTTK.Model.Responses;
using GOToPTTK.Model.Extensions;
using GOToPTTK.Model;

namespace GOToPTTK.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITripService _service;
       
        public TripsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Trips
        [HttpPost]
        public IActionResult PostTrip([FromBody] PlannedTripRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newTrip = _service.CreateTrip(request);  
            return CreatedAtRoute("GetTrip", new {tripId = newTrip.Id}, request);
        }


        [HttpGet]
        [Route("{tripId}", Name = "GetTrip")]
        public IActionResult GetTrip(int tripId)
        {
            
            var trip = _context.TripsWithIncludedRelatedData().First(t => t.Id == tripId);
            if (trip == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(PlannedTripRequest.BuildFromModel(trip));
            }
        }
        // GET: api/Trips/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWycieczka([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var wycieczka = await _context.Wycieczka.FindAsync(id);

            if (wycieczka == null)
            {
                return NotFound();
            }

            return Ok(wycieczka);
        }

        // PUT: api/Trips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWycieczka([FromRoute] int id, [FromBody] Wycieczka wycieczka)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != wycieczka.Id)
            {
                return BadRequest();
            }

            _context.Entry(wycieczka).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WycieczkaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

//        // POST: api/Trips
//        [HttpPost]
//        public async Task<IActionResult> PostWycieczka([FromBody] Wycieczka wycieczka)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//
//            _context.Wycieczka.Add(wycieczka);
//            await _context.SaveChangesAsync();
//
//            return CreatedAtAction("GetWycieczka", new { id = wycieczka.Id }, wycieczka);
//        }

        // DELETE: api/Trips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWycieczka([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var wycieczka = await _context.Wycieczka.FindAsync(id);
            if (wycieczka == null)
            {
                return NotFound();
            }

            _context.Wycieczka.Remove(wycieczka);
            await _context.SaveChangesAsync();

            return Ok(wycieczka);
        }

        private bool WycieczkaExists(int id)
        {
            return _context.Wycieczka.Any(e => e.Id == id);
        }
    }
}