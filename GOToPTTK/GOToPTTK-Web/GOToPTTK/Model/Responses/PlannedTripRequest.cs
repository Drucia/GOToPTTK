using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOToPTTK.Model.Entities;

namespace GOToPTTK.Model.Responses
{
    public class PlannedTripRequest
    {
        public List<RouteResponse> Routes { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }

        public static PlannedTripRequest BuildFromModel(Wycieczka trip)
        {
            var routes = new List<RouteResponse>();
            foreach (var route in trip.Odcinek)
            {
                if (route.OdcinekPunktowany != null)
                {
                    routes.Add(RouteResponse.BuildFromModel(route.OdcinekPunktowany));
                }
                else if (route.OdcinekWłasny != null)
                {
                    routes.Add(RouteResponse.BuildFromModel(route.OdcinekWłasny));
                }
                
            }

            return new PlannedTripRequest()
            {
                Date = trip.DataRozpoczecia,
                UserId = trip.TurystaUzytkownikId,
                Routes = routes
            };
        }
    }
}
