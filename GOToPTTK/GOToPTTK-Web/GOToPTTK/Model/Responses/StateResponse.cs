using GOToPTTK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Responses
{
    public class StateResponse
    {
        public string State { get; set; }

        public static StateResponse BuildFromModel(StatusWeryfikacji state)
        {
            var s_res = new StateResponse()
            {
                State = state.Status
            };

            return s_res;
        }
    }
}
