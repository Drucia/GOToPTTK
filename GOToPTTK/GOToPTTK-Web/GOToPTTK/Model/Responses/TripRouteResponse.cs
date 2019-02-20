using GOToPTTK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Responses
{
    public class TripRouteResponse
    {
        public int Id { get; set; }
        public RouteResponse Route { get; set; }
        public StateResponse State { get; set; }

        public static TripRouteResponse BuildFromModel(Odcinek route)
        {
            var response = new TripRouteResponse()
            {
                Id = route.Id,
                Route = route.OdcinekPunktowanyId != null ? RouteResponse.BuildFromModel(route.OdcinekPunktowany) : RouteResponse.BuildFromModel(route.OdcinekWłasny),
                State = StateResponse.BuildFromModel(route.ZweryfikowanyNavigation)
            };

            return response;
        }
    }
}
