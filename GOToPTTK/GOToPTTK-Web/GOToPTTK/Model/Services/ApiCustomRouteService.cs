using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOToPTTK.Model.Extensions;
using GOToPTTK.Model.Entities;
using GOToPTTK.Model.Responses;
using QuickGraph;
using QuickGraph.Algorithms.ShortestPath;
using QuickGraph.Algorithms.Observers;

namespace GOToPTTK.Model.Services
{
    public class ApiCustomRouteService : IApiCustomRouteService
    {
        private const int EARTH_RADIUS_KM = 6371;
        private const decimal RADIANS_CONSTANT = 180m;
        private const double MAXIMUM_DISTANCE = 10;
        private const double MINIMUM_DISTANCE = 2;
        private readonly ApplicationDbContext _context;

        public ApiCustomRouteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateCustomRoute(CustomRouteRequest route, out IList<RouteResponse> result)
        {
            var routes = _context.GradedRoutesWithIncludedRelatedData();
            var places = FindPlacesFromRoutes(routes);

            PlaceResponse start = route.Start;
            PlaceResponse end = route.End;

            double distanceFromStart = FindClosestPlace(start, places, out Miejsce closestToStart);
            double distanceFromEnd = FindClosestPlace(end, places, out Miejsce closestToEnd);

            if (distanceFromStart <= MAXIMUM_DISTANCE && distanceFromEnd <= MAXIMUM_DISTANCE)
            {
                var mountainGroup = FindMountainGroup(closestToStart);
                var mountainGroupName = _context.GrupaGorska.First(m => m.Id == mountainGroup).Nazwa;
                routes = routes.Where(r => r.GrupaGorskaId == mountainGroup).ToList();
                places = FindPlacesFromRoutes(routes);

                //create graph from locations in a mountain group
                var graph = new BidirectionalGraph<PlaceVertex, Edge<PlaceVertex>>();
                var edgeCostDictionary = new Dictionary<Edge<PlaceVertex>, int>();
                PopulateGraph(graph, edgeCostDictionary, places, routes);

                //find path between start location and end location using A* Algorithm
                 AStarShortestPathAlgorithm<PlaceVertex, Edge<PlaceVertex>> astar = new AStarShortestPathAlgorithm<PlaceVertex, Edge<PlaceVertex>>(graph, x => edgeCostDictionary[x], x => 0 );
                 var startVertex = new PlaceVertex(closestToStart.Id);
                 var endVertex = new PlaceVertex(closestToEnd.Id);

                if(astar.ComputeDistanceBetween(startVertex, endVertex, out var path))
                {
                    result = new List<RouteResponse>();
                    if(distanceFromStart >= MINIMUM_DISTANCE)
                    {
                        result.Add(RouteResponse.CreateCustomRoute(start, PlaceResponse.BuildFromModel(closestToStart), mountainGroupName, distanceFromStart));   
                    }
                    result = result.Concat(CreateResponseListFromPath(path)).ToList();
                    if (distanceFromEnd >= MINIMUM_DISTANCE)
                    {
                        result.Add(RouteResponse.CreateCustomRoute( PlaceResponse.BuildFromModel(closestToEnd), end, mountainGroupName, distanceFromEnd));
                    }
                    return true;
                }
                else
                {
                    result = new List<RouteResponse>();
                    return false;
                }  
            }
            else
            {
                result = new List<RouteResponse>();
                return false;

            }
        }

        private IList<RouteResponse> CreateResponseListFromPath(IEnumerable<IEdge<PlaceVertex>> path)
        {
            List<OdcinekPunktowany> routes = _context.GradedRoutesWithIncludedRelatedData().ToList();
            List<RouteResponse> responses = new List<RouteResponse>();
            foreach (var edge in path)
            {
                var route = routes.First(r => r.Id == edge.Source.RouteId);
                responses.Add(RouteResponse.BuildFromModel(route));
            }
            return responses;
        }

        private int FindMountainGroup(Miejsce place)
        {
            return place.OdcinekPunktowanyPoczatek?.FirstOrDefault()?.GrupaGorskaId ?? place.OdcinekPunktowanyKoniec.FirstOrDefault().GrupaGorskaId;
        }

        public static IList<Miejsce> FindPlacesFromRoutes(IList<OdcinekPunktowany> routes)
        {
            var beginnings = routes.Select(r => r.Poczatek).Where(p => p.Nazwa != null);
            var endings = routes.Select(r => r.Koniec).Where(p => p.Nazwa != null);
            return  beginnings.Concat(endings).Distinct().ToList();
        }

        private void PopulateGraph(BidirectionalGraph<PlaceVertex, Edge<PlaceVertex>> graph, Dictionary<Edge<PlaceVertex>, int> edgeCostDictionary, IList<Miejsce> places, IList<OdcinekPunktowany> routes)
        {
            foreach (var place in places){
                var vertex = new PlaceVertex(place.Id);
                graph.AddVertex(vertex);
            }
            foreach (var route in routes)
            {
                var start = new PlaceVertex(route.PoczatekId, route.Id);
                var end = new PlaceVertex(route.KoniecId, route.Id);
                Edge<PlaceVertex> edge = new Edge<PlaceVertex>(start, end);
                graph.AddEdge(edge);
                edgeCostDictionary.Add(edge, route.Punkty);
            }
        }

        private double FindClosestPlace(PlaceResponse response, IList<Miejsce> places, out Miejsce closestPlace)
        {
            decimal lat = response.Latitude;
            decimal lon = response.Longitude;
            closestPlace = places.OrderBy(p => DistanceInKmBetweenEarthCoordinates(p.SzerokoscGeograficzna, lat, p.DlugoscGeograficzna, lon)).First();
            var distance = DistanceInKmBetweenEarthCoordinates(closestPlace.SzerokoscGeograficzna, lat, closestPlace.DlugoscGeograficzna, lon);
            return distance;
        }

        private static double DistanceInKmBetweenEarthCoordinates(decimal lat1, decimal lat2, decimal lon1, decimal lon2)
        {

            var dLat = (double)DegreesToRadians(lat2 - lat1);
            var dLon = (double)DegreesToRadians(lon2 - lon1);

            lat1 = DegreesToRadians(lat1);
            lat2 = DegreesToRadians(lat2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos((double)lat1) * Math.Cos((double)lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EARTH_RADIUS_KM * c;
        }

        private static decimal DegreesToRadians(decimal degrees)
        {
            decimal pi = (decimal)Math.PI;
            return degrees * pi / RADIANS_CONSTANT;
        }

        private class PlaceVertex
        {
            public PlaceVertex(int placeId)
            {
                PlaceId = placeId;
            }

            public PlaceVertex(int placeId, int routeId) : this(placeId)
            {
                RouteId = routeId;
            }

            public int PlaceId { get; set; } = -1;
            public int RouteId { get; set; } = -1;

            public override bool Equals(object obj)
            {
                return obj is PlaceVertex vertex &&
                       PlaceId == vertex.PlaceId;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(PlaceId);
            }
            public override string ToString()
            {
                return PlaceId.ToString();
            }
        }

      
    }

}
