using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOToPTTK.Model.Entities;

namespace GOToPTTK.Model.Services
{
    public class PlaceListService : IPlaceListService
    {
        public bool ExistInPlaceList(IList<Miejsce> places, Miejsce place)
        {
            return place != null && (places?.Any(p => p.IsEqual(place)) ?? false);
        }

        public bool IsPlaceInTrips(IList<Odcinek> trip_routes, Miejsce place)
        {
            return place == null || (trip_routes?.Any(r => (r.OdcinekPunktowanyId != null && (r.OdcinekPunktowany.PoczatekId == place.Id || r.OdcinekPunktowany.KoniecId == place.Id)) ||
            (r.OdcinekWłasnyId != null && (r.OdcinekWłasny.PoczatekId == place.Id || r.OdcinekWłasny.KoniecId == place.Id))) ?? false);
        }
    }
}
