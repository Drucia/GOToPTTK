using GOToPTTK.Model.Entities;
using GOToPTTK.Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Services
{
    public interface IApiCustomRouteService
    {
         bool CreateCustomRoute(CustomRouteRequest route, out IList<RouteResponse> result);
    }
}
