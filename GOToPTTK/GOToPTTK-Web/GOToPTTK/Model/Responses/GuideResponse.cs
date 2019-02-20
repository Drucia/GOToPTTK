using GOToPTTK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Responses
{
    public class GuideResponse
    {
        public UserResponse User { get; set; }

        public static GuideResponse BuildFromModel(Przodownik guide)
        {
            var g_res = new GuideResponse()
            {
                User = UserResponse.BuildFromModel(guide.Uzytkownik)
            };

            return g_res;
        }
    }
}
