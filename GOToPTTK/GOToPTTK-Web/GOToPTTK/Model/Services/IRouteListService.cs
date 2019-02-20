using GOToPTTK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Services
{
    public interface IRouteListService
    {
        IList<OdcinekPunktowany> GetRoutesFromRouteList(IList<OdcinekPunktowany> routes, int routeListId);
        bool EquivalentRouteExistsInRouteList(IList<OdcinekPunktowany> routes, OdcinekPunktowany route);
        
    }
}
