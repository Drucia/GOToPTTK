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
    public class VerificationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IVerifyService _verifyService;

        public VerificationsController(ApplicationDbContext context, IVerifyService verifyService)
        {
            _context = context;
            _verifyService = verifyService;
        }

        [HttpPost]
        public IActionResult PostVerify([FromBody] VerifyRequest request)
        {
            bool isTripVerify = request.TripRouteId < 0;

            bool isGoodState = isTripVerify ? _verifyService.CanGuideVerifyTrip(_context.Wycieczka.ToList(), request.GuideId, request.TripId) : true;

            if (!ModelState.IsValid || !isGoodState)
            {
                return BadRequest(ModelState);
            }

            Weryfikacja newVerify = new Weryfikacja()
            {
                PrzodownikUzytkownikId = request.GuideId,
                DataWeryfikacji = request.VerifyDate,
                DataWyslaniaDoWeryfikacji = request.SendVerifyDate,
                Uwagi = request.Issues,
                WycieczkaId = request.TripId,
                StatusWeryfikacji = request.VerificationStatusId,
                OdcinekId = request.TripRouteId < 0 ? null : request.TripRouteId
            };

            if (_verifyService.ExistInVerificationsList(_context.Weryfikacja.ToList(), newVerify))
                return BadRequest("Taka weryfikacja już istnieje");
            else
            {
                _context.Weryfikacja.Add(newVerify);
                _context.SaveChanges();
            }

            if (!isTripVerify)
            {
                var route = _context.Odcinek.FirstOrDefault(r => r.Id == request.TripRouteId && r.WycieczkaId == request.TripId);
                route.Zweryfikowany = request.VerificationStatusId;
                _context.Odcinek.Update(route);
                _context.SaveChanges();
            }

            _verifyService.CheckForUpdateTripStatus(request.TripId, request.VerificationStatusId, _context, isTripVerify);

            return Ok(VerifyRequest.BuildFromModel(newVerify));
        }
    }
}