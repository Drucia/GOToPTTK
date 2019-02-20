using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOToPTTK.Model.Entities;

namespace GOToPTTK.Model.Services
{
    public class RouteListService : IRouteListService
    {

        public IList<OdcinekPunktowany> GetRoutesFromRouteList(IList<OdcinekPunktowany> routes, int routeListId)
        {
            return routes.Where(r => r.WykazTrasId == routeListId).ToList();
        }

        /// <summary>
        /// Metoda mówi o tym czy w podanej liście odcinków punktowanych istnieje odcinek równoważny parametrowi route -
        /// - to znaczy taki, który ma taki sam początek i koniec, ale równocześnie ma inne id
        /// W przypadku gdy któryś z parametrów ma wartość null lub lista odcinków jest pusta - zwracana jest wartość false
        /// </summary>
        /// <param name="routes">Lista odcinków</param>
        /// <param name="route">Porównywany odcinek</param>
        /// <returns>Wartość logiczna określająca czy w wykazie istnieje już równoważna trasa</returns>
        public bool EquivalentRouteExistsInRouteList(IList<OdcinekPunktowany> routes, OdcinekPunktowany route)
        {
            return route != null  && (routes?.Any(r => r.IsRouteEquivalent(route)) ?? false);
        }

    }
}
