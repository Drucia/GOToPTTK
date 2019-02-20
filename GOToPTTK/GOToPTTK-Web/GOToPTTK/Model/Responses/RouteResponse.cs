using GOToPTTK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Responses
{
    public class RouteResponse
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public PlaceResponse Start { get; set; }
        public PlaceResponse End { get; set; }
        public string MountainGroup { get; set; }

        public static RouteResponse BuildFromModel(OdcinekPunktowany route)
        {
            var response = new RouteResponse()
            {
                Id = route.Id,
                Points = route.Punkty,
                Start = PlaceResponse.BuildFromModel(route.Poczatek),
                End = PlaceResponse.BuildFromModel(route.Koniec),
                MountainGroup = route.GrupaGorska?.Nazwa
            };
            return response;
        }

        public static RouteResponse BuildFromModel(OdcinekWłasny route)
        {
            var response = new RouteResponse()
            {
                Id = route.Id,
                Points = route.Punkty,
                Start = PlaceResponse.BuildFromModel(route.Poczatek),
                End = PlaceResponse.BuildFromModel(route.Koniec),
                MountainGroup = route.GrupaGorska.Nazwa
            };
            return response;
        }

        public static RouteResponse CreateCustomRoute(PlaceResponse start, PlaceResponse end, string mountainGroup, double distance)
        {
            var response = new RouteResponse()
            {
                Id = -1,
                Points = 0,
                Start = start,
                End = end,
                MountainGroup = mountainGroup
            };
            response.CalculatePoints(distance);
            return response;
        }



        private void CalculatePoints(double distance)
        {
            Points = (int) distance;
            if (End.Altitude > 0 && Start.Altitude > 0)
            {
                var altitudeDifference = End.Altitude - Start.Altitude;
                Points += (int)(altitudeDifference / 100);
            }

        }

        public bool IsCustomRoute()
        {
            return Id < 0;
        }

 
    }
}
