using GOToPTTK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Responses
{
    public class TripResponse
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TouristResponse Tourist { get; set; }
        public GuideResponse Guide { get; set; }
        public StateResponse State { get; set; }
        public IEnumerable<TripRouteResponse> Routes { get; set; }

        public static TripResponse BuildFromModel(Wycieczka trip)
        {
            var t_res = new TripResponse()
            {
                Id = trip.Id,
                Points = trip.SumaPunktow,
                StartDate = trip.DataRozpoczecia,
                EndDate = trip.DataZakonczenia,
                Tourist = TouristResponse.BuildFromModel(trip.TurystaUzytkownik),
                Guide = trip.PrzodownikUzytkownikId == null ? null : GuideResponse.BuildFromModel(trip.PrzodownikUzytkownik),
                State = StateResponse.BuildFromModel(trip.ZweryfikowanaNavigation),
                Routes = trip.Odcinek.Select(r => TripRouteResponse.BuildFromModel(r))
            };

            return t_res;
        }

    }
}
