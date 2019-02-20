using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOToPTTK.Model.Entities;
using GOToPTTK.Model.Responses;

namespace GOToPTTK.Model.Services
{
    interface ITripService
    {
        Wycieczka CreateTrip(PlannedTripRequest request);
    }
}
