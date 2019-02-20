using GOToPTTK.Model.Entities;
using System.Collections.Generic;


namespace GOToPTTK.Model.Services
{
    public interface IPlaceListService
    {
        bool ExistInPlaceList(IList<Miejsce> places, Miejsce place);
        bool IsPlaceInTrips(IList<Odcinek> route_trips, Miejsce place);
    }
}
