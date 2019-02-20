using GOToPTTK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Responses
{
    public class TouristResponse
    {
        public UserResponse User { get; set; }

        public static TouristResponse BuildFromModel(Turysta tourist)
        {
            var t_res = new TouristResponse()
            {
                User = UserResponse.BuildFromModel(tourist.Uzytkownik)
            };

            return t_res;
        }
    }
}
