using GOToPTTK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Services
{
    public interface ITripListService
    {
        IList<Wycieczka> GetTripsToVerifyByGuide(IList<Wycieczka> trips, IList<UprawnieniePrzodownika> permissions, int guide);
        IList<Odcinek> GetTripRoutesToVerifyByGuide(IList<Odcinek> routes, IList<UprawnieniePrzodownika> permissions, int guide, int tID);
    }
}
