using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOToPTTK.Model.Entities;

namespace GOToPTTK.Model.Services
{
    public class TripListService : ITripListService
    {
        public IList<Wycieczka> GetTripsToVerifyByGuide(IList<Wycieczka> tripsToVerify, IList<UprawnieniePrzodownika> permissions, int guide)
        {
            var score = tripsToVerify.Where(t => t.Zweryfikowana.Equals("OCZEKUJACA"));
            return score.Where(t => (t.PrzodownikUzytkownikId == guide) || t.Odcinek.Any(r => (r.OdcinekPunktowanyId != null && permissions.Any(p => p.GrupaGorskaId == r.OdcinekPunktowany.GrupaGorskaId && p.PrzodownikUzytkownikId == guide))
            || (r.OdcinekWłasnyId != null && permissions.Any(p => p.GrupaGorskaId == r.OdcinekWłasny.GrupaGorskaId && p.PrzodownikUzytkownikId == guide)))).ToList();
        }

        public IList<Odcinek> GetTripRoutesToVerifyByGuide(IList<Odcinek> routes, IList<UprawnieniePrzodownika> permissions, int guide, int tID)
        {
            var routesByRegion = routes.Where(r => (r.OdcinekPunktowanyId != null && permissions.Any(p => p.GrupaGorskaId == r.OdcinekPunktowany.GrupaGorskaId && p.PrzodownikUzytkownikId == guide))
            || (r.OdcinekWłasnyId != null && permissions.Any(p => p.GrupaGorskaId == r.OdcinekWłasny.GrupaGorskaId && p.PrzodownikUzytkownikId == guide)));

            return routesByRegion.Where(rt => rt.WycieczkaId == tID).ToList();
        }

    }
}
