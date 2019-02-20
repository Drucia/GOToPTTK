using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Responses
{
    public class CustomRouteRequest 
    {
        public PlaceResponse Start { get; set; }
        public PlaceResponse End { get; set; }
    }
}
