using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOToPTTK.Model.Entities;
using GOToPTTK.Model.Responses;

namespace GOToPTTK.Model.Services
{
    public class TripService : ITripService
    {
        private readonly ApplicationDbContext _context;

        public TripService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Wycieczka CreateTrip(PlannedTripRequest request)
        {
            var newTrip = CreateModelFromRequest(request);
            _context.SaveChanges();
            return newTrip;
        }

        private Wycieczka CreateModelFromRequest(PlannedTripRequest request)
        {
            Wycieczka newTrip = new Wycieczka()
            {
                TurystaUzytkownikId = request.UserId,
                DataRozpoczecia = request.Date
            };

            _context.Wycieczka.Add(newTrip);
            _context.SaveChanges();


            foreach (var route in request.Routes)
            {
                CreateTripRoute(newTrip, route);
            }

            return newTrip;
        }

        private void CreateTripRoute(Wycieczka newTrip, RouteResponse route)
        {
            var tripRoute = new Odcinek()
            {
                WycieczkaId = newTrip.Id
            };
            if (route.IsCustomRoute())
            {
                var start = route.Start.ToModel();
                var end = route.End.ToModel();
                var mountainGroup = _context.GrupaGorska.FirstOrDefault(m => m.Nazwa == route.MountainGroup);
                _context.Update(start);
                _context.Update(end);
                _context.SaveChanges();
                OdcinekWłasny customRoute = new OdcinekWłasny()
                {
                    Punkty = route.Points,
                    PoczatekId = start.Id,
                    KoniecId = end.Id,
                    GrupaGorska = mountainGroup
                };
                _context.OdcinekWłasny.Add(customRoute);
                _context.SaveChanges();
                tripRoute.OdcinekWłasnyId = customRoute.Id;
            }
            else
            {
                tripRoute.OdcinekPunktowanyId = route.Id;
            }

            newTrip.Odcinek.Add(tripRoute);
        }
    }
}
